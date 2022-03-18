
namespace K2EmailDecrypter
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.StartBtn = new System.Windows.Forms.Button();
            this.OutputTxt = new System.Windows.Forms.TextBox();
            this.HaltBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(12, 12);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 28);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // OutputTxt
            // 
            this.OutputTxt.Location = new System.Drawing.Point(93, 12);
            this.OutputTxt.Multiline = true;
            this.OutputTxt.Name = "OutputTxt";
            this.OutputTxt.ReadOnly = true;
            this.OutputTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTxt.Size = new System.Drawing.Size(680, 200);
            this.OutputTxt.TabIndex = 1;
            // 
            // HaltBtn
            // 
            this.HaltBtn.Location = new System.Drawing.Point(12, 46);
            this.HaltBtn.Name = "HaltBtn";
            this.HaltBtn.Size = new System.Drawing.Size(75, 28);
            this.HaltBtn.TabIndex = 2;
            this.HaltBtn.Text = "Halt";
            this.HaltBtn.UseVisualStyleBackColor = true;
            this.HaltBtn.Click += new System.EventHandler(this.HaltBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 211);
            this.Controls.Add(this.HaltBtn);
            this.Controls.Add(this.OutputTxt);
            this.Controls.Add(this.StartBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Email Decrypter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox OutputTxt;
        private System.Windows.Forms.Button HaltBtn;
    }
}

