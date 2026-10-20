namespace AlarmClock.Forms
{
    partial class AwakeForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AlarmMessageLabel = new System.Windows.Forms.Label();
            this.AwakeButton = new System.Windows.Forms.Button();
            this.AwakePictureBox = new System.Windows.Forms.PictureBox();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.SnoozeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AlarmMessageLabel
            // 
            this.AlarmMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.AlarmMessageLabel.Location = new System.Drawing.Point(12, 9);
            this.AlarmMessageLabel.Name = "AlarmMessageLabel";
            this.AlarmMessageLabel.Size = new System.Drawing.Size(584, 38);
            this.AlarmMessageLabel.TabIndex = 0;
            this.AlarmMessageLabel.Text = "Сообщение";
            this.AlarmMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AwakeButton
            // 
            this.AwakeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AwakeButton.Location = new System.Drawing.Point(197, 415);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(118, 23);
            this.AwakeButton.TabIndex = 1;
            this.AwakeButton.Text = "Я проснулся";
            this.AwakeButton.UseVisualStyleBackColor = true;
            // 
            // AwakePictureBox
            // 
            this.AwakePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AwakePictureBox.Location = new System.Drawing.Point(13, 51);
            this.AwakePictureBox.Name = "AwakePictureBox";
            this.AwakePictureBox.Size = new System.Drawing.Size(583, 358);
            this.AwakePictureBox.TabIndex = 2;
            this.AwakePictureBox.TabStop = false;
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);
            // 
            // SnoozeButton
            // 
            this.SnoozeButton.Location = new System.Drawing.Point(321, 415);
            this.SnoozeButton.Name = "SnoozeButton";
            this.SnoozeButton.Size = new System.Drawing.Size(140, 23);
            this.SnoozeButton.TabIndex = 3;
            this.SnoozeButton.Text = "Отложить";
            this.SnoozeButton.UseVisualStyleBackColor = true;
            this.SnoozeButton.Click += new System.EventHandler(this.SnoozeButton_Click);
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 450);
            this.Controls.Add(this.SnoozeButton);
            this.Controls.Add(this.AwakePictureBox);
            this.Controls.Add(this.AwakeButton);
            this.Controls.Add(this.AlarmMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.Text = "Просыпайся!";
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label AlarmMessageLabel;
        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.PictureBox AwakePictureBox;
        private System.Windows.Forms.Timer AwakeTimer;
        private System.Windows.Forms.Button SnoozeButton;
    }
}
