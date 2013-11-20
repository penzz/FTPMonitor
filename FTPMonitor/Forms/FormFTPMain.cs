using FTPMonitor.Control;
using FTPMonitor.Data;
using FTPMonitor.DB;
using FTPMonitor.Forms;
using FTPMonitor.Model;
using LogLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTPMonitor
{
    public partial class FormFTPMain : Form
    {
        #region Property
        public static FormFTPMain Current;//当前主窗体实例
        QueryParameter queryPara;
        DataTable datatable;//保存每页显示的数据
        string criteria;//检索时构成的条件
        #endregion

        #region 系统
        public FormFTPMain()
        {
            InitializeComponent();
            InitFormStyle();
            RegisterEvent();
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        void InitFormStyle()
        {
            Current = this;
            this.WindowState = FormWindowState.Maximized;
            this.dtCreateTime.Checked = false;
            this.dtPhotoTime.Checked = false;
            this.cbDeleted.Checked = true;

            //设置隔行背景色
            this.dgvResult.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvResult.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            //设置不显示数据库中未绑定的列
            this.dgvResult.AutoGenerateColumns = false;
            this.pagerControl.RowsPerPage = 30;
        }
        /// <summary>
        /// 注册控件事件
        /// </summary>
        void RegisterEvent()
        {
            this.btnCopy.Click += btnCopy_Click;//复制
            this.btnQuery.Click += btnQuery_Click;//查询
            this.btnToday.Click += btnToday_Click;//今天数据
            this.btnExport.Click += btnExport_Click;//导出

            this.pagerControl.GetPageEvent += pagerControl_GetPageEvent;//分页控件
        }
        #endregion

        #region 复制
        void btnCopy_Click(object sender, EventArgs e)
        {
            if (this.cbDeleted.Checked)
            {
                MessageBox.Show("复制数据不能包括已删除数据！请取消已删除数据重新检索后操作。");
                return;
            }
            string destFolder = null;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                destFolder = fbd.SelectedPath;
            }
            int count = this.pagerControl.RecordCount;
            DialogResult dr = MessageBox.Show("是否将检索到的【" + count + "】条数据复制到目录：" + destFolder + "中？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr != DialogResult.OK || string.IsNullOrEmpty(destFolder) || count == 0)
            {
                return;
            }
            CopyProgress copyProgress = new CopyProgress(destFolder, criteria);
            ProgressForm progressForm = new ProgressForm(copyProgress, count);
            progressForm.ShowDialog(FormFTPMain.Current);
        }
        #endregion

        #region 分页
        void pagerControl_GetPageEvent()
        {
            QueryAndBinding(criteria);
        }
        private void QueryAndBinding(string criteria)
        {
            //获得总记录条数
            pagerControl.RecordCount = DataHelper.GetCount(criteria);
            //获得当前页
            int cur = pagerControl.CurrentPage;
            //获得每页显示的记录数
            int rows = pagerControl.RowsPerPage;
            //计算显示记录的开始值
            int start = rows;
            //计算显示记录的结束值
            int end = (cur - 1) * rows;
            //获得从开始值到结束值的记录
            datatable = DataHelper.GetDataTable(start, end, criteria);
            //将得到的记录绑定到DataGridView显示给用户
            this.dgvResult.DataSource = datatable.DefaultView;
        }
        #endregion

        #region 今天数据
        /// <summary>
        /// 今天到达的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnToday_Click(object sender, EventArgs e)
        {
            queryPara = new QueryParameter();
            queryPara.isToday = true;
            queryPara.createTime = DateTime.Now.ToString();
            queryPara.hasDelete = cbDeleted.Checked;
            criteria = DataHelper.GetCriteria(queryPara);
            QueryAndBinding(criteria);
        }
        #endregion

        #region 查询

        /// <summary>
        /// 初始化系统变量并验证其正确性
        /// </summary>
        /// <returns></returns>
        bool VerifyVariable()
        {
            queryPara = new QueryParameter();
            if (!double.TryParse(this.tbEast.Text, out queryPara.eastLon))
            {
                MessageBox.Show("经度范围结束值有错误，请重新输入！");
                return false;
            }
            if (!double.TryParse(this.tbWest.Text, out queryPara.westLon))
            {
                MessageBox.Show("经度范围开始值有错误，请重新输入！");
                return false;
            }
            if (!double.TryParse(this.tbSouth.Text, out queryPara.southLat))
            {
                MessageBox.Show("纬度范围开始值有错误，请重新输入！");
                return false;
            }
            if (!double.TryParse(this.tbNorth.Text, out queryPara.northLat))
            {
                MessageBox.Show("纬度范围结束值输入有错误，请重新输入！");
                return false;
            }
            if (this.dtPhotoTime.Checked)
            {
                DateTime dt = DateTime.Parse(this.dtPhotoTime.Text);
                queryPara.photoTime = String.Format("{0:yyyyMMdd}", dt);
            }
            if (this.dtCreateTime.Checked)
            {
                queryPara.createTime = this.dtCreateTime.Text;
            }
            queryPara.hasDelete = cbDeleted.Checked;
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnQuery_Click(object sender, EventArgs e)
        {
            if (!VerifyVariable())
            {
                return;
            }
            criteria = DataHelper.GetCriteria(queryPara);
            QueryAndBinding(criteria);
        }

        #endregion

        #region 导出
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
                ExportDataInfo(sfd.FileName);
            }
        }
        /// <summary>
        /// 导出到文件
        /// </summary>
        private void ExportDataInfo(string exportFilePath)
        {
            if (this.pagerControl.RecordCount == 0 || this.datatable.Rows.Count <= 0)
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
            DataTable DT = DataHelper.GetDataTable(criteria);
            ExcelOperate.ExportExcel(DT, exportFilePath);
            this.Cursor = Cursors.Default;
            MessageBox.Show("导出完成！");
        }
        #endregion

        #region method
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
    }
}
