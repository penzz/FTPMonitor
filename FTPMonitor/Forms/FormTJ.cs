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

namespace FTPMonitor
{
    public partial class FormTJ : Form
    {
        DataTable datatable;//保存每页显示的数据
        string exportFilePath;//导出路径
        string criteria;//检索时构成的条件
        public FormTJ()
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
            this.cbDeleted.Checked = true;
            this.pagerControl1.RowsPerPage = 30;
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent()
        {
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.btnToday.Click += new EventHandler(btnToday_Click);
            this.pagerControl1.GetPageEvent += new PagerLib.PagerControl.GetPageHandle(pagerControl1_GetPageEvent);
            this.btnExport.Click += new EventHandler(btnExport_Click);
        }

        void pagerControl1_GetPageEvent()
        {
            QueryAndBinding(criteria);
        }
        private void QueryAndBinding(string criteria)
        {
            //获得总记录条数
            string sqlcount = String.Format("select count(*) from DataInfo where  1=1 {0}", criteria);
            pagerControl1.RecordCount = Convert.ToInt32(DataBaseControl.RunSqlForScalar(sqlcount));
            //获得当前页
            int cur = pagerControl1.CurrentPage;
            //获得每页显示的记录数
            int rows = pagerControl1.RowsPerPage;
            //计算显示记录的开始值
            int start = rows;
            //计算显示记录的结束值
            int end = (cur - 1) * rows;
            //获得从开始值到结束值的记录
            string sql = string.Format("select top ({0}) * from DataInfo where 1=1 {1} and (id NOT IN (" +
            "select top ({2}) id from DataInfo where 1=1 {3} order by id)) order by id", start, criteria, end, criteria);
            datatable = DataBaseControl.RunSqlForDataTable(sql);
            //将得到的记录绑定到DataGridView显示给用户
            this.dgvResult.DataSource = datatable.DefaultView;
        }

        /// <summary>
        /// 今天到达的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnToday_Click(object sender, EventArgs e)
        {
            criteria = CC.CombineTimeCriteria(null, DateTime.Now.ToString());
            if (!cbDeleted.Checked)
            {
                criteria += CC.CombineIsExistedCriteria(1);
            }
            QueryAndBinding(criteria);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnQuery_Click(object sender, EventArgs e)
        {
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
            criteria = CC.CombineTimeCriteria(phototime, createtime);
            if (!cbDeleted.Checked)
            {
                criteria += CC.CombineIsExistedCriteria(1);
            }
            QueryAndBinding(criteria);
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 导出到文件
        /// </summary>
        private void ExportDataInfo()
        {
            if (this.pagerControl1.RecordCount == 0 || this.datatable.Rows.Count <= 0)
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
            string sql = String.Format("select id,name,satellite,sensor,phototime,createtime,centerlat,centerlon,fullpath,isexisted from datainfo where 1=1 {0}", criteria);
            DataTable DT = DataBaseControl.RunSqlForDataTable(sql);
            ExcelOperate.ExportExcel(DT, exportFilePath);
            this.Cursor = Cursors.Default;
            MessageBox.Show("导出完成！");
        }
    }
}
