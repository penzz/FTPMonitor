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
    /// 上报进度的信息
    /// </summary>
    public class ProgressInfo
    {
        /// <summary>
        /// 信息类型
        /// </summary>
        public MessageType MT { get; set; }
        /// <summary>
        /// 复制完成的数据名
        /// </summary>
        public string name { get; set; }

        public ProgressInfo(MessageType MT, string name)
        {
            this.MT = MT;
            this.name = name;
        }
    }
    /// <summary>
    /// 完成复制的类
    /// </summary>
    public class CopyProgress
    {
        /// <summary>
        /// 上报进度事件，供外部注册计算
        /// </summary>
        public event Action<ProgressInfo> ReportCopyProgress;
        private delegate ProgressInfo CopyFileDelegate(string name, string fullpath);
        private static object obj = new object();
        public string destFolder;
        string append;

        public CopyProgress(string destFolder, string append)
        {
            this.destFolder = destFolder;
            this.append = append;
        }

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
                    if (ReportCopyProgress != null)
                    {
                        ReportCopyProgress(new ProgressInfo(MessageType.ERR, name));
                    }
                    continue;
                }
                CopyFileDelegate copyAction = new CopyFileDelegate(CopyFile);
                copyAction.BeginInvoke(name, fullpath, new AsyncCallback(CopyFileCallBack), copyAction);
            }
            reader.Close();
            DataBaseControl.CloseConnection();
        }

        #region 操作文件
        /// <summary>
        /// 将数据从FTP监控目录复制到目标目录
        /// </summary>
        /// <param name="datainfo"></param>
        private ProgressInfo CopyFile(string name, string fullpath)
        {
            if (!File.Exists(destFolder + "\\" + name))
            {
                bool isCanCopy = false;
                while (!isCanCopy)
                {
                    try
                    {
                        File.Copy(fullpath, destFolder + "\\" + name, true);
                        isCanCopy = true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(1000);
                    }
                }
                return new ProgressInfo(MessageType.COPY, name);
            }
            else
            {
                return new ProgressInfo(MessageType.EXIST, name);
            }
        }
        /// <summary>
        /// 复制完成回调函数，执行上报复制文件信息，计算复制进度
        /// </summary>
        /// <param name="iasync"></param>
        private void CopyFileCallBack(IAsyncResult iasync)
        {
            CopyFileDelegate copyAction = (CopyFileDelegate)iasync.AsyncState;
            ProgressInfo progressInfo = copyAction.EndInvoke(iasync);
            if (ReportCopyProgress != null)
            {
                ReportCopyProgress(progressInfo);
            }
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
