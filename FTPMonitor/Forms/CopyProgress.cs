using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LogLib;
using System.Data.SqlClient;
using FTPMonitor.DB;
using System.Threading;

namespace FTPMonitor.Forms
{
    /// <summary>
    /// 完成复制的类
    /// </summary>
    public class CopyProgress
    {
        /// <summary>
        /// 上报进度事件，供外部注册计算
        /// </summary>
        public event EventHandler<ProgressInfoEventArgs> ReportCopyProgress;
        private static object obj = new object();//线程同步锁
        public string destFolder;
        string append;

        public CopyProgress(string destFolder, string append)
        {
            this.destFolder = destFolder;
            this.append = append;
        }
        /// <summary>
        /// 开始复制过程，外部调用
        /// </summary>
        public void StartCopy()
        {
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            StringBuilder sql = new StringBuilder("select * from datainfo where 1=1 ");
            sql.Append(append);
            SqlDataReader reader = DataBaseControl.RunSqlForDataReader(sql.ToString());
            while (reader.Read())
            {
                string name = reader["name"].ToString();
                string fullpath = reader["fullpath"].ToString();
                if (!File.Exists(fullpath))
                {
                    ProgressInfoEventArgs e = new ProgressInfoEventArgs(MessageType.ERR, name);
                    OnReportCopyProgress(e);
                    continue;
                }
                Func<string, string, ProgressInfoEventArgs> copyAction = CopyFile;
                copyAction.BeginInvoke(name, fullpath, new AsyncCallback(CopyFileCallBack), copyAction);
            }
            reader.Close();
            DataBaseControl.CloseConnection();
        }
        /// <summary>
        /// 触发上报进度事件
        /// </summary>
        /// <param name="e"></param>
        private void OnReportCopyProgress(ProgressInfoEventArgs e)
        {
            EventHandler<ProgressInfoEventArgs> temp = Interlocked.CompareExchange(ref ReportCopyProgress, null, null);
            if (temp != null)
            {
                temp(this, e);
            }
        }

        #region 操作文件
        /// <summary>
        /// 将数据从FTP监控目录复制到目标目录
        /// </summary>
        /// <param name="datainfo"></param>
        private ProgressInfoEventArgs CopyFile(string name, string fullpath)
        {
            string destFullPath = Path.Combine(destFolder, name);
            if (!File.Exists(destFullPath))
            {
                bool isCanCopy = false;
                while (!isCanCopy)
                {
                    try
                    {
                        File.Copy(fullpath, destFullPath, true);
                        isCanCopy = true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(1000);
                    }
                }
                return new ProgressInfoEventArgs(MessageType.COPY, name);
            }
            else
            {
                return new ProgressInfoEventArgs(MessageType.EXIST, name);
            }
        }
        /// <summary>
        /// 复制完成回调函数，执行上报复制文件信息，计算复制进度
        /// </summary>
        /// <param name="iasync"></param>
        private void CopyFileCallBack(IAsyncResult iasync)
        {
            Func<string, string, ProgressInfoEventArgs> copyAction = (Func<string, string, ProgressInfoEventArgs>)iasync.AsyncState;
            ProgressInfoEventArgs e = copyAction.EndInvoke(iasync);
            OnReportCopyProgress(e);
        }
        /// <summary>
        /// 向文件中写记录
        /// </summary>
        /// <param name="record"></param>
        /// <param name="filePath"></param>
        public static void WriteRecord(string record, string filePath)
        {
            lock (obj)
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Append))
                    {
                        StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                        sw.WriteLine(record);
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion
    }
}
