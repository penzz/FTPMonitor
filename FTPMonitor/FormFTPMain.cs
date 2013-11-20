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
        private static bool isRun;

        public static FormFTPMain Current;//当前主窗体实例

        List<string> CurrentCopyFile;//保持当前正在推送的数据对象
        QueryParameter queryPara;
        DataTable datatable;//保存每页显示的数据
        string criteria;//检索时构成的条件
        #endregion

        #region 系统
        public FormFTPMain()
        {
            InitializeComponent();
            InitFormStyle();
            InitProperty();
            RegisterEvent();
        }

        void InitProperty()
        {
            Current = this;
            isRun = true;
            CurrentCopyFile = new List<string>();
            this.FSWatcher.Path = CC.monitorFolder;
            this.FSWatcher.IncludeSubdirectories = true;
            this.FSWatcher.Filter = "*.tar.gz";
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        void InitFormStyle()
        {
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
            this.FSWatcher.Changed += FSWatcher_Changed;//监控新数据到达
            this.FSWatcher.Deleted += FSWatcher_Deleted;//监控数据删除

            this.btnCopy.Click += btnCopy_Click;//复制
            this.btnQuery.Click += btnQuery_Click;//查询
            this.btnToday.Click += btnToday_Click;//今天数据
            this.btnExport.Click += btnExport_Click;//导出

            this.pagerControl.GetPageEvent += pagerControl_GetPageEvent;//分页控件
            LogManager.ShowLogEvent += new Action<string>(LogManager_ShowLogEvent);//注册日志信息事件

            #region 窗体显示隐藏事件注册
            this.toolStripMenuItemCloseForm.Click += new EventHandler(toolStripMenuItemCloseForm_Click);
            this.toolStripMenuItemShowForm.Click += new EventHandler(toolStripMenuItemShowForm_Click);
            this.notifyIconServer.MouseDoubleClick += new MouseEventHandler(notifyIconServer_MouseDoubleClick);
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
            #endregion
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

        #region 监控
        /// <summary>
        /// 监控文件目录数据改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FSWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            //FullPath —E:\test\123456\GF1_WFV1_E117.6_N33.0_20130724_L1A0000057813.tar.gz
            string fullpath = e.FullPath;
            //如果是增加的是目录则返回
            if ((File.GetAttributes(fullpath) & FileAttributes.Directory) == FileAttributes.Directory)
                return;
            // FileSystemWather在数据刚开始复制和复制结束时都会触发Changed事件
            // 使用List持有对象，数据刚开始复制则添加持有该对象，复制结束后移除
            bool isCopyOk = CurrentCopyFile.Contains(fullpath);
            if (isCopyOk)//是否已经在FTP上准备好的数据
            {
                CurrentCopyFile.Remove(fullpath);
                DataInfo datainfo = DataFactory.CreateDataInfo(e.Name, fullpath);
                if (datainfo == null)
                {
                    return;
                }
                DataOperate.GetInstance().AddNewData(datainfo);
            }
            else
            {
                CurrentCopyFile.Add(fullpath);
            }
        }
        /// <summary>
        /// 检测到文件删除，移除记录，防止检索出现数据不同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FSWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            DataOperate.GetInstance().RemoveData(e.FullPath);//触发数据删除事件
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
        /// <summary>
        /// 将日志显示在状态栏
        /// </summary>
        /// <param name="obj"></param>
        void LogManager_ShowLogEvent(string obj)
        {

        }
        #endregion

        #region 窗体关闭显示与隐藏
        private bool isExit = false;
        private bool isHide = false;
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit == false && isRun)
            {
                HideForm();
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 托盘图标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIconServer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isHide)
                ShowForm();
            else
                HideForm();
        }
        /// <summary>
        /// 显示主窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemShowForm_Click(object sender, EventArgs e)
        {
            if (isHide)
            {
                ShowForm();
            }
        }
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCloseForm_Click(object sender, EventArgs e)
        {
            CloseMainForm();
        }
        /// <summary>
        /// 关闭主窗体
        /// </summary>
        private void CloseMainForm()
        {
            if (!isRun)
            {
                isExit = true;
                this.Close();
                Application.Exit();
            }
            else
            {
                DialogResult result = MessageBox.Show(this, "严重警告：\r\n\r\n如果关闭该窗体,系统纪录的信息都将丢失！\r\n\r\n是否仍要关闭?", "严重警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)//如果选择是,则直接关闭,
                {
                    return;
                }
                else
                {
                    isExit = true;
                    this.Close();
                    Application.Exit();
                }
            }
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        private void ShowForm()
        {
            isHide = false;
            this.Show();
            this.Activate();
        }
        /// <summary>
        /// 隐藏窗体
        /// </summary>
        private void HideForm()
        {
            isHide = true;
            this.Hide();
        }
        #endregion
    }
}
