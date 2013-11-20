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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
            this.label5 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnScanMonitor = new System.Windows.Forms.Button();
            this.tbMonitorFolder = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tooStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "监控路径：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(86, 72);
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
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(247, 72);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 23);
            this.btnExit.TabIndex = 34;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tooStatusLabelInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 107);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(410, 22);
            this.statusStrip1.TabIndex = 35;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tooStatusLabelInfo
            // 
            this.tooStatusLabelInfo.Name = "tooStatusLabelInfo";
            this.tooStatusLabelInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 129);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnScanMonitor);
            this.Controls.Add(this.tbMonitorFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStart";
            this.Text = "系统启动";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnScanMonitor;
        private System.Windows.Forms.TextBox tbMonitorFolder;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tooStatusLabelInfo;
    }
}