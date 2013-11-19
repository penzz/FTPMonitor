using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LogLib;

namespace FTPMonitor.Forms
{
    /// <summary>
    /// 显示复制进度
    /// </summary>
    public partial class ProgressForm : Form
    {
        private object obj = new object();
        private int count;
        private CopyProgress copyProgress;
        private StringBuilder stringBuilder = new StringBuilder();
        private readonly string recordName = "\\record.txt";
        private string recordPath;
        public ProgressForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="copyProgress"></param>
        /// <param name="count"></param>
        public ProgressForm(CopyProgress copyProgress, int count)
        {
            InitializeComponent();
            this.count = count;
            this.copyProgress = copyProgress;
            recordPath = this.copyProgress.destFolder + recordName;
            this.labelInfo.Text = "0%";
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = count;
            copyProgress.ReportCopyProgress += new Action<ProgressInfo>(copyProgress_ReportCopyProgress);
            this.Shown += new EventHandler(ProgressForm_Shown);
            this.FormClosing += new FormClosingEventHandler(ProgressForm_FormClosing);
        }

        void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.progressBar.Value != count)
            {
                MessageBox.Show("正在进行复制无法关闭窗口！");
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 窗体显示后开始复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProgressForm_Shown(object sender, EventArgs e)
        {
            copyProgress.StartCopy();
            this.groupBox1.Text = "正在复制...";
            WriteRecord(DateTime.Now.ToString() + "总计:" + count.ToString() + "条数据，详细清单：", recordPath);
        }
        /// <summary>
        /// 计算复制进度
        /// </summary>
        /// <param name="progressInfo"></param>
        void copyProgress_ReportCopyProgress(ProgressInfo progressInfo)
        {
            if (this.progressBar.InvokeRequired)
            {
                Action<ProgressInfo> action = new Action<ProgressInfo>(copyProgress_ReportCopyProgress);
                this.progressBar.Invoke(action, progressInfo);
            }
            else
            {
                lock (obj)
                {
                    try
                    {
                        stringBuilder.Clear();
                        stringBuilder.Append("[" + DateTime.Now.ToString() + "]");
                        stringBuilder.Append(" " + progressInfo.MT.ToString() + ": " + progressInfo.name);
                        stringBuilder.Append(Environment.NewLine);
                        this.textBox1.AppendText(stringBuilder.ToString());

                        string filename = progressInfo.name;
                        if (progressInfo.MT == MessageType.EXIST)
                        {
                            filename += " 已经存在";
                        }
                        WriteRecord(filename, recordPath);

                        this.progressBar.Value += 1;
                        double value = this.progressBar.Value * 1.0 / count * 100;
                        this.labelInfo.Text = string.Format("{0:.00}%", value);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 向文件中写记录
        /// </summary>
        /// <param name="record"></param>
        /// <param name="filePath"></param>
        void WriteRecord(string record, string filePath)
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
}
