namespace RoutingWinApp
{
    partial class CFRoutingWinAppMainToolBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFRoutingWinAppMainToolBar));
            this.dispatchtrackgrp = new System.Windows.Forms.GroupBox();
            this.cmdExportOrdersDispatchTrack = new System.Windows.Forms.Button();
            this.cmdImportOrdersDispatchTrack = new System.Windows.Forms.Button();
            this.cmdSettings = new System.Windows.Forms.Button();
            this.dispatchtrackgrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // dispatchtrackgrp
            // 
            this.dispatchtrackgrp.Controls.Add(this.cmdExportOrdersDispatchTrack);
            this.dispatchtrackgrp.Controls.Add(this.cmdImportOrdersDispatchTrack);
            this.dispatchtrackgrp.Location = new System.Drawing.Point(17, 11);
            this.dispatchtrackgrp.Margin = new System.Windows.Forms.Padding(2);
            this.dispatchtrackgrp.Name = "dispatchtrackgrp";
            this.dispatchtrackgrp.Padding = new System.Windows.Forms.Padding(2);
            this.dispatchtrackgrp.Size = new System.Drawing.Size(371, 140);
            this.dispatchtrackgrp.TabIndex = 7;
            this.dispatchtrackgrp.TabStop = false;
            this.dispatchtrackgrp.Text = "DispatchTrack Routing";
            // 
            // cmdExportOrdersDispatchTrack
            // 
            this.cmdExportOrdersDispatchTrack.BackColor = System.Drawing.SystemColors.Info;
            this.cmdExportOrdersDispatchTrack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdExportOrdersDispatchTrack.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportOrdersDispatchTrack.Image = global::RoutingWinApp.Properties.Resources.Export2;
            this.cmdExportOrdersDispatchTrack.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExportOrdersDispatchTrack.Location = new System.Drawing.Point(152, 24);
            this.cmdExportOrdersDispatchTrack.Name = "cmdExportOrdersDispatchTrack";
            this.cmdExportOrdersDispatchTrack.Size = new System.Drawing.Size(116, 106);
            this.cmdExportOrdersDispatchTrack.TabIndex = 5;
            this.cmdExportOrdersDispatchTrack.Text = "Retrieve Routed Orders from DispatchTrack";
            this.cmdExportOrdersDispatchTrack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdExportOrdersDispatchTrack.UseVisualStyleBackColor = false;
            this.cmdExportOrdersDispatchTrack.Click += new System.EventHandler(this.cmdExportOrdersDispatchTrack_Click);
            // 
            // cmdImportOrdersDispatchTrack
            // 
            this.cmdImportOrdersDispatchTrack.BackColor = System.Drawing.SystemColors.Info;
            this.cmdImportOrdersDispatchTrack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdImportOrdersDispatchTrack.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdImportOrdersDispatchTrack.Image = global::RoutingWinApp.Properties.Resources.Import2;
            this.cmdImportOrdersDispatchTrack.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdImportOrdersDispatchTrack.Location = new System.Drawing.Point(16, 24);
            this.cmdImportOrdersDispatchTrack.Name = "cmdImportOrdersDispatchTrack";
            this.cmdImportOrdersDispatchTrack.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmdImportOrdersDispatchTrack.Size = new System.Drawing.Size(116, 106);
            this.cmdImportOrdersDispatchTrack.TabIndex = 4;
            this.cmdImportOrdersDispatchTrack.Text = "Send Orders to DispatchTrack";
            this.cmdImportOrdersDispatchTrack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdImportOrdersDispatchTrack.UseVisualStyleBackColor = false;
            this.cmdImportOrdersDispatchTrack.Click += new System.EventHandler(this.cmdImportOrdersDispatchTrack_Click);
            // 
            // cmdSettings
            // 
            this.cmdSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdSettings.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.cmdSettings.Image = global::RoutingWinApp.Properties.Resources.Settings;
            this.cmdSettings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSettings.Location = new System.Drawing.Point(298, 35);
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.Size = new System.Drawing.Size(80, 106);
            this.cmdSettings.TabIndex = 3;
            this.cmdSettings.Text = "Settings";
            this.cmdSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSettings.UseVisualStyleBackColor = true;
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // CFRoutingWinAppMainToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 157);
            this.Controls.Add(this.cmdSettings);
            this.Controls.Add(this.dispatchtrackgrp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFRoutingWinAppMainToolBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CF - Roouting Tools";
            this.dispatchtrackgrp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdSettings;
        private System.Windows.Forms.Button cmdExportOrdersDispatchTrack;
        private System.Windows.Forms.Button cmdImportOrdersDispatchTrack;
        private System.Windows.Forms.GroupBox dispatchtrackgrp;
    }
}