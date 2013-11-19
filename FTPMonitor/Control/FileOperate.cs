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
        }
        #endregion
    }
}
