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
            this.AwakePictureBox = new System.Windows.Forms.PictureBox();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.Snooze5Button = new System.Windows.Forms.Button();
            this.Snooze10Button = new System.Windows.Forms.Button();
            this.Snooze30Button = new System.Windows.Forms.Button();
            this.Snooze60Button = new System.Windows.Forms.Button();
            this.AwakeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).BeginInit();
            this.SuspendLayout();
            this.AwakePictureBox.Location = new System.Drawing.Point(12, 12);
            this.AwakePictureBox.Name = "AwakePictureBox";
            this.AwakePictureBox.Size = new System.Drawing.Size(573, 333);
            this.AwakePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AwakePictureBox.TabIndex = 0;
            this.AwakePictureBox.TabStop = false;
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);
            this.Snooze5Button.Location = new System.Drawing.Point(12, 351);
            this.Snooze5Button.Name = "Snooze5Button";
            this.Snooze5Button.Size = new System.Drawing.Size(75, 30);
            this.Snooze5Button.TabIndex = 1;
            this.Snooze5Button.Text = "5 мин";
            this.Snooze5Button.UseVisualStyleBackColor = true;
            this.Snooze5Button.Click += new System.EventHandler(this.Snooze5Button_Click);
            this.Snooze10Button.Location = new System.Drawing.Point(93, 351);
            this.Snooze10Button.Name = "Snooze10Button";
            this.Snooze10Button.Size = new System.Drawing.Size(75, 30);
            this.Snooze10Button.TabIndex = 2;
            this.Snooze10Button.Text = "10 мин";
            this.Snooze10Button.UseVisualStyleBackColor = true;
            this.Snooze10Button.Click += new System.EventHandler(this.Snooze10Button_Click); 
            this.Snooze30Button.Location = new System.Drawing.Point(174, 351);
            this.Snooze30Button.Name = "Snooze30Button";
            this.Snooze30Button.Size = new System.Drawing.Size(75, 30);
            this.Snooze30Button.TabIndex = 3;
            this.Snooze30Button.Text = "30 мин";
            this.Snooze30Button.UseVisualStyleBackColor = true;
            this.Snooze30Button.Click += new System.EventHandler(this.Snooze30Button_Click);
            this.Snooze60Button.Location = new System.Drawing.Point(255, 351);
            this.Snooze60Button.Name = "Snooze60Button";
            this.Snooze60Button.Size = new System.Drawing.Size(75, 30);
            this.Snooze60Button.TabIndex = 4;
            this.Snooze60Button.Text = "60 мин";
            this.Snooze60Button.UseVisualStyleBackColor = true;
            this.Snooze60Button.Click += new System.EventHandler(this.Snooze60Button_Click);
            this.AwakeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AwakeButton.Location = new System.Drawing.Point(336, 351);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(100, 30);
            this.AwakeButton.TabIndex = 5;
            this.AwakeButton.Text = "Я проснулся";
            this.AwakeButton.UseVisualStyleBackColor = true;
            this.AwakeButton.Click += new System.EventHandler(this.AwakeButton_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 393);
            this.Controls.Add(this.AwakeButton);
            this.Controls.Add(this.Snooze60Button);
            this.Controls.Add(this.Snooze30Button);
            this.Controls.Add(this.Snooze10Button);
            this.Controls.Add(this.Snooze5Button);
            this.Controls.Add(this.AwakePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AwakeForm_FormClosed);
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox AwakePictureBox;
        private System.Windows.Forms.Timer AwakeTimer;
        private System.Windows.Forms.Button Snooze5Button;
        private System.Windows.Forms.Button Snooze10Button;
        private System.Windows.Forms.Button Snooze30Button;
        private System.Windows.Forms.Button Snooze60Button;
        private System.Windows.Forms.Button AwakeButton;
    }
}