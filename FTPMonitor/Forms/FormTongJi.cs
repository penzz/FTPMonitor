using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FTPMonitor.DB;

namespace FTPMonitor.Forms
{
    public partial class FormTongJi : Form
    {
        DataTable datatable;
        string exportFilePath;
        public FormTongJi()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            InitProperty();
            RegisterEvent();
        }
        /// <summary>
        /// 初始化属性
        /// </summary>
        void InitProperty()
        {
            //设置隔行背景色
            this.dgvResult.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvResult.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            //设置不显示数据库中未绑定的列
            this.dgvResult.AutoGenerateColumns = false;
            this.dtCreateTime.Checked = false;
            this.dtPhotoTime.Checked = false;
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent()
        {
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.btnToday.Click += new EventHandler(btnToday_Click);
            this.btnExport.Click += new EventHandler(btnExport_Click);
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.xlsx|*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.exportFilePath = sfd.FileName;
                ExportDataInfo();
            }
        }
        /// <summary>
        /// 今天到达的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnToday_Click(object sender, EventArgs e)
        {
            string append = CC.CombineTimeCriteria(null, DateTime.Now.ToString());
            string sql = String.Format("select * from datainfo where 1=1 {0}", append);
            string sqlcount = String.Format("select count(*) from datainfo where  1=1 {0}", append);
            QueryAndBinding(sql, sqlcount);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnQuery_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder("select * from datainfo where 1=1 ");
            StringBuilder sqlcount = new StringBuilder("select count(*) from datainfo where 1=1 ");
            string phototime = null, createtime = null;
            if (this.dtPhotoTime.Checked)
            {
                DateTime dt = DateTime.Parse(this.dtPhotoTime.Text);
                phototime = String.Format("{0:yyyyMMdd}", dt);
            }
            if (this.dtCreateTime.Checked)
            {
                createtime = this.dtCreateTime.Text;
            }
            string append = CC.CombineTimeCriteria(phototime, createtime);
            sql.Append(append);
            sqlcount.Append(append);
            QueryAndBinding(sql.ToString(), sqlcount.ToString());
        }

        /// <summary>
        /// 查询并显示数据
        /// </summary>
        /// <param name="sql"></param>
        private void QueryAndBinding(string sql, string sqlcount)
        {
            int count = Convert.ToInt32(DataBaseControl.RunSqlForScalar(sqlcount));
            this.labelCount.Text = count.ToString();
            datatable = DataBaseControl.RunSqlForDataTable(sql);
            this.bSource.DataSource = datatable;
            this.bNavigator.BindingSource = this.bSource;
            this.dgvResult.DataSource = this.bSource;
        }
        /// <summary>
        /// 导出到文件
        /// </summary>
        private void ExportDataInfo()
        {
            if (this.labelCount.Text == "0" || this.datatable.Rows.Count <= 0)
            {
                MessageBox.Show("没有可以导出的数据信息！");
                return;
            }
            if (string.IsNullOrEmpty(exportFilePath))
            {
                MessageBox.Show("请点击【导出到文件】按钮选择文件路径！");
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            if (File.Exists(exportFilePath))
            {
                File.Delete(exportFilePath);
            }
            ExcelOperate.ExportExcel(datatable, exportFilePath);
            this.Cursor = Cursors.Default;
            MessageBox.Show("导出完成！");
        }
    }
}
