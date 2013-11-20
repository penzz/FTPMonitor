using FTPMonitor.Data;
using LogLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FTPMonitor
{
    public partial class FormStart : Form
    {
        #region Property
        object obj = new object();//多线程锁对象
        FormFTPMain ftpForm;
        string monitorFolder = "";
        List<string> CurrentCopyFile;//保持当前正在推送的数据对象
        private static bool isRun;
        long loadSum;
        #endregion

        #region 系统
        public FormStart()
        {
            InitializeComponent();
            RegisterMethod();
        }

        private void RegisterMethod()
        {
            this.btnStart.Click += btnStart_Click;
            this.btnScanMonitor.Click += btnScanMonitor_Click;
            this.btnOperate.Click += btnOperate_Click;
            this.ToolStripMenuItemQueryData.Click += ToolStripMenuItemQueryData_Click;
            #region 窗体显示隐藏事件注册
            this.toolStripMenuItemCloseForm.Click += new EventHandler(toolStripMenuItemCloseForm_Click);
            this.toolStripMenuItemShowForm.Click += new EventHandler(toolStripMenuItemShowForm_Click);
            this.notifyIconServer.MouseDoubleClick += new MouseEventHandler(notifyIconServer_MouseDoubleClick);
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
            #endregion

            this.FSWatcher.Changed += FSWatcher_Changed;//监控新数据到达
            this.FSWatcher.Deleted += FSWatcher_Deleted;//监控数据删除
            LogManager.ShowLogEvent += LogManager_ShowLogEvent;//注册日志信息事件
        }

        #endregion

        #region method
        /// <summary>
        /// 显示操作数据窗体
        /// </summary>
        private void ShowFTPMain()
        {
            if (!isRun)
            {
                DialogResult dr = MessageBox.Show(this,"系统尚未系统，检索到的结果可能不是最新数据，是否仍然检索？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr != DialogResult.OK)
                {
                    return;
                }
            }
            if (ftpForm == null || ftpForm.IsDisposed)
            {
                ftpForm = new FormFTPMain();
                //ftpForm.Owner = this;
                HideForm();
                ftpForm.Show();
            }
            else
            {
                ftpForm.WindowState = FormWindowState.Maximized;
            }
        }
        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="msg"></param>
        public void AddMessage(string msg)
        {
            if (this.rtbMessage.InvokeRequired)
            {
                Action<string> action = new Action<string>(AddMessage);
                rtbMessage.Invoke(action, msg);
            }
            else
            {
                lock (obj)
                {
                    if (this.rtbMessage.Text.Length > 5000)
                    {
                        this.rtbMessage.Clear();
                        rtbMessage.AppendText("[消息]日志信息显示已经刷新,如果要查看历史信息,请查看程序运行目录Logs下的日志文件:log-Message.txt" + Environment.NewLine);
                    }
                    //rtbMessage.AppendText(string.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}{2}", DateTime.Now, msg, Environment.NewLine));
                    rtbMessage.AppendText(msg);
                }
            }
        }

        /// <summary>
        /// 改变窗体鼠标手势
        /// </summary>
        /// <param name="cursor"></param>
        public void ChangeCursor(Cursor cursor)
        {
            if (this.InvokeRequired)
            {
                Action<Cursor> action = new Action<Cursor>(ChangeCursor);
                this.Invoke(action, cursor);
            }
            else
            {
                this.Cursor = cursor;
            }
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

        #region Events
        /// <summary>
        /// 浏览监控目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnScanMonitor_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.tbMonitorFolder.Text = fbd.SelectedPath;
            }
        }
        /// <summary>
        /// 显示操作主窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOperate_Click(object sender, EventArgs e)
        {
            ShowFTPMain();
        }
        /// <summary>
        /// 任务栏快捷操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ToolStripMenuItemQueryData_Click(object sender, EventArgs e)
        {
            ShowFTPMain();
        }
        /// <summary>
        /// 日志事件
        /// </summary>
        /// <param name="obj"></param>
        void LogManager_ShowLogEvent(string obj)
        {
            AddMessage(obj);
        }
        #endregion

        #region 启动
        /// <summary>
        /// 执行按钮
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
            isRun = true;
            LogManager.AddMessage(MessageType.INFO, "系统开始初始化... ...");
            this.btnStart.Enabled = false;
            this.btnScanMonitor.Enabled = false;
            InitDatabase();
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void InitDatabase()
        {
            LogManager.AddMessage(MessageType.INFO, "开始置位数据库... ...");
            //程序启动置已有记录为不存在
            int count = DataOperate.GetInstance().SetDataNotExist();
            if (count < 0)
            {
                MessageBox.Show("程序初始化出错，无法置位数据库：！");
                return;
            }
            loadSum = 0;
            LogManager.AddMessage(MessageType.INFO, "开始扫描数据... ...");
            //遍历文件夹将存在的数据设置存在
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
            CurrentCopyFile = new List<string>();
            this.FSWatcher.Path = monitorFolder;
            this.FSWatcher.IncludeSubdirectories = true;
            this.FSWatcher.Filter = "*.tar.gz";
        }

        /// <summary>
        /// 加载已有数据
        /// </summary>
        /// <param name="dir"></param>
        private void TraverseDir(DirectoryInfo dir)
        {
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

        #region 窗体关闭显示与隐藏
        private bool isExit = false;
        private bool isHide = false;
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit == false && isRun)
            {
                HideForm();
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 托盘图标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIconServer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isHide)
                ShowForm();
            else
                HideForm();
        }
        /// <summary>
        /// 显示主窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemShowForm_Click(object sender, EventArgs e)
        {
            if (isHide)
            {
                ShowForm();
            }
        }
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCloseForm_Click(object sender, EventArgs e)
        {
            CloseMainForm();
        }
        /// <summary>
        /// 关闭主窗体
        /// </summary>
        private void CloseMainForm()
        {
            if (!isRun)
            {
                isExit = true;
                this.Close();
                Application.Exit();
            }
            else
            {
                DialogResult result = MessageBox.Show(this, "严重警告：\r\n\r\n如果关闭该窗体,系统纪录的信息都将丢失！\r\n\r\n是否仍要关闭?", "严重警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)//如果选择是,则直接关闭,
                {
                    return;
                }
                else
                {
                    isExit = true;
                    this.Close();
                    Application.Exit();
                }
            }
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        private void ShowForm()
        {
            isHide = false;
            this.Show();
            this.Activate();
        }
        /// <summary>
        /// 隐藏窗体
        /// </summary>
        private void HideForm()
        {
            isHide = true;
            this.Hide();
        }
        #endregion
    }
}
