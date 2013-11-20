namespace FTPMonitor
{
    partial class FormStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
            this.label5 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnScanMonitor = new System.Windows.Forms.Button();
            this.tbMonitorFolder = new System.Windows.Forms.TextBox();
            this.notifyIconServer = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemShowForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCloseForm = new System.Windows.Forms.ToolStripMenuItem();
            this.FSWatcher = new System.IO.FileSystemWatcher();
            this.btnOperate = new System.Windows.Forms.Button();
            this.rtbMessage = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ToolStripMenuItemQueryData = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FSWatcher)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "监控路径：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(407, 23);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 23);
            this.btnStart.TabIndex = 31;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnScanMonitor
            // 
            this.btnScanMonitor.Location = new System.Drawing.Point(341, 24);
            this.btnScanMonitor.Name = "btnScanMonitor";
            this.btnScanMonitor.Size = new System.Drawing.Size(49, 23);
            this.btnScanMonitor.TabIndex = 30;
            this.btnScanMonitor.Text = "...";
            this.btnScanMonitor.UseVisualStyleBackColor = true;
            // 
            // tbMonitorFolder
            // 
            this.tbMonitorFolder.Location = new System.Drawing.Point(86, 25);
            this.tbMonitorFolder.Name = "tbMonitorFolder";
            this.tbMonitorFolder.ReadOnly = true;
            this.tbMonitorFolder.Size = new System.Drawing.Size(246, 21);
            this.tbMonitorFolder.TabIndex = 32;
            this.tbMonitorFolder.Text = "D:\\ftpMain";
            // 
            // notifyIconServer
            // 
            this.notifyIconServer.BalloonTipText = "FTP目录监控小程序";
            this.notifyIconServer.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIconServer.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconServer.Icon")));
            this.notifyIconServer.Text = "FTPMonitor";
            this.notifyIconServer.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShowForm,
            this.ToolStripMenuItemQueryData,
            this.toolStripMenuItemCloseForm});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 70);
            // 
            // toolStripMenuItemShowForm
            // 
            this.toolStripMenuItemShowForm.Name = "toolStripMenuItemShowForm";
            this.toolStripMenuItemShowForm.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemShowForm.Text = "显示窗体";
            // 
            // toolStripMenuItemCloseForm
            // 
            this.toolStripMenuItemCloseForm.Name = "toolStripMenuItemCloseForm";
            this.toolStripMenuItemCloseForm.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemCloseForm.Text = "关闭窗体";
            // 
            // FSWatcher
            // 
            this.FSWatcher.EnableRaisingEvents = true;
            this.FSWatcher.NotifyFilter = ((System.IO.NotifyFilters)((((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName) 
            | System.IO.NotifyFilters.LastWrite) 
            | System.IO.NotifyFilters.LastAccess)));
            this.FSWatcher.SynchronizingObject = this;
            // 
            // btnOperate
            // 
            this.btnOperate.Location = new System.Drawing.Point(498, 24);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(75, 23);
            this.btnOperate.TabIndex = 37;
            this.btnOperate.Text = "检索";
            this.btnOperate.UseVisualStyleBackColor = true;
            // 
            // rtbMessage
            // 
            this.rtbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMessage.Location = new System.Drawing.Point(5, 19);
            this.rtbMessage.Multiline = true;
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.ReadOnly = true;
            this.rtbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.rtbMessage.Size = new System.Drawing.Size(569, 213);
            this.rtbMessage.TabIndex = 38;
            this.rtbMessage.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbMessage);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(579, 237);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统信息";
            // 
            // ToolStripMenuItemQueryData
            // 
            this.ToolStripMenuItemQueryData.Name = "ToolStripMenuItemQueryData";
            this.ToolStripMenuItemQueryData.Size = new System.Drawing.Size(118, 22);
            this.ToolStripMenuItemQueryData.Text = "检索数据";
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 301);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOperate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnScanMonitor);
            this.Controls.Add(this.tbMonitorFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormStart";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "系统启动";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FSWatcher)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnScanMonitor;
        private System.Windows.Forms.TextBox tbMonitorFolder;
        private System.Windows.Forms.NotifyIcon notifyIconServer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowForm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloseForm;
        private System.IO.FileSystemWatcher FSWatcher;
        private System.Windows.Forms.Button btnOperate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox rtbMessage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemQueryData;
    }
}