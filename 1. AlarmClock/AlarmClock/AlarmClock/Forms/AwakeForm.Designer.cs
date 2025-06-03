namespace AlarmClock.Forms
{
    partial class AwakeForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label AwakeMessage;
        private System.Windows.Forms.PictureBox AwakePictureBox;
        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.Timer AwakeTimer;
        private System.Windows.Forms.Label StopwatchLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AwakeMessage = new System.Windows.Forms.Label();
            this.AwakePictureBox = new System.Windows.Forms.PictureBox();
            this.AwakeButton = new System.Windows.Forms.Button();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.StopwatchLabel = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).BeginInit();
            this.SuspendLayout();

            // AwakeMessage
            this.AwakeMessage.AutoSize = true;
            this.AwakeMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.AwakeMessage.Location = new System.Drawing.Point(20, 20);
            this.AwakeMessage.Name = "AwakeMessage";
            this.AwakeMessage.Size = new System.Drawing.Size(200, 24);
            this.AwakeMessage.Text = "Сообщение будильника";

            // AwakePictureBox
            this.AwakePictureBox.Location = new System.Drawing.Point(20, 60);
            this.AwakePictureBox.Name = "AwakePictureBox";
            this.AwakePictureBox.Size = new System.Drawing.Size(200, 150);
            this.AwakePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // StopwatchLabel
            this.StopwatchLabel.AutoSize = true;
            this.StopwatchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.StopwatchLabel.Location = new System.Drawing.Point(20, 220);
            this.StopwatchLabel.Name = "StopwatchLabel";
            this.StopwatchLabel.Size = new System.Drawing.Size(200, 20);
            this.StopwatchLabel.Text = "Прошло времени: 00:00:00";

            // AwakeButton
            this.AwakeButton.Location = new System.Drawing.Point(20, 260);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(200, 40);
            this.AwakeButton.Text = "Проснуться";
            this.AwakeButton.UseVisualStyleBackColor = true;
            this.AwakeButton.Click += new System.EventHandler(this.AwakeButton_Click);

            // AwakeTimer
            this.AwakeTimer.Interval = 3000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);

            // AwakeForm
            this.ClientSize = new System.Drawing.Size(250, 320);
            this.Controls.Add(this.AwakeMessage);
            this.Controls.Add(this.AwakePictureBox);
            this.Controls.Add(this.StopwatchLabel);
            this.Controls.Add(this.AwakeButton);
            this.Name = "AwakeForm";
            this.Text = "Будильник сработал!";
            this.Load += new System.EventHandler(this.AwakeForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
