using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Text;
using FTPMonitor.Forms;

namespace FTPMonitor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 防止启动多个客户端
            bool bCreateNew;
            Mutex mutex = new Mutex(true, "OnlyRunOneInstance", out bCreateNew);
            if (!bCreateNew)
            {
                MessageBox.Show("已有客户端在运行，不能同时启动两个客户端！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            #region 调试时数据库路径相关
            string dataDir = AppDomain.CurrentDomain.BaseDirectory;
            if (dataDir.EndsWith("\\bin\\Debug\\") || dataDir.EndsWith("\\bin\\Release\\"))
            {
                dataDir = System.IO.Directory.GetParent(dataDir).Parent.Parent.FullName;
                AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);
            }
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormStart());
        }
    }
}
