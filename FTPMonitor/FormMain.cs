using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using System.Runtime.InteropServices;
using FTPMonitor.Data;
using LogLib;
using FTPMonitor.Forms;

namespace FTPMonitor
{
    public partial class FormMain : Form
    {
        #region Property
        public static FormMain Current;//当前主窗体实例

        List<string> CurrentCopyFile;//保持当前正在推送的数据对象

        double east, west, south, north;//东西南北 经纬度
        string monitorFolder, destFolder;//监控目录，目标目录

        bool isInited = false;
        private static bool isRun;
        long loadSum;
        #endregion

        #region 系统
        public FormMain()
        {
            InitializeComponent();
            RegisterEvent();
            InitFormStyle();
            Current = this;
        }
        /// <summary>
        /// 初始化界面
        /// </summary>
        void InitFormStyle()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.dtCreateTime.Checked = false;
            this.dtPhotoTime.Checked = false;
        }
        /// <summary>
        /// 注册控件事件
        /// </summary>
        void RegisterEvent()
        {
            this.FSWatcher.Changed += new FileSystemEventHandler(FSWatcher_Changed);
            this.FSWatcher.Deleted += new FileSystemEventHandler(FSWatcher_Deleted);

            this.btnStart.Click += new EventHandler(btnStart_Click);//启动
            this.btnFind.Click += new EventHandler(btnFind_Click);//查找复制
            this.btnQuery.Click += new EventHandler(btnQuery_Click);//统计

            this.btnScanMonitor.Click += new EventHandler(btnScanMonitor_Click);//选择监控目录
            this.btnScanDest.Click += new EventHandler(btnScanDest_Click);//选择复制目标目录

            //注册日志信息事件
            LogManager.ShowLogEvent += new Action<string>(LogManager_ShowLogEvent);

            #region 窗体显示隐藏事件注册
            this.toolStripMenuItemCloseForm.Click += new EventHandler(toolStripMenuItemCloseForm_Click);
            this.toolStripMenuItemShowForm.Click += new EventHandler(toolStripMenuItemShowForm_Click);
            this.notifyIconServer.MouseDoubleClick += new MouseEventHandler(notifyIconServer_MouseDoubleClick);
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
            #endregion
        }
        #endregion

        #region 监控
        /// <summary>
        /// 监控文件目录数据改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FSWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            //FullPath —E:\test\123456\GF1_WFV1_E117.6_N33.0_20130724_L1A0000057813.tar.gz
            string fullpath = e.FullPath;
            //如果是增加的是目录则返回
            if ((File.GetAttributes(fullpath) & FileAttributes.Directory) == FileAttributes.Directory)
                return;
            // FileSystemWather在数据刚开始复制和复制结束时都会触发Changed事件
            // 使用List持有对象，数据刚开始复制则添加持有该对象，复制结束后移除
            bool isCopyOk = CurrentCopyFile.Contains(fullpath);
            if (isCopyOk)//是否已经在FTP上准备好的数据
            {
                CurrentCopyFile.Remove(fullpath);
                DataInfo datainfo = DataFactory.CreateDataInfo(e.Name, fullpath);
                if (datainfo == null)
                {
                    return;
                }
                DataOperate.GetInstance().AddNewData(datainfo);
            }
            else
            {
                CurrentCopyFile.Add(fullpath);
            }
        }
        /// <summary>
        /// 检测到文件删除，移除记录，防止检索出现数据不同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FSWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            DataOperate.GetInstance().RemoveData(e.FullPath);//触发数据删除事件
        }
        #endregion

        #region 启动

        /// <summary>
        /// 开始监控，进行初始化工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnStart_Click(object sender, EventArgs e)
        {
            monitorFolder = this.tbMonitorFolder.Text;
            if (string.IsNullOrEmpty(monitorFolder))
            {
                MessageBox.Show("监控目录为空，请重新输入！");
                return;
            }
            if (!Directory.Exists(monitorFolder))
            {
                MessageBox.Show("监控目录不存在，请重新输入！");
                return;
            }
            ChangeCursor(Cursors.WaitCursor);
            LogManager.AddMessage(MessageType.INFO, "系统开始初始化... ...");
            isRun = true;//系统开始运行
            CurrentCopyFile = new List<string>();
            this.btnStart.Enabled = false;
            this.btnScanMonitor.Enabled = false;
            InitDatabase();
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void InitDatabase()
        {
            //程序启动置已有记录为不存在
            int count = DataOperate.GetInstance().SetDataNotExist();
            if (count < 0)
            {
                MessageBox.Show("程序初始化出错，无法清空数据库：！");
                ChangeCursor(Cursors.Default);
                return;
            }
            //遍历文件夹将存在的数据设置存在
            loadSum = 0;
            Action<DirectoryInfo> delegateTraverse = TraverseDir;
            delegateTraverse.BeginInvoke(new DirectoryInfo(monitorFolder), new AsyncCallback(CallBackTraverse), delegateTraverse);

        }

        /// <summary>
        /// 回调函数，加载完数据后续操作
        /// </summary>
        /// <param name="iasync"></param>
        private void CallBackTraverse(IAsyncResult iasync)
        {
            LogManager.AddMessage(MessageType.INFO, string.Format("初始化完成，共加载【{0}】条数据", loadSum));
            isInited = true;

            this.FSWatcher.Path = monitorFolder;
            this.FSWatcher.IncludeSubdirectories = true;
            this.FSWatcher.Filter = "*.tar.gz";

            ChangeCursor(Cursors.Default);
        }

        /// <summary>
        /// 加载已有数据
        /// </summary>
        /// <param name="dir"></param>
        private void TraverseDir(DirectoryInfo dir)
        {
            if (isExit)
            {
                Thread.CurrentThread.Interrupt();
            }
            foreach (DirectoryInfo nextDir in dir.GetDirectories())
            {
                TraverseDir(nextDir);
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                int count = DataOperate.GetInstance().UpdateOrInsertData(file.Name, file.FullName);
                if (count < 0)
                {
                    string str = new StringBuilder().AppendFormat("加载已有数据{0}入库错误", file.Name).ToString();
                    LogManager.AddMessage(MessageType.ERR, str);
                    continue;
                }
                loadSum++;
            }
        }
        #endregion

        #region 统计
        /// <summary>
        /// 统计按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnQuery_Click(object sender, EventArgs e)
        {
            if (!isInited)
            {
                MessageBox.Show("请先点击【启动】按钮对系统进行初始化！");
                return;
            }
            //FormTongJi ft = new FormTongJi();
            FormTJ ft = new FormTJ();
            ft.ShowDialog();
        }
        #endregion

        #region 查找复制
        /// <summary>
        /// 查找功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnFind_Click(object sender, EventArgs e)
        {
            if (!isInited)
            {
                MessageBox.Show("请先点击【启动】按钮对系统进行初始化！");
                return;
            }
            if (!VerifyVariable())
            {
                return;
            }
            string photoTime = "", createTime = "";
            if (this.dtPhotoTime.Checked)
            {
                DateTime dt = DateTime.Parse(this.dtPhotoTime.Text);
                photoTime = string.Format("{0:yyyyMMdd}", dt);
            }
            if (this.dtCreateTime.Checked)
            {
                createTime = this.dtCreateTime.Text;
            }
            FileOperate fileOperate = new FileOperate(this.destFolder);
            fileOperate.ExecuteCopy(south.ToString(), north.ToString(), west.ToString(), east.ToString(), photoTime, createTime);
        }
        /// <summary>
        /// 初始化系统变量并验证其正确性
        /// </summary>
        /// <returns></returns>
        bool VerifyVariable()
        {
            destFolder = this.tbDestFolder.Text;
            if (destFolder == "")
            {
                MessageBox.Show("输出路径为空，请重新输入！");
                return false;
            }
            if (!double.TryParse(this.tbEast.Text, out east))
            {
                MessageBox.Show("经度范围结束值有错误，请重新输入！");
                return false;
            }
            if (!double.TryParse(this.tbWest.Text, out west))
            {
                MessageBox.Show("经度范围开始值有错误，请重新输入！");
                return false;
            }
            if (!double.TryParse(this.tbSouth.Text, out south))
            {
                MessageBox.Show("纬度范围开始值有错误，请重新输入！");
                return false;
            }
            if (!double.TryParse(this.tbNorth.Text, out north))
            {
                MessageBox.Show("纬度范围结束值输入有错误，请重新输入！");
                return false;
            }
            return true;
        }
        #endregion
    }
}
