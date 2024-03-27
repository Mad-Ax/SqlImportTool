
namespace SqlImportTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sitelogFileButton = new System.Windows.Forms.Button();
            this.proactButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sitelogFileButton
            // 
            this.sitelogFileButton.Location = new System.Drawing.Point(12, 12);
            this.sitelogFileButton.Name = "sitelogFileButton";
            this.sitelogFileButton.Size = new System.Drawing.Size(56, 23);
            this.sitelogFileButton.TabIndex = 0;
            this.sitelogFileButton.Text = "Sitelog";
            this.sitelogFileButton.UseVisualStyleBackColor = true;
            this.sitelogFileButton.Click += new System.EventHandler(this.sitelogFileButton_Click);
            // 
            // proactButton
            // 
            this.proactButton.Location = new System.Drawing.Point(12, 41);
            this.proactButton.Name = "proactButton";
            this.proactButton.Size = new System.Drawing.Size(56, 23);
            this.proactButton.TabIndex = 1;
            this.proactButton.Text = "Proact";
            this.proactButton.UseVisualStyleBackColor = true;
            this.proactButton.Click += new System.EventHandler(this.proactButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.proactButton);
            this.Controls.Add(this.sitelogFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button sitelogFileButton;
        private System.Windows.Forms.Button proactButton;
    }
}

