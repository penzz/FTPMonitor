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
        string monitorFolder = "";
        long loadSum;

        public FormStart()
        {
            InitializeComponent();
            this.btnStart.Click += btnStart_Click;
            this.btnExit.Click += btnExit_Click;
            this.btnScanMonitor.Click += btnScanMonitor_Click;
        }
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
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            Application.Exit();
        }
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
            CC.monitorFolder = monitorFolder;
            LogManager.AddMessage(MessageType.INFO, "系统开始初始化... ...");
            UpdateSystemInfo("系统开始初始化... ...");
            this.btnStart.Enabled = false;
            this.btnScanMonitor.Enabled = false;
            this.btnExit.Enabled = false;
            InitDatabase();
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void InitDatabase()
        {
            UpdateSystemInfo("开始置位数据库... ...");
            //程序启动置已有记录为不存在
            int count = DataOperate.GetInstance().SetDataNotExist();
            if (count < 0)
            {
                MessageBox.Show("程序初始化出错，无法置位数据库：！");
                return;
            }
            loadSum = 0;
            UpdateSystemInfo("开始扫描数据... ...");
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
            UpdateSystemInfo(string.Format("初始化完成，共加载【{0}】条数据", loadSum));
            Thread.Sleep(1000);
            UpdateSystemInfo("正在打开操作界面... ...");
            Thread.Sleep(1000);
            this.DialogResult = DialogResult.OK;
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
                UpdateSystemInfo(string.Format("发现数据{0}... ...", file.Name));
                loadSum++;
            }
        }
        /// <summary>
        /// 更新系统执行信息
        /// </summary>
        /// <param name="str"></param>
        private void UpdateSystemInfo(string str)
        {
            this.tooStatusLabelInfo.Text = "[" + DateTime.Now.ToString() + "] " + str;
        }
    }
}
