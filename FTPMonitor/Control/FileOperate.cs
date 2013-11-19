using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LogLib;
using FTPMonitor.DB;
using FTPMonitor.Forms;

namespace FTPMonitor
{
    class FileOperate
    {
        static object obj = new object();
        private readonly string recordName = "\\record.txt";
        private string recordPath;
        private string destFolder;

        public FileOperate(string destFolder)
        {
            this.destFolder = destFolder;
            recordPath = destFolder + recordName;
        }

        #region Method
        /// <summary>
        /// 外部调用查询复制
        /// </summary>
        /// <param name="south"></param>
        /// <param name="north"></param>
        /// <param name="west"></param>
        /// <param name="east"></param>
        /// <param name="phototime"></param>
        /// <param name="createtime"></param>
        public void ExecuteCopy(string south, string north, string west, string east, string phototime, string createtime)
        {
            string locationAppend = CC.CommbineLocationCriteria(south, north, west, east);
            string timeAppend = CC.CombineTimeCriteria(phototime, createtime);
            string existAppend = CC.CombineIsExistedCriteria(1);
            string append = new StringBuilder(locationAppend).Append(timeAppend).Append(existAppend).ToString();
            
            StringBuilder sqlcount = new StringBuilder("select count(*) from datainfo where 1=1 ");
            sqlcount.Append(append);
            int count = Convert.ToInt32(DataBaseControl.RunSqlForScalar(sqlcount.ToString()));
            DialogResult dr = MessageBox.Show("共找到【" + count + "】条数据，是否复制到输出路径中？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr != DialogResult.OK || count == 0)
            {
                return;
            }

            CopyProgress copyProgress = new CopyProgress(destFolder, append);
            ProgressForm progressForm = new ProgressForm(copyProgress, count);
            progressForm.ShowDialog(FormMain.Current);

            //if (!Directory.Exists(destFolder))
            //{
            //    Directory.CreateDirectory(destFolder);
            //}
            //LogManager.AddMessage(MessageType.INFO, "---------------------------");
            //FormMain.Current.UpdateFindCount(count);
            //WriteRecord(DateTime.Now.ToString() + "总计:" + count.ToString() + "条数据，详细清单：", recordPath);
            //StringBuilder sql = new StringBuilder("select * from datainfo where 1=1 ");
            //sql.Append(append);
            //SqlDataReader reader = DataBaseControl.RunSqlForDataReader(sql.ToString());
            //while (reader.Read())
            //{
            //    string name = reader["name"].ToString();
            //    string fullpath = reader["fullpath"].ToString();
            //    if (!File.Exists(fullpath))
            //    {
            //        continue;
            //    }
            //    ThreadPool.QueueUserWorkItem(new WaitCallback(CopyFileHandle), name + "@" + fullpath);
            //}
            //reader.Close();
            //DataBaseControl.CloseConnection();
        }
        #endregion

        #region 操作文件
        /// <summary>
        /// 将数据从FTP监控目录复制到目标目录
        /// </summary>
        /// <param name="datainfo"></param>
        private void CopyFileHandle(object obj)
        {
            string str = obj as string;
            string[] strs = str.Split('@');
            string name = strs[0];
            string fullpath = strs[1];

            if (!File.Exists(destFolder + "\\" + name))
            {
                CopyFile(fullpath, name);
            }
            else
            {
                WriteRecord(name + " 已经存在", recordPath);
                FormMain.Current.UpdateExistCount();
                LogManager.AddMessage(MessageType.EXIST, name);
            }
        }
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="fullpath"></param>
        /// <param name="name"></param>
        private void CopyFile(string fullpath, string name)
        {
            bool isCanCopy = false;
            while (!isCanCopy)
            {
                try
                {
                    File.Copy(fullpath, destFolder + "\\" + name, true);
                    isCanCopy = true;
                    WriteRecord(name, recordPath);
                    LogManager.AddMessage(MessageType.COPY, name);
                    FormMain.Current.UpdateCopyCount();
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
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
