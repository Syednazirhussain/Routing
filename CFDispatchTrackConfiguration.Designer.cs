namespace RoutingWinApp
{
    partial class CFDispatchTrackConfiguration
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmailFrom = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDispatchTrackURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDispatchTrackPort = new System.Windows.Forms.TextBox();
            this.txtDispatchTrackImportOrder = new System.Windows.Forms.TextBox();
            this.txtDispatchTrackExportOrder = new System.Windows.Forms.TextBox();
            this.txtDispatchTrackGetOrderInfo = new System.Windows.Forms.TextBox();
            this.txtDispatchTrackUpdateDriver = new System.Windows.Forms.TextBox();
            this.txtDispatchTrackUpdateRouteHist = new System.Windows.Forms.TextBox();
            this.txtDispatchTrackDateBeforeExport = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email Settings";
            // 
            // txtEmailFrom
            // 
            this.txtEmailFrom.Location = new System.Drawing.Point(217, 56);
            this.txtEmailFrom.Name = "txtEmailFrom";
            this.txtEmailFrom.Size = new System.Drawing.Size(425, 20);
            this.txtEmailFrom.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(656, 371);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sender Address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Recipient Address:";
            // 
            // txtEmailTo
            // 
            this.txtEmailTo.Location = new System.Drawing.Point(217, 89);
            this.txtEmailTo.Name = "txtEmailTo";
            this.txtEmailTo.Size = new System.Drawing.Size(425, 20);
            this.txtEmailTo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "DispatchTrack API Host:";
            // 
            // txtDispatchTrackURL
            // 
            this.txtDispatchTrackURL.Location = new System.Drawing.Point(217, 122);
            this.txtDispatchTrackURL.Name = "txtDispatchTrackURL";
            this.txtDispatchTrackURL.ReadOnly = true;
            this.txtDispatchTrackURL.Size = new System.Drawing.Size(425, 20);
            this.txtDispatchTrackURL.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "DispatchTrack Port:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "DispatchTrack Import Order:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "DispatchTrack Export Order:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "DispatchTrack Get Order Info:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "DispatchTrack Update Driver:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 315);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "DispatchTrack Update Route History:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 348);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(173, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "DispatchTrack Date Before Export:";
            // 
            // txtDispatchTrackPort
            // 
            this.txtDispatchTrackPort.Location = new System.Drawing.Point(217, 158);
            this.txtDispatchTrackPort.Name = "txtDispatchTrackPort";
            this.txtDispatchTrackPort.Size = new System.Drawing.Size(425, 20);
            this.txtDispatchTrackPort.TabIndex = 14;
            // 
            // txtDispatchTrackImportOrder
            // 
            this.txtDispatchTrackImportOrder.Location = new System.Drawing.Point(217, 187);
            this.txtDispatchTrackImportOrder.Name = "txtDispatchTrackImportOrder";
            this.txtDispatchTrackImportOrder.Size = new System.Drawing.Size(425, 20);
            this.txtDispatchTrackImportOrder.TabIndex = 15;
            // 
            // txtDispatchTrackExportOrder
            // 
            this.txtDispatchTrackExportOrder.Location = new System.Drawing.Point(217, 216);
            this.txtDispatchTrackExportOrder.Name = "txtDispatchTrackExportOrder";
            this.txtDispatchTrackExportOrder.Size = new System.Drawing.Size(425, 20);
            this.txtDispatchTrackExportOrder.TabIndex = 16;
            // 
            // txtDispatchTrackGetOrderInfo
            // 
            this.txtDispatchTrackGetOrderInfo.Location = new System.Drawing.Point(217, 246);
            this.txtDispatchTrackGetOrderInfo.Name = "txtDispatchTrackGetOrderInfo";
            this.txtDispatchTrackGetOrderInfo.Size = new System.Drawing.Size(425, 20);
            this.txtDispatchTrackGetOrderInfo.TabIndex = 17;
            // 
            // txtDispatchTrackUpdateDriver
            // 
            this.txtDispatchTrackUpdateDriver.Location = new System.Drawing.Point(217, 278);
            this.txtDispatchTrackUpdateDriver.Name = "txtDispatchTrackUpdateDriver";
            this.txtDispatchTrackUpdateDriver.Size = new System.Drawing.Size(425, 20);
            this.txtDispatchTrackUpdateDriver.TabIndex = 18;
            // 
            // txtDispatchTrackUpdateRouteHist
            // 
            this.txtDispatchTrackUpdateRouteHist.Location = new System.Drawing.Point(217, 308);
            this.txtDispatchTrackUpdateRouteHist.Name = "txtDispatchTrackUpdateRouteHist";
            this.txtDispatchTrackUpdateRouteHist.Size = new System.Drawing.Size(425, 20);
            this.txtDispatchTrackUpdateRouteHist.TabIndex = 19;
            // 
            // txtDispatchTrackDateBeforeExport
            // 
            this.txtDispatchTrackDateBeforeExport.Location = new System.Drawing.Point(217, 341);
            this.txtDispatchTrackDateBeforeExport.Name = "txtDispatchTrackDateBeforeExport";
            this.txtDispatchTrackDateBeforeExport.Size = new System.Drawing.Size(425, 20);
            this.txtDispatchTrackDateBeforeExport.TabIndex = 20;
            // 
            // CFDispatchTrackConfiguration
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 428);
            this.Controls.Add(this.txtDispatchTrackDateBeforeExport);
            this.Controls.Add(this.txtDispatchTrackUpdateRouteHist);
            this.Controls.Add(this.txtDispatchTrackUpdateDriver);
            this.Controls.Add(this.txtDispatchTrackGetOrderInfo);
            this.Controls.Add(this.txtDispatchTrackExportOrder);
            this.Controls.Add(this.txtDispatchTrackImportOrder);
            this.Controls.Add(this.txtDispatchTrackPort);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDispatchTrackURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEmailTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.txtEmailFrom);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFDispatchTrackConfiguration";
            this.Text = "CFRoadNetConfiguration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmailFrom;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmailTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDispatchTrackURL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDispatchTrackPort;
        private System.Windows.Forms.TextBox txtDispatchTrackImportOrder;
        private System.Windows.Forms.TextBox txtDispatchTrackExportOrder;
        private System.Windows.Forms.TextBox txtDispatchTrackGetOrderInfo;
        private System.Windows.Forms.TextBox txtDispatchTrackUpdateDriver;
        private System.Windows.Forms.TextBox txtDispatchTrackUpdateRouteHist;
        private System.Windows.Forms.TextBox txtDispatchTrackDateBeforeExport;
    }
}