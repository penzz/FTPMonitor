using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using System.Runtime.InteropServices;
namespace FTPMonitor
{
    /// <summary>
    /// 主窗体的分布类，将一些不重要的辅助操作如访问控件、窗体隐藏、选择文件夹等实现放在此处
    /// 更加高效的关注主要逻辑
    /// </summary>
    partial class FormMain : Form
    {

        object obj = new object();//多线程锁对象
        FolderBrowserDialog fbd;//文件夹浏览对话框


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
            this.tooStatusLabelInfo.Text = obj;
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

        #region Events
        /// <summary>
        /// 浏览监控目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanMonitor_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.tbMonitorFolder.Text = fbd.SelectedPath;
            }
        }

        /// <summary>
        /// 浏览目标目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanDest_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.tbDestFolder.Text = fbd.SelectedPath;
                destFolder = this.tbDestFolder.Text;
            }
        }
        #endregion
    }
}
