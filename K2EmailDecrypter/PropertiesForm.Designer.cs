
namespace K2EmailDecrypter
{
    partial class PropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesForm));
            this.VersionLbl = new System.Windows.Forms.Label();
            this.VersionNumberLbl = new System.Windows.Forms.Label();
            this.CryptoProviderCbb = new System.Windows.Forms.ComboBox();
            this.CryptoProviderLbl = new System.Windows.Forms.Label();
            this.RefreshLbl = new System.Windows.Forms.Label();
            this.RefreshTrk = new System.Windows.Forms.TrackBar();
            this.SecondsLbl = new System.Windows.Forms.Label();
            this.ValLbl = new System.Windows.Forms.Label();
            this.TokenLbl = new System.Windows.Forms.Label();
            this.TokenTxt = new System.Windows.Forms.TextBox();
            this.ViewBtn = new System.Windows.Forms.Button();
            this.NotificationsLbl = new System.Windows.Forms.Label();
            this.NotificationsCbx = new System.Windows.Forms.CheckBox();
            this.PortTxt = new System.Windows.Forms.TextBox();
            this.PortLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshTrk)).BeginInit();
            this.SuspendLayout();
            // 
            // VersionLbl
            // 
            this.VersionLbl.AutoSize = true;
            this.VersionLbl.Location = new System.Drawing.Point(186, 239);
            this.VersionLbl.Name = "VersionLbl";
            this.VersionLbl.Size = new System.Drawing.Size(90, 13);
            this.VersionLbl.TabIndex = 0;
            this.VersionLbl.Text = "Software Version:";
            // 
            // VersionNumberLbl
            // 
            this.VersionNumberLbl.AutoSize = true;
            this.VersionNumberLbl.Location = new System.Drawing.Point(274, 239);
            this.VersionNumberLbl.Name = "VersionNumberLbl";
            this.VersionNumberLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.VersionNumberLbl.Size = new System.Drawing.Size(0, 13);
            this.VersionNumberLbl.TabIndex = 1;
            // 
            // CryptoProviderCbb
            // 
            this.CryptoProviderCbb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CryptoProviderCbb.FormattingEnabled = true;
            this.CryptoProviderCbb.Location = new System.Drawing.Point(142, 75);
            this.CryptoProviderCbb.Name = "CryptoProviderCbb";
            this.CryptoProviderCbb.Size = new System.Drawing.Size(320, 21);
            this.CryptoProviderCbb.TabIndex = 2;
            // 
            // CryptoProviderLbl
            // 
            this.CryptoProviderLbl.AutoSize = true;
            this.CryptoProviderLbl.Location = new System.Drawing.Point(22, 78);
            this.CryptoProviderLbl.Name = "CryptoProviderLbl";
            this.CryptoProviderLbl.Size = new System.Drawing.Size(114, 13);
            this.CryptoProviderLbl.TabIndex = 3;
            this.CryptoProviderLbl.Text = "Cryptographic Provider";
            // 
            // RefreshLbl
            // 
            this.RefreshLbl.AutoSize = true;
            this.RefreshLbl.Location = new System.Drawing.Point(54, 115);
            this.RefreshLbl.Name = "RefreshLbl";
            this.RefreshLbl.Size = new System.Drawing.Size(82, 13);
            this.RefreshLbl.TabIndex = 4;
            this.RefreshLbl.Text = "Refresh Interval";
            // 
            // RefreshTrk
            // 
            this.RefreshTrk.LargeChange = 60;
            this.RefreshTrk.Location = new System.Drawing.Point(136, 106);
            this.RefreshTrk.Name = "RefreshTrk";
            this.RefreshTrk.Size = new System.Drawing.Size(247, 45);
            this.RefreshTrk.SmallChange = 30;
            this.RefreshTrk.TabIndex = 5;
            this.RefreshTrk.Value = 10;
            this.RefreshTrk.ValueChanged += new System.EventHandler(this.RefeshTrk_ValueChanged);
            // 
            // SecondsLbl
            // 
            this.SecondsLbl.AutoSize = true;
            this.SecondsLbl.Location = new System.Drawing.Point(414, 115);
            this.SecondsLbl.Name = "SecondsLbl";
            this.SecondsLbl.Size = new System.Drawing.Size(49, 13);
            this.SecondsLbl.TabIndex = 6;
            this.SecondsLbl.Text = "Seconds";
            // 
            // ValLbl
            // 
            this.ValLbl.AutoSize = true;
            this.ValLbl.Location = new System.Drawing.Point(384, 115);
            this.ValLbl.Name = "ValLbl";
            this.ValLbl.Size = new System.Drawing.Size(0, 13);
            this.ValLbl.TabIndex = 7;
            // 
            // TokenLbl
            // 
            this.TokenLbl.AutoSize = true;
            this.TokenLbl.Location = new System.Drawing.Point(98, 47);
            this.TokenLbl.Name = "TokenLbl";
            this.TokenLbl.Size = new System.Drawing.Size(38, 13);
            this.TokenLbl.TabIndex = 8;
            this.TokenLbl.Text = "Token";
            // 
            // TokenTxt
            // 
            this.TokenTxt.Location = new System.Drawing.Point(143, 44);
            this.TokenTxt.Name = "TokenTxt";
            this.TokenTxt.Size = new System.Drawing.Size(290, 20);
            this.TokenTxt.TabIndex = 9;
            // 
            // ViewBtn
            // 
            this.ViewBtn.Location = new System.Drawing.Point(439, 44);
            this.ViewBtn.Name = "ViewBtn";
            this.ViewBtn.Size = new System.Drawing.Size(23, 19);
            this.ViewBtn.TabIndex = 10;
            this.ViewBtn.Text = "V";
            this.ViewBtn.UseVisualStyleBackColor = true;
            this.ViewBtn.Click += new System.EventHandler(this.ViewBtn_Click);
            // 
            // NotificationsLbl
            // 
            this.NotificationsLbl.AutoSize = true;
            this.NotificationsLbl.Location = new System.Drawing.Point(41, 154);
            this.NotificationsLbl.Name = "NotificationsLbl";
            this.NotificationsLbl.Size = new System.Drawing.Size(95, 13);
            this.NotificationsLbl.TabIndex = 11;
            this.NotificationsLbl.Text = "Show Notifications";
            // 
            // NotificationsCbx
            // 
            this.NotificationsCbx.AutoSize = true;
            this.NotificationsCbx.Location = new System.Drawing.Point(142, 154);
            this.NotificationsCbx.Name = "NotificationsCbx";
            this.NotificationsCbx.Size = new System.Drawing.Size(15, 14);
            this.NotificationsCbx.TabIndex = 12;
            this.NotificationsCbx.UseVisualStyleBackColor = true;
            // 
            // PortTxt
            // 
            this.PortTxt.Location = new System.Drawing.Point(142, 12);
            this.PortTxt.Name = "PortTxt";
            this.PortTxt.Size = new System.Drawing.Size(129, 20);
            this.PortTxt.TabIndex = 14;
            // 
            // PortLbl
            // 
            this.PortLbl.AutoSize = true;
            this.PortLbl.Location = new System.Drawing.Point(97, 15);
            this.PortLbl.Name = "PortLbl";
            this.PortLbl.Size = new System.Drawing.Size(26, 13);
            this.PortLbl.TabIndex = 13;
            this.PortLbl.Text = "Port";
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.PortTxt);
            this.Controls.Add(this.PortLbl);
            this.Controls.Add(this.NotificationsCbx);
            this.Controls.Add(this.NotificationsLbl);
            this.Controls.Add(this.ViewBtn);
            this.Controls.Add(this.TokenTxt);
            this.Controls.Add(this.TokenLbl);
            this.Controls.Add(this.ValLbl);
            this.Controls.Add(this.SecondsLbl);
            this.Controls.Add(this.RefreshTrk);
            this.Controls.Add(this.RefreshLbl);
            this.Controls.Add(this.CryptoProviderLbl);
            this.Controls.Add(this.CryptoProviderCbb);
            this.Controls.Add(this.VersionNumberLbl);
            this.Controls.Add(this.VersionLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesForm";
            this.Text = "Preferences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropertiesForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.RefreshTrk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label VersionLbl;
        private System.Windows.Forms.Label VersionNumberLbl;
        private System.Windows.Forms.ComboBox CryptoProviderCbb;
        private System.Windows.Forms.Label CryptoProviderLbl;
        private System.Windows.Forms.Label RefreshLbl;
        private System.Windows.Forms.TrackBar RefreshTrk;
        private System.Windows.Forms.Label SecondsLbl;
        private System.Windows.Forms.Label ValLbl;
        private System.Windows.Forms.Label TokenLbl;
        private System.Windows.Forms.TextBox TokenTxt;
        private System.Windows.Forms.Button ViewBtn;
        private System.Windows.Forms.Label NotificationsLbl;
        private System.Windows.Forms.CheckBox NotificationsCbx;
        private System.Windows.Forms.TextBox PortTxt;
        private System.Windows.Forms.Label PortLbl;
    }
}