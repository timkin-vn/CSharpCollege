namespace AlarmClock.Forms
{
    partial class AwakeForm
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
            this.components = new System.ComponentModel.Container();
            this.AlarmMessageLabel = new System.Windows.Forms.Label();
            this.AwakePictureBox = new System.Windows.Forms.PictureBox();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.InputProblem = new System.Windows.Forms.TextBox();
            this.ProblemText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AlarmMessageLabel
            // 
            this.AlarmMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AlarmMessageLabel.Location = new System.Drawing.Point(12, 9);
            this.AlarmMessageLabel.Name = "AlarmMessageLabel";
            this.AlarmMessageLabel.Size = new System.Drawing.Size(456, 38);
            this.AlarmMessageLabel.TabIndex = 0;
            this.AlarmMessageLabel.Text = "Сообщение";
            this.AlarmMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AlarmMessageLabel.Click += new System.EventHandler(this.AlarmMessageLabel_Click);
            // 
            // AwakePictureBox
            // 
            this.AwakePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AwakePictureBox.Location = new System.Drawing.Point(12, 51);
            this.AwakePictureBox.Name = "AwakePictureBox";
            this.AwakePictureBox.Size = new System.Drawing.Size(456, 248);
            this.AwakePictureBox.TabIndex = 2;
            this.AwakePictureBox.TabStop = false;
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);
            // 
            // InputProblem
            // 
            this.InputProblem.Location = new System.Drawing.Point(12, 348);
            this.InputProblem.Name = "InputProblem";
            this.InputProblem.Size = new System.Drawing.Size(456, 20);
            this.InputProblem.TabIndex = 3;
            this.InputProblem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ProblemText
            // 
            this.ProblemText.Location = new System.Drawing.Point(12, 302);
            this.ProblemText.Name = "ProblemText";
            this.ProblemText.Size = new System.Drawing.Size(453, 43);
            this.ProblemText.TabIndex = 4;
            this.ProblemText.Text = "label1";
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 381);
            this.Controls.Add(this.ProblemText);
            this.Controls.Add(this.InputProblem);
            this.Controls.Add(this.AwakePictureBox);
            this.Controls.Add(this.AlarmMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.Text = "Просыпайся!";
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label ProblemText;

        private System.Windows.Forms.TextBox InputProblem;

        #endregion

        private System.Windows.Forms.Label AlarmMessageLabel;
        private System.Windows.Forms.PictureBox AwakePictureBox;
        private System.Windows.Forms.Timer AwakeTimer;
    }
}