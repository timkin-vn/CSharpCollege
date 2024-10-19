namespace HelloWinFormsApp
{
    partial class HelloForm
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
            this.HelloButton = new System.Windows.Forms.Button();
            this.NameInputLabel = new System.Windows.Forms.Label();
            this.NameInputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // HelloButton
            // 
            this.HelloButton.Location = new System.Drawing.Point(119, 67);
            this.HelloButton.Name = "HelloButton";
            this.HelloButton.Size = new System.Drawing.Size(75, 23);
            this.HelloButton.TabIndex = 0;
            this.HelloButton.Text = "Привет!";
            this.HelloButton.UseVisualStyleBackColor = true;
            this.HelloButton.Click += new System.EventHandler(this.HelloButton_Click);
            // 
            // NameInputLabel
            // 
            this.NameInputLabel.AutoSize = true;
            this.NameInputLabel.Location = new System.Drawing.Point(12, 9);
            this.NameInputLabel.Name = "NameInputLabel";
            this.NameInputLabel.Size = new System.Drawing.Size(154, 13);
            this.NameInputLabel.TabIndex = 1;
            this.NameInputLabel.Text = "Представьтесь, пожалуйста:";
            // 
            // NameInputTextBox
            // 
            this.NameInputTextBox.Location = new System.Drawing.Point(12, 25);
            this.NameInputTextBox.Name = "NameInputTextBox";
            this.NameInputTextBox.Size = new System.Drawing.Size(182, 20);
            this.NameInputTextBox.TabIndex = 2;
            // 
            // HelloForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 102);
            this.Controls.Add(this.NameInputTextBox);
            this.Controls.Add(this.NameInputLabel);
            this.Controls.Add(this.HelloButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HelloForm";
            this.Text = "Привет!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HelloButton;
        private System.Windows.Forms.Label NameInputLabel;
        private System.Windows.Forms.TextBox NameInputTextBox;
    }
}

