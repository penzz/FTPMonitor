namespace FTPMonitor
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.FSWatcher = new System.IO.FileSystemWatcher();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnScanDest = new System.Windows.Forms.Button();
            this.tbDestFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMonitorFolder = new System.Windows.Forms.TextBox();
            this.btnScanMonitor = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.notifyIconServer = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemShowForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCloseForm = new System.Windows.Forms.ToolStripMenuItem();
            this.tbWest = new System.Windows.Forms.NumericUpDown();
            this.tbEast = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtPhotoTime = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtCreateTime = new System.Windows.Forms.DateTimePicker();
            this.tbNorth = new System.Windows.Forms.NumericUpDown();
            this.tbSouth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tooStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.FSWatcher)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbWest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbEast)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNorth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSouth)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FSWatcher
            // 
            this.FSWatcher.EnableRaisingEvents = true;
            this.FSWatcher.NotifyFilter = ((System.IO.NotifyFilters)((((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName) 
            | System.IO.NotifyFilters.LastWrite) 
            | System.IO.NotifyFilters.LastAccess)));
            this.FSWatcher.SynchronizingObject = this;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(409, 83);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(87, 23);
            this.btnFind.TabIndex = 3;
            this.btnFind.Text = "查找复制";
            this.btnFind.UseVisualStyleBackColor = true;
            // 
            // btnScanDest
            // 
            this.btnScanDest.Location = new System.Drawing.Point(338, 83);
            this.btnScanDest.Name = "btnScanDest";
            this.btnScanDest.Size = new System.Drawing.Size(33, 23);
            this.btnScanDest.TabIndex = 1;
            this.btnScanDest.Text = "...";
            this.btnScanDest.UseVisualStyleBackColor = true;
            // 
            // tbDestFolder
            // 
            this.tbDestFolder.Location = new System.Drawing.Point(85, 85);
            this.tbDestFolder.Name = "tbDestFolder";
            this.tbDestFolder.ReadOnly = true;
            this.tbDestFolder.Size = new System.Drawing.Size(244, 21);
            this.tbDestFolder.TabIndex = 32;
            this.tbDestFolder.Text = "E:\\test";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 31;
            this.label6.Text = "输出路径：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "查询路径：";
            // 
            // tbMonitorFolder
            // 
            this.tbMonitorFolder.Location = new System.Drawing.Point(83, 20);
            this.tbMonitorFolder.Name = "tbMonitorFolder";
            this.tbMonitorFolder.ReadOnly = true;
            this.tbMonitorFolder.Size = new System.Drawing.Size(246, 21);
            this.tbMonitorFolder.TabIndex = 24;
            this.tbMonitorFolder.Text = "D:\\ftpMain";
            // 
            // btnScanMonitor
            // 
            this.btnScanMonitor.Location = new System.Drawing.Point(338, 20);
            this.btnScanMonitor.Name = "btnScanMonitor";
            this.btnScanMonitor.Size = new System.Drawing.Size(33, 23);
            this.btnScanMonitor.TabIndex = 0;
            this.btnScanMonitor.Text = "...";
            this.btnScanMonitor.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(380, 20);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(71, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(304, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 68;
            this.label9.Text = "到达时间";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(460, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 23);
            this.btnQuery.TabIndex = 33;
            this.btnQuery.Text = "统计";
            this.btnQuery.UseVisualStyleBackColor = true;
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
            this.toolStripMenuItemCloseForm});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
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
            // tbWest
            // 
            this.tbWest.DecimalPlaces = 2;
            this.tbWest.Location = new System.Drawing.Point(85, 22);
            this.tbWest.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.tbWest.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.tbWest.Name = "tbWest";
            this.tbWest.Size = new System.Drawing.Size(73, 21);
            this.tbWest.TabIndex = 69;
            this.tbWest.Value = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            // 
            // tbEast
            // 
            this.tbEast.DecimalPlaces = 2;
            this.tbEast.Location = new System.Drawing.Point(193, 22);
            this.tbEast.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.tbEast.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.tbEast.Name = "tbEast";
            this.tbEast.Size = new System.Drawing.Size(73, 21);
            this.tbEast.TabIndex = 70;
            this.tbEast.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnQuery);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.btnScanMonitor);
            this.groupBox3.Controls.Add(this.tbMonitorFolder);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(556, 56);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "系统启动";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtPhotoTime);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.dtCreateTime);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.tbNorth);
            this.groupBox4.Controls.Add(this.tbSouth);
            this.groupBox4.Controls.Add(this.tbEast);
            this.groupBox4.Controls.Add(this.tbDestFolder);
            this.groupBox4.Controls.Add(this.tbWest);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.btnFind);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btnScanDest);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 56);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(556, 117);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "查找复制";
            // 
            // dtPhotoTime
            // 
            this.dtPhotoTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPhotoTime.Location = new System.Drawing.Point(378, 20);
            this.dtPhotoTime.Name = "dtPhotoTime";
            this.dtPhotoTime.ShowCheckBox = true;
            this.dtPhotoTime.Size = new System.Drawing.Size(145, 21);
            this.dtPhotoTime.TabIndex = 79;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(304, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 78;
            this.label7.Text = "拍摄时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 77;
            this.label4.Text = "到";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 76;
            this.label3.Text = "纬度范围从";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 75;
            this.label2.Text = "到";
            // 
            // dtCreateTime
            // 
            this.dtCreateTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCreateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtCreateTime.Location = new System.Drawing.Point(377, 50);
            this.dtCreateTime.Name = "dtCreateTime";
            this.dtCreateTime.ShowCheckBox = true;
            this.dtCreateTime.Size = new System.Drawing.Size(147, 21);
            this.dtCreateTime.TabIndex = 74;
            // 
            // tbNorth
            // 
            this.tbNorth.DecimalPlaces = 2;
            this.tbNorth.Location = new System.Drawing.Point(193, 52);
            this.tbNorth.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.tbNorth.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.tbNorth.Name = "tbNorth";
            this.tbNorth.Size = new System.Drawing.Size(73, 21);
            this.tbNorth.TabIndex = 72;
            this.tbNorth.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // tbSouth
            // 
            this.tbSouth.DecimalPlaces = 2;
            this.tbSouth.Location = new System.Drawing.Point(85, 52);
            this.tbSouth.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.tbSouth.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.tbSouth.Name = "tbSouth";
            this.tbSouth.Size = new System.Drawing.Size(73, 21);
            this.tbSouth.TabIndex = 71;
            this.tbSouth.Value = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "经度范围从";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tooStatusLabelInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 177);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(556, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tooStatusLabelInfo
            // 
            this.tooStatusLabelInfo.Name = "tooStatusLabelInfo";
            this.tooStatusLabelInfo.Size = new System.Drawing.Size(53, 17);
            this.tooStatusLabelInfo.Text = "系统信息";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 199);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "FTP目录监控小程序";
            ((System.ComponentModel.ISupportInitialize)(this.FSWatcher)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbWest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbEast)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNorth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSouth)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.FileSystemWatcher FSWatcher;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnScanDest;
        private System.Windows.Forms.TextBox tbDestFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbMonitorFolder;
        private System.Windows.Forms.Button btnScanMonitor;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NotifyIcon notifyIconServer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowForm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloseForm;
        private System.Windows.Forms.NumericUpDown tbWest;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown tbEast;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtCreateTime;
        private System.Windows.Forms.NumericUpDown tbNorth;
        private System.Windows.Forms.NumericUpDown tbSouth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtPhotoTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tooStatusLabelInfo;

    }
}

