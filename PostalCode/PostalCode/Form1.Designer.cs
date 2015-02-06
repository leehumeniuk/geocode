namespace PostalCode
{
    partial class Form1
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
            this.retrieveData = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.quitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // retrieveData
            // 
            this.retrieveData.Location = new System.Drawing.Point(42, 50);
            this.retrieveData.Name = "retrieveData";
            this.retrieveData.Size = new System.Drawing.Size(126, 23);
            this.retrieveData.TabIndex = 8;
            this.retrieveData.Text = "Retrieve Data";
            this.retrieveData.UseVisualStyleBackColor = true;
            this.retrieveData.Click += new System.EventHandler(this.retrieveDataButton_Click);
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(352, 50);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(82, 13);
            this.messageLabel.TabIndex = 9;
            this.messageLabel.Text = "Status: Pending";
            this.messageLabel.Click += new System.EventHandler(this.messageLabel_Click);
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(458, 134);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 23);
            this.quitButton.TabIndex = 10;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 175);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.retrieveData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button retrieveData;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button quitButton;
    }
}

