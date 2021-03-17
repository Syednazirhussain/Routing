namespace RoutingWinApp
{
    partial class CFDispatchTrackExportOrders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFDispatchTrackExportOrders));
            this.dtRoutingDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdExportOrders = new System.Windows.Forms.Button();
            this.grpProcess = new System.Windows.Forms.GroupBox();
            this.JobProgressBar = new System.Windows.Forms.ProgressBar();
            this.txtProcessLog = new System.Windows.Forms.RichTextBox();
            this.cboBuildRoutingWave = new System.Windows.Forms.ComboBox();
            this.opgBuildRoutes = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.RoutesList = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.opgExportMode = new System.Windows.Forms.Panel();
            this.chkVerified = new System.Windows.Forms.CheckBox();
            this.lblListHeader = new System.Windows.Forms.Label();
            this.cmdReset = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.bgwExportOrders = new System.ComponentModel.BackgroundWorker();
            this.bgwLoadingRoutes = new System.ComponentModel.BackgroundWorker();
            this.lblNodesSelected = new System.Windows.Forms.Label();
            this.txtExportAlert = new System.Windows.Forms.TextBox();
            this.grpProcess.SuspendLayout();
            this.opgExportMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtRoutingDate
            // 
            this.dtRoutingDate.Location = new System.Drawing.Point(22, 78);
            this.dtRoutingDate.Name = "dtRoutingDate";
            this.dtRoutingDate.Size = new System.Drawing.Size(248, 20);
            this.dtRoutingDate.TabIndex = 0;
            this.dtRoutingDate.ValueChanged += new System.EventHandler(this.dtRoutingDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Session Date:";
            // 
            // cmdExportOrders
            // 
            this.cmdExportOrders.Enabled = false;
            this.cmdExportOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportOrders.Location = new System.Drawing.Point(376, 221);
            this.cmdExportOrders.Name = "cmdExportOrders";
            this.cmdExportOrders.Size = new System.Drawing.Size(300, 43);
            this.cmdExportOrders.TabIndex = 3;
            this.cmdExportOrders.Text = "Export Routed Orders from DispatchTrack to AS400";
            this.cmdExportOrders.UseVisualStyleBackColor = true;
            this.cmdExportOrders.Click += new System.EventHandler(this.cmdExportOrders_Click);
            // 
            // grpProcess
            // 
            this.grpProcess.Controls.Add(this.JobProgressBar);
            this.grpProcess.Controls.Add(this.txtProcessLog);
            this.grpProcess.Location = new System.Drawing.Point(22, 266);
            this.grpProcess.Name = "grpProcess";
            this.grpProcess.Size = new System.Drawing.Size(651, 317);
            this.grpProcess.TabIndex = 10;
            this.grpProcess.TabStop = false;
            this.grpProcess.Text = "Process Result";
            // 
            // JobProgressBar
            // 
            this.JobProgressBar.Location = new System.Drawing.Point(11, 22);
            this.JobProgressBar.Name = "JobProgressBar";
            this.JobProgressBar.Size = new System.Drawing.Size(439, 23);
            this.JobProgressBar.TabIndex = 0;
            // 
            // txtProcessLog
            // 
            this.txtProcessLog.Location = new System.Drawing.Point(11, 51);
            this.txtProcessLog.Name = "txtProcessLog";
            this.txtProcessLog.ReadOnly = true;
            this.txtProcessLog.Size = new System.Drawing.Size(622, 252);
            this.txtProcessLog.TabIndex = 1;
            this.txtProcessLog.TabStop = false;
            this.txtProcessLog.Text = "";
            this.txtProcessLog.WordWrap = false;
            // 
            // cboBuildRoutingWave
            // 
            this.cboBuildRoutingWave.AllowDrop = true;
            this.cboBuildRoutingWave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuildRoutingWave.FormattingEnabled = true;
            this.cboBuildRoutingWave.Location = new System.Drawing.Point(102, 110);
            this.cboBuildRoutingWave.Name = "cboBuildRoutingWave";
            this.cboBuildRoutingWave.Size = new System.Drawing.Size(456, 21);
            this.cboBuildRoutingWave.TabIndex = 1;
            this.cboBuildRoutingWave.SelectedIndexChanged += new System.EventHandler(this.cboBuildRoutingWave_SelectedIndexChanged);
            // 
            // opgBuildRoutes
            // 
            this.opgBuildRoutes.AutoSize = true;
            this.opgBuildRoutes.Location = new System.Drawing.Point(9, 17);
            this.opgBuildRoutes.Name = "opgBuildRoutes";
            this.opgBuildRoutes.Size = new System.Drawing.Size(127, 17);
            this.opgBuildRoutes.TabIndex = 13;
            this.opgBuildRoutes.TabStop = true;
            this.opgBuildRoutes.Text = "Build Routes to WMS";
            this.opgBuildRoutes.UseVisualStyleBackColor = true;
            this.opgBuildRoutes.CheckedChanged += new System.EventHandler(this.opgBuildRoutes_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Export Mode:";
            // 
            // RoutesList
            // 
            this.RoutesList.Location = new System.Drawing.Point(690, 62);
            this.RoutesList.Name = "RoutesList";
            this.RoutesList.Size = new System.Drawing.Size(419, 521);
            this.RoutesList.TabIndex = 16;
            this.RoutesList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.RoutesList_AfterCheck);
            this.RoutesList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RoutesList_AfterSelect);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Sessions:";
            // 
            // opgExportMode
            // 
            this.opgExportMode.Controls.Add(this.chkVerified);
            this.opgExportMode.Controls.Add(this.opgBuildRoutes);
            this.opgExportMode.Location = new System.Drawing.Point(102, 137);
            this.opgExportMode.Name = "opgExportMode";
            this.opgExportMode.Size = new System.Drawing.Size(456, 77);
            this.opgExportMode.TabIndex = 18;
            this.opgExportMode.Paint += new System.Windows.Forms.PaintEventHandler(this.opgExportMode_Paint);
            // 
            // chkVerified
            // 
            this.chkVerified.AutoSize = true;
            this.chkVerified.Enabled = false;
            this.chkVerified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.chkVerified.Location = new System.Drawing.Point(21, 44);
            this.chkVerified.Name = "chkVerified";
            this.chkVerified.Size = new System.Drawing.Size(358, 17);
            this.chkVerified.TabIndex = 14;
            this.chkVerified.Text = "I have verified the routes and stops and agree to proceed.";
            this.chkVerified.UseVisualStyleBackColor = true;
            this.chkVerified.CheckedChanged += new System.EventHandler(this.chkVerified_CheckedChanged);
            // 
            // lblListHeader
            // 
            this.lblListHeader.AutoSize = true;
            this.lblListHeader.Location = new System.Drawing.Point(687, 16);
            this.lblListHeader.Name = "lblListHeader";
            this.lblListHeader.Size = new System.Drawing.Size(42, 13);
            this.lblListHeader.TabIndex = 19;
            this.lblListHeader.Text = "Details:";
            // 
            // cmdReset
            // 
            this.cmdReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.Location = new System.Drawing.Point(293, 73);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(109, 31);
            this.cmdReset.TabIndex = 20;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(383, 37);
            this.label5.TabIndex = 21;
            this.label5.Text = "Select the date and sessions to process. Only Sessions with unassigned stops and " +
    "build routes will be listed. ";
            // 
            // cmdExpand
            // 
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(1063, 11);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(21, 23);
            this.cmdExpand.TabIndex = 22;
            this.cmdExpand.Text = "E";
            this.cmdExpand.UseVisualStyleBackColor = true;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(1085, 11);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(21, 23);
            this.cmdCollapse.TabIndex = 23;
            this.cmdCollapse.Text = "C";
            this.cmdCollapse.UseVisualStyleBackColor = true;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // bgwExportOrders
            // 
            this.bgwExportOrders.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwExportOrders_DoWork);
            this.bgwExportOrders.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwExportOrders_ProgressChanged);
            this.bgwExportOrders.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwExportOrders_RunWorkerCompleted);
            // 
            // bgwLoadingRoutes
            // 
            this.bgwLoadingRoutes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoadingRoutes_DoWork);
            this.bgwLoadingRoutes.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwLoadingRoutes_ProgressChanged);
            this.bgwLoadingRoutes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoadingRoutes_RunWorkerCompleted);
            // 
            // lblNodesSelected
            // 
            this.lblNodesSelected.AutoSize = true;
            this.lblNodesSelected.Location = new System.Drawing.Point(687, 41);
            this.lblNodesSelected.Name = "lblNodesSelected";
            this.lblNodesSelected.Size = new System.Drawing.Size(95, 13);
            this.lblNodesSelected.TabIndex = 24;
            this.lblNodesSelected.Text = "Selected Nodes: 0";
            // 
            // txtExportAlert
            // 
            this.txtExportAlert.BackColor = System.Drawing.SystemColors.Window;
            this.txtExportAlert.Location = new System.Drawing.Point(253, 234);
            this.txtExportAlert.Name = "txtExportAlert";
            this.txtExportAlert.Size = new System.Drawing.Size(100, 20);
            this.txtExportAlert.TabIndex = 25;
            // 
            // CFDispatchTrackExportOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1121, 595);
            this.Controls.Add(this.txtExportAlert);
            this.Controls.Add(this.lblNodesSelected);
            this.Controls.Add(this.cmdExportOrders);
            this.Controls.Add(this.cmdCollapse);
            this.Controls.Add(this.cmdExpand);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.lblListHeader);
            this.Controls.Add(this.opgExportMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RoutesList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboBuildRoutingWave);
            this.Controls.Add(this.grpProcess);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtRoutingDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFDispatchTrackExportOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retrieve Routed Orders from DispatchTrack to AS400";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CFDispatchTrackExportOrders_FormClosing);
            this.Load += new System.EventHandler(this.CFRoadNetExportOrders_Load);
            this.grpProcess.ResumeLayout(false);
            this.opgExportMode.ResumeLayout(false);
            this.opgExportMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdExportOrders;
        private System.Windows.Forms.GroupBox grpProcess;
        public System.Windows.Forms.RichTextBox txtProcessLog;
        public System.Windows.Forms.DateTimePicker dtRoutingDate;
        public System.Windows.Forms.ComboBox cboBuildRoutingWave;
        public System.Windows.Forms.ProgressBar JobProgressBar;
        private System.Windows.Forms.RadioButton opgBuildRoutes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView RoutesList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel opgExportMode;
        private System.Windows.Forms.Label lblListHeader;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.CheckBox chkVerified;
        private System.ComponentModel.BackgroundWorker bgwExportOrders;
        private System.ComponentModel.BackgroundWorker bgwLoadingRoutes;
        private System.Windows.Forms.Label lblNodesSelected;
        private System.Windows.Forms.TextBox txtExportAlert;
    }
}

