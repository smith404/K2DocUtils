
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
            this.SuspendLayout();
            // 
            // versionLbl
            // 
            this.versionLbl.AutoSize = true;
            this.versionLbl.Location = new System.Drawing.Point(54, 440);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(90, 13);
            this.versionLbl.TabIndex = 0;
            this.versionLbl.Text = "Software Version:";
            // 
            // versionNumberLbl
            // 
            this.versionNumberLbl.AutoSize = true;
            this.versionNumberLbl.Location = new System.Drawing.Point(142, 440);
            this.versionNumberLbl.Name = "versionNumberLbl";
            this.versionNumberLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.versionNumberLbl.Size = new System.Drawing.Size(0, 13);
            this.versionNumberLbl.TabIndex = 1;
            // 
            // PropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 461);
            this.Controls.Add(this.versionNumberLbl);
            this.Controls.Add(this.versionLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesForm";
            this.Text = "Preferences";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PropertiesForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label versionLbl;
        private System.Windows.Forms.Label versionNumberLbl;
    }
}