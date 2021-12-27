
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
            this.versionLbl = new System.Windows.Forms.Label();
            this.versionNumberLbl = new System.Windows.Forms.Label();
            this.cryptoProviderCbb = new System.Windows.Forms.ComboBox();
            this.cryptoProviderLbl = new System.Windows.Forms.Label();
            this.refreshLbl = new System.Windows.Forms.Label();
            this.refreshTrk = new System.Windows.Forms.TrackBar();
            this.secondsLbl = new System.Windows.Forms.Label();
            this.valLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.refreshTrk)).BeginInit();
            this.SuspendLayout();
            // 
            // versionLbl
            // 
            this.versionLbl.AutoSize = true;
            this.versionLbl.Location = new System.Drawing.Point(186, 239);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(90, 13);
            this.versionLbl.TabIndex = 0;
            this.versionLbl.Text = "Software Version:";
            // 
            // versionNumberLbl
            // 
            this.versionNumberLbl.AutoSize = true;
            this.versionNumberLbl.Location = new System.Drawing.Point(274, 239);
            this.versionNumberLbl.Name = "versionNumberLbl";
            this.versionNumberLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.versionNumberLbl.Size = new System.Drawing.Size(0, 13);
            this.versionNumberLbl.TabIndex = 1;
            // 
            // cryptoProviderCbb
            // 
            this.cryptoProviderCbb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cryptoProviderCbb.FormattingEnabled = true;
            this.cryptoProviderCbb.Location = new System.Drawing.Point(142, 36);
            this.cryptoProviderCbb.Name = "cryptoProviderCbb";
            this.cryptoProviderCbb.Size = new System.Drawing.Size(320, 21);
            this.cryptoProviderCbb.TabIndex = 2;
            // 
            // cryptoProviderLbl
            // 
            this.cryptoProviderLbl.AutoSize = true;
            this.cryptoProviderLbl.Location = new System.Drawing.Point(22, 39);
            this.cryptoProviderLbl.Name = "cryptoProviderLbl";
            this.cryptoProviderLbl.Size = new System.Drawing.Size(114, 13);
            this.cryptoProviderLbl.TabIndex = 3;
            this.cryptoProviderLbl.Text = "Cryptographic Provider";
            // 
            // refreshLbl
            // 
            this.refreshLbl.AutoSize = true;
            this.refreshLbl.Location = new System.Drawing.Point(54, 76);
            this.refreshLbl.Name = "refreshLbl";
            this.refreshLbl.Size = new System.Drawing.Size(82, 13);
            this.refreshLbl.TabIndex = 4;
            this.refreshLbl.Text = "Refresh Interval";
            // 
            // refreshTrk
            // 
            this.refreshTrk.LargeChange = 60;
            this.refreshTrk.Location = new System.Drawing.Point(136, 67);
            this.refreshTrk.Maximum = 1800;
            this.refreshTrk.Minimum = 60;
            this.refreshTrk.Name = "refreshTrk";
            this.refreshTrk.Size = new System.Drawing.Size(247, 45);
            this.refreshTrk.SmallChange = 30;
            this.refreshTrk.TabIndex = 5;
            this.refreshTrk.Value = 60;
            this.refreshTrk.ValueChanged += new System.EventHandler(this.refeshTrk_ValueChanged);
            // 
            // secondsLbl
            // 
            this.secondsLbl.AutoSize = true;
            this.secondsLbl.Location = new System.Drawing.Point(414, 76);
            this.secondsLbl.Name = "secondsLbl";
            this.secondsLbl.Size = new System.Drawing.Size(49, 13);
            this.secondsLbl.TabIndex = 6;
            this.secondsLbl.Text = "Seconds";
            // 
            // valLbl
            // 
            this.valLbl.AutoSize = true;
            this.valLbl.Location = new System.Drawing.Point(384, 76);
            this.valLbl.Name = "valLbl";
            this.valLbl.Size = new System.Drawing.Size(0, 13);
            this.valLbl.TabIndex = 7;
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.valLbl);
            this.Controls.Add(this.secondsLbl);
            this.Controls.Add(this.refreshTrk);
            this.Controls.Add(this.refreshLbl);
            this.Controls.Add(this.cryptoProviderLbl);
            this.Controls.Add(this.cryptoProviderCbb);
            this.Controls.Add(this.versionNumberLbl);
            this.Controls.Add(this.versionLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesForm";
            this.Text = "Preferences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropertiesForm_FormClosing);
            this.Load += new System.EventHandler(this.PropertiesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.refreshTrk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label versionLbl;
        private System.Windows.Forms.Label versionNumberLbl;
        private System.Windows.Forms.ComboBox cryptoProviderCbb;
        private System.Windows.Forms.Label cryptoProviderLbl;
        private System.Windows.Forms.Label refreshLbl;
        private System.Windows.Forms.TrackBar refreshTrk;
        private System.Windows.Forms.Label secondsLbl;
        private System.Windows.Forms.Label valLbl;
    }
}