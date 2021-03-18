namespace RoutingWinApp
{
    partial class CFDispatchTrackImportOrders
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
            this.dtRoutingDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdImportOrders = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.grpProcess = new System.Windows.Forms.GroupBox();
            this.JobProgressBar = new System.Windows.Forms.ProgressBar();
            this.txtProcessLog = new System.Windows.Forms.RichTextBox();
            this.cboRoutingWave = new System.Windows.Forms.ComboBox();
            this.chkNoRegularInvoices = new System.Windows.Forms.CheckBox();
            this.grpBoxFilter = new System.Windows.Forms.GroupBox();
            this.chkOnlyTransfers = new System.Windows.Forms.CheckBox();
            this.btnUpdateDrivers = new System.Windows.Forms.Button();
            this.bgwImportOrders = new System.ComponentModel.BackgroundWorker();
            this.bgwUpdateDTDrivers = new System.ComponentModel.BackgroundWorker();
            this.cmdReset = new System.Windows.Forms.Button();
            this.chkLockStatus = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboRoutingWaveSub = new System.Windows.Forms.ComboBox();
            this.grpProcess.SuspendLayout();
            this.grpBoxFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtRoutingDate
            // 
            this.dtRoutingDate.Location = new System.Drawing.Point(22, 28);
            this.dtRoutingDate.Name = "dtRoutingDate";
            this.dtRoutingDate.Size = new System.Drawing.Size(263, 20);
            this.dtRoutingDate.TabIndex = 0;
            this.dtRoutingDate.ValueChanged += new System.EventHandler(this.dtRoutingDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Routing Date:";
            // 
            // cmdImportOrders
            // 
            this.cmdImportOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdImportOrders.Location = new System.Drawing.Point(508, 245);
            this.cmdImportOrders.Name = "cmdImportOrders";
            this.cmdImportOrders.Size = new System.Drawing.Size(200, 63);
            this.cmdImportOrders.TabIndex = 3;
            this.cmdImportOrders.Text = "Import Orders from AS400";
            this.cmdImportOrders.UseVisualStyleBackColor = true;
            this.cmdImportOrders.Click += new System.EventHandler(this.cmdImportOrders_click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Distribution Center:";
            // 
            // grpProcess
            // 
            this.grpProcess.Controls.Add(this.JobProgressBar);
            this.grpProcess.Controls.Add(this.txtProcessLog);
            this.grpProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProcess.Location = new System.Drawing.Point(12, 306);
            this.grpProcess.Name = "grpProcess";
            this.grpProcess.Size = new System.Drawing.Size(712, 320);
            this.grpProcess.TabIndex = 5;
            this.grpProcess.TabStop = false;
            this.grpProcess.Text = "Process Result";
            // 
            // JobProgressBar
            // 
            this.JobProgressBar.Location = new System.Drawing.Point(11, 22);
            this.JobProgressBar.Name = "JobProgressBar";
            this.JobProgressBar.Size = new System.Drawing.Size(685, 23);
            this.JobProgressBar.TabIndex = 0;
            // 
            // txtProcessLog
            // 
            this.txtProcessLog.Location = new System.Drawing.Point(11, 56);
            this.txtProcessLog.Name = "txtProcessLog";
            this.txtProcessLog.ReadOnly = true;
            this.txtProcessLog.Size = new System.Drawing.Size(685, 249);
            this.txtProcessLog.TabIndex = 1;
            this.txtProcessLog.TabStop = false;
            this.txtProcessLog.Text = "";
            this.txtProcessLog.WordWrap = false;
            // 
            // cboRoutingWave
            // 
            this.cboRoutingWave.AllowDrop = true;
            this.cboRoutingWave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoutingWave.FormattingEnabled = true;
            this.cboRoutingWave.Location = new System.Drawing.Point(23, 72);
            this.cboRoutingWave.Name = "cboRoutingWave";
            this.cboRoutingWave.Size = new System.Drawing.Size(262, 21);
            this.cboRoutingWave.TabIndex = 1;
            this.cboRoutingWave.SelectedIndexChanged += new System.EventHandler(this.cboRoutingWave_SelectedIndexChanged);
            // 
            // chkNoRegularInvoices
            // 
            this.chkNoRegularInvoices.AutoSize = true;
            this.chkNoRegularInvoices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNoRegularInvoices.Location = new System.Drawing.Point(16, 26);
            this.chkNoRegularInvoices.Name = "chkNoRegularInvoices";
            this.chkNoRegularInvoices.Size = new System.Drawing.Size(357, 17);
            this.chkNoRegularInvoices.TabIndex = 0;
            this.chkNoRegularInvoices.Text = "Exclude Stops with only Regular Invoices (Delivery and Free Shipping)";
            this.chkNoRegularInvoices.UseVisualStyleBackColor = true;
            this.chkNoRegularInvoices.CheckedChanged += new System.EventHandler(this.chkNoRegularInvoices_CheckedChanged);
            // 
            // grpBoxFilter
            // 
            this.grpBoxFilter.Controls.Add(this.chkOnlyTransfers);
            this.grpBoxFilter.Controls.Add(this.chkNoRegularInvoices);
            this.grpBoxFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxFilter.Location = new System.Drawing.Point(22, 155);
            this.grpBoxFilter.Name = "grpBoxFilter";
            this.grpBoxFilter.Size = new System.Drawing.Size(476, 84);
            this.grpBoxFilter.TabIndex = 2;
            this.grpBoxFilter.TabStop = false;
            this.grpBoxFilter.Text = "Wave Filters";
            // 
            // chkOnlyTransfers
            // 
            this.chkOnlyTransfers.AutoSize = true;
            this.chkOnlyTransfers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOnlyTransfers.Location = new System.Drawing.Point(16, 49);
            this.chkOnlyTransfers.Name = "chkOnlyTransfers";
            this.chkOnlyTransfers.Size = new System.Drawing.Size(157, 17);
            this.chkOnlyTransfers.TabIndex = 1;
            this.chkOnlyTransfers.Text = "Import transfer invoices only";
            this.chkOnlyTransfers.UseVisualStyleBackColor = true;
            this.chkOnlyTransfers.CheckedChanged += new System.EventHandler(this.chkOnlyTransfers_CheckedChanged);
            // 
            // btnUpdateDrivers
            // 
            this.btnUpdateDrivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDrivers.Location = new System.Drawing.Point(508, 28);
            this.btnUpdateDrivers.Name = "btnUpdateDrivers";
            this.btnUpdateDrivers.Size = new System.Drawing.Size(200, 65);
            this.btnUpdateDrivers.TabIndex = 6;
            this.btnUpdateDrivers.Text = "Update Drivers Information in Dispatch Track by Date\r\n\r\n";
            this.btnUpdateDrivers.UseVisualStyleBackColor = true;
            this.btnUpdateDrivers.Click += new System.EventHandler(this.btnUpdateDrivers_Click);
            // 
            // bgwImportOrders
            // 
            this.bgwImportOrders.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwImportOrders_DoWork);
            this.bgwImportOrders.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwImportOrders_ProgressChanged);
            this.bgwImportOrders.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwImportOrders_RunWorkerCompleted);
            // 
            // bgwUpdateDTDrivers
            // 
            this.bgwUpdateDTDrivers.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwUpdateDTDrivers_DoWork);
            this.bgwUpdateDTDrivers.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwUpdateDTDrivers_ProgressChanged);
            this.bgwUpdateDTDrivers.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwUpdateDTDrivers_RunWorkerCompleted);
            // 
            // cmdReset
            // 
            this.cmdReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.Location = new System.Drawing.Point(305, 28);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(109, 31);
            this.cmdReset.TabIndex = 21;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // chkLockStatus
            // 
            this.chkLockStatus.AutoSize = true;
            this.chkLockStatus.Enabled = false;
            this.chkLockStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLockStatus.Location = new System.Drawing.Point(22, 245);
            this.chkLockStatus.Name = "chkLockStatus";
            this.chkLockStatus.Size = new System.Drawing.Size(227, 17);
            this.chkLockStatus.TabIndex = 22;
            this.chkLockStatus.Text = "Check Lock Status for each Orders/Route";
            this.chkLockStatus.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Routing Wave:";
            // 
            // cboRoutingWaveSub
            // 
            this.cboRoutingWaveSub.AllowDrop = true;
            this.cboRoutingWaveSub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoutingWaveSub.FormattingEnabled = true;
            this.cboRoutingWaveSub.Location = new System.Drawing.Point(22, 123);
            this.cboRoutingWaveSub.Name = "cboRoutingWaveSub";
            this.cboRoutingWaveSub.Size = new System.Drawing.Size(262, 21);
            this.cboRoutingWaveSub.TabIndex = 24;
            // 
            // CFDispatchTrackImportOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(736, 638);
            this.Controls.Add(this.cboRoutingWaveSub);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkLockStatus);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.btnUpdateDrivers);
            this.Controls.Add(this.grpBoxFilter);
            this.Controls.Add(this.cmdImportOrders);
            this.Controls.Add(this.cboRoutingWave);
            this.Controls.Add(this.grpProcess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtRoutingDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFDispatchTrackImportOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Orders From AS400 to DispatchTRACK";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CFDispatchTrackImportOrders_FormClosing);
            this.Load += new System.EventHandler(this.CFDispatchTrackImportOrders_Load);
            this.grpProcess.ResumeLayout(false);
            this.grpBoxFilter.ResumeLayout(false);
            this.grpBoxFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdImportOrders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpProcess;
        public System.Windows.Forms.RichTextBox txtProcessLog;
        public System.Windows.Forms.DateTimePicker dtRoutingDate;
        public System.Windows.Forms.ComboBox cboRoutingWave;
        public System.Windows.Forms.ProgressBar JobProgressBar;
        private System.Windows.Forms.CheckBox chkNoRegularInvoices;
        private System.Windows.Forms.GroupBox grpBoxFilter;
        private System.Windows.Forms.Button btnUpdateDrivers;
        private System.ComponentModel.BackgroundWorker bgwImportOrders;
        private System.ComponentModel.BackgroundWorker bgwUpdateDTDrivers;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.CheckBox chkLockStatus;
        private System.Windows.Forms.CheckBox chkOnlyTransfers;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cboRoutingWaveSub;
    }
}

