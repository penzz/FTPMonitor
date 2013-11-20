namespace FTPMonitor
{
    partial class FormFTPMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFTPMain));
            this.groupBoxTiaojian = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.cbDeleted = new System.Windows.Forms.CheckBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dtPhotoTime = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtCreateTime = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.tbNorth = new System.Windows.Forms.NumericUpDown();
            this.tbSouth = new System.Windows.Forms.NumericUpDown();
            this.tbEast = new System.Windows.Forms.NumericUpDown();
            this.tbWest = new System.Windows.Forms.NumericUpDown();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerControl = new PagerLib.PagerControl();
            this.groupBoxTiaojian.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNorth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSouth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbEast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbWest)).BeginInit();
            this.groupBoxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTiaojian
            // 
            this.groupBoxTiaojian.Controls.Add(this.btnExport);
            this.groupBoxTiaojian.Controls.Add(this.btnToday);
            this.groupBoxTiaojian.Controls.Add(this.cbDeleted);
            this.groupBoxTiaojian.Controls.Add(this.btnQuery);
            this.groupBoxTiaojian.Controls.Add(this.dtPhotoTime);
            this.groupBoxTiaojian.Controls.Add(this.label7);
            this.groupBoxTiaojian.Controls.Add(this.label4);
            this.groupBoxTiaojian.Controls.Add(this.label3);
            this.groupBoxTiaojian.Controls.Add(this.label2);
            this.groupBoxTiaojian.Controls.Add(this.dtCreateTime);
            this.groupBoxTiaojian.Controls.Add(this.label9);
            this.groupBoxTiaojian.Controls.Add(this.tbNorth);
            this.groupBoxTiaojian.Controls.Add(this.tbSouth);
            this.groupBoxTiaojian.Controls.Add(this.tbEast);
            this.groupBoxTiaojian.Controls.Add(this.tbWest);
            this.groupBoxTiaojian.Controls.Add(this.btnCopy);
            this.groupBoxTiaojian.Controls.Add(this.label1);
            this.groupBoxTiaojian.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTiaojian.Location = new System.Drawing.Point(5, 5);
            this.groupBoxTiaojian.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxTiaojian.Name = "groupBoxTiaojian";
            this.groupBoxTiaojian.Size = new System.Drawing.Size(812, 87);
            this.groupBoxTiaojian.TabIndex = 5;
            this.groupBoxTiaojian.TabStop = false;
            this.groupBoxTiaojian.Text = "条件设置";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(684, 52);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(97, 23);
            this.btnExport.TabIndex = 86;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(581, 21);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(95, 23);
            this.btnToday.TabIndex = 85;
            this.btnToday.Text = "今天到达数据";
            this.btnToday.UseVisualStyleBackColor = true;
            // 
            // cbDeleted
            // 
            this.cbDeleted.AutoSize = true;
            this.cbDeleted.Location = new System.Drawing.Point(480, 24);
            this.cbDeleted.Name = "cbDeleted";
            this.cbDeleted.Size = new System.Drawing.Size(84, 16);
            this.cbDeleted.TabIndex = 84;
            this.cbDeleted.Text = "已删除数据";
            this.cbDeleted.UseVisualStyleBackColor = true;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(581, 52);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(95, 23);
            this.btnQuery.TabIndex = 80;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // dtPhotoTime
            // 
            this.dtPhotoTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPhotoTime.Location = new System.Drawing.Point(321, 22);
            this.dtPhotoTime.Name = "dtPhotoTime";
            this.dtPhotoTime.ShowCheckBox = true;
            this.dtPhotoTime.Size = new System.Drawing.Size(145, 21);
            this.dtPhotoTime.TabIndex = 79;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(262, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 78;
            this.label7.Text = "拍摄时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 77;
            this.label4.Text = "—";
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
            this.label2.Location = new System.Drawing.Point(153, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 75;
            this.label2.Text = "—";
            // 
            // dtCreateTime
            // 
            this.dtCreateTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCreateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtCreateTime.Location = new System.Drawing.Point(320, 52);
            this.dtCreateTime.Name = "dtCreateTime";
            this.dtCreateTime.ShowCheckBox = true;
            this.dtCreateTime.Size = new System.Drawing.Size(147, 21);
            this.dtCreateTime.TabIndex = 74;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(262, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 68;
            this.label9.Text = "到达时间";
            // 
            // tbNorth
            // 
            this.tbNorth.DecimalPlaces = 2;
            this.tbNorth.Location = new System.Drawing.Point(171, 51);
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
            this.tbSouth.Location = new System.Drawing.Point(79, 51);
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
            // tbEast
            // 
            this.tbEast.DecimalPlaces = 2;
            this.tbEast.Location = new System.Drawing.Point(171, 21);
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
            // tbWest
            // 
            this.tbWest.DecimalPlaces = 2;
            this.tbWest.Location = new System.Drawing.Point(79, 21);
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
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(684, 21);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(95, 23);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
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
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.dgvResult);
            this.groupBoxResult.Controls.Add(this.pagerControl);
            this.groupBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResult.Location = new System.Drawing.Point(5, 92);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxResult.Size = new System.Drawing.Size(812, 397);
            this.groupBoxResult.TabIndex = 6;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "结果";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(5, 19);
            this.dgvResult.MultiSelect = false;
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(802, 349);
            this.dgvResult.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "id";
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "name";
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "satellite";
            this.Column3.HeaderText = "卫星";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "sensor";
            this.Column4.HeaderText = "传感器";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "phototime";
            this.Column5.HeaderText = "拍摄时间";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "createtime";
            this.Column6.HeaderText = "创建时间";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "centerlat";
            this.Column7.HeaderText = "中心纬度";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "centerlon";
            this.Column8.HeaderText = "中心经度";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "fullpath";
            this.Column9.HeaderText = "存放位置";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "isexisted";
            this.Column10.HeaderText = "是否存在";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // pagerControl
            // 
            this.pagerControl.BackColor = System.Drawing.SystemColors.Control;
            this.pagerControl.CurrentPage = 1;
            this.pagerControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl.Location = new System.Drawing.Point(5, 368);
            this.pagerControl.Name = "pagerControl";
            this.pagerControl.RecordCount = 0;
            this.pagerControl.RowsPerPage = 5;
            this.pagerControl.Size = new System.Drawing.Size(802, 24);
            this.pagerControl.TabIndex = 2;
            this.pagerControl.TotalPage = 0;
            // 
            // FormFTPMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 494);
            this.Controls.Add(this.groupBoxResult);
            this.Controls.Add(this.groupBoxTiaojian);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFTPMain";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "FTP监控小程序";
            this.groupBoxTiaojian.ResumeLayout(false);
            this.groupBoxTiaojian.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNorth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSouth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbEast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbWest)).EndInit();
            this.groupBoxResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTiaojian;
        private System.Windows.Forms.DateTimePicker dtPhotoTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtCreateTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown tbNorth;
        private System.Windows.Forms.NumericUpDown tbSouth;
        private System.Windows.Forms.NumericUpDown tbEast;
        private System.Windows.Forms.NumericUpDown tbWest;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.CheckBox cbDeleted;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnToday;
        private PagerLib.PagerControl pagerControl;
    }
}