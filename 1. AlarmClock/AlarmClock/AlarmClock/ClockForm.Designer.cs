namespace AlarmClock
{
    partial class ClockForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label DisplayLabel;

        private System.Windows.Forms.Label StopwatchLabel;
        private System.Windows.Forms.Button StartStopwatchButton;
        private System.Windows.Forms.Button StopStopwatchButton;
        private System.Windows.Forms.Button ResetStopwatchButton;

        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button AboutButton;

        private System.Windows.Forms.Timer ClockTimer;

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

            this.DisplayLabel = new System.Windows.Forms.Label();

            this.StopwatchLabel = new System.Windows.Forms.Label();
            this.StartStopwatchButton = new System.Windows.Forms.Button();
            this.StopStopwatchButton = new System.Windows.Forms.Button();
            this.ResetStopwatchButton = new System.Windows.Forms.Button();

            this.ExitButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.AboutButton = new System.Windows.Forms.Button();

            this.ClockTimer = new System.Windows.Forms.Timer(this.components);

            this.SuspendLayout();

            // DisplayLabel
            this.DisplayLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.DisplayLabel.Location = new System.Drawing.Point(20, 20);
            this.DisplayLabel.Size = new System.Drawing.Size(300, 50);
            this.DisplayLabel.Text = "00:00:00";

            // StopwatchLabel
            this.StopwatchLabel.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.StopwatchLabel.Location = new System.Drawing.Point(20, 90);
            this.StopwatchLabel.Size = new System.Drawing.Size(200, 40);
            this.StopwatchLabel.Text = "00:00:00";

            // Stopwatch Buttons
            this.StartStopwatchButton.Location = new System.Drawing.Point(230, 90);
            this.StartStopwatchButton.Size = new System.Drawing.Size(75, 30);
            this.StartStopwatchButton.Text = "Старт";
            this.StartStopwatchButton.Click += new System.EventHandler(this.StartStopwatchButton_Click);

            this.StopStopwatchButton.Location = new System.Drawing.Point(310, 90);
            this.StopStopwatchButton.Size = new System.Drawing.Size(75, 30);
            this.StopStopwatchButton.Text = "Стоп";
            this.StopStopwatchButton.Click += new System.EventHandler(this.StopStopwatchButton_Click);

            this.ResetStopwatchButton.Location = new System.Drawing.Point(390, 90);
            this.ResetStopwatchButton.Size = new System.Drawing.Size(75, 30);
            this.ResetStopwatchButton.Text = "Сброс";
            this.ResetStopwatchButton.Click += new System.EventHandler(this.ResetStopwatchButton_Click);

            // Settings Button
            this.SettingsButton.Location = new System.Drawing.Point(20, 150);
            this.SettingsButton.Size = new System.Drawing.Size(100, 30);
            this.SettingsButton.Text = "Настройки";
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);

            // About Button
            this.AboutButton.Location = new System.Drawing.Point(130, 150);
            this.AboutButton.Size = new System.Drawing.Size(100, 30);
            this.AboutButton.Text = "О программе";
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);

            // Exit Button
            this.ExitButton.Location = new System.Drawing.Point(390, 150);
            this.ExitButton.Size = new System.Drawing.Size(75, 30);
            this.ExitButton.Text = "Выход";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);

            // ClockTimer
            this.ClockTimer.Interval = 1000;
            this.ClockTimer.Tick += new System.EventHandler(this.ClockTimer_Tick);
            this.ClockTimer.Start();

            // ClockForm
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.StopwatchLabel);
            this.Controls.Add(this.StartStopwatchButton);
            this.Controls.Add(this.StopStopwatchButton);
            this.Controls.Add(this.ResetStopwatchButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.ExitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ClockForm";
            this.Text = "Будильник";
            this.ResumeLayout(false);
        }
    }
}
