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
            this.AwakeMessageLabel = new System.Windows.Forms.Label();
            this.AwakePictureBox = new System.Windows.Forms.PictureBox();
            this.AwakeButton = new System.Windows.Forms.Button();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AwakeMessageLabel
            // 
            this.AwakeMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AwakeMessageLabel.Location = new System.Drawing.Point(12, 13);
            this.AwakeMessageLabel.Name = "AwakeMessageLabel";
            this.AwakeMessageLabel.Size = new System.Drawing.Size(621, 74);
            this.AwakeMessageLabel.TabIndex = 0;
            this.AwakeMessageLabel.Text = "Сообщение";
            this.AwakeMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AwakePictureBox
            // 
            this.AwakePictureBox.Location = new System.Drawing.Point(12, 90);
            this.AwakePictureBox.Name = "AwakePictureBox";
            this.AwakePictureBox.Size = new System.Drawing.Size(621, 428);
            this.AwakePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.AwakePictureBox.TabIndex = 1;
            this.AwakePictureBox.TabStop = false;
            // 
            // AwakeButton
            // 
            this.AwakeButton.Location = new System.Drawing.Point(277, 524);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(93, 23);
            this.AwakeButton.TabIndex = 2;
            this.AwakeButton.Text = "Я проснулся";
            this.AwakeButton.UseVisualStyleBackColor = true;
            this.AwakeButton.Click += new System.EventHandler(this.AwakeButton_Click);
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 559);
            this.ControlBox = false;
            this.Controls.Add(this.AwakeButton);
            this.Controls.Add(this.AwakePictureBox);
            this.Controls.Add(this.AwakeMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.Text = "Просыпайся!";
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label AwakeMessageLabel;
        private System.Windows.Forms.PictureBox AwakePictureBox;
        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.Timer AwakeTimer;
    }
}