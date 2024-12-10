namespace AlarmClock
{
    partial class ClockForm
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
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.AboutButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.startTimerButton = new System.Windows.Forms.Button();
            this.stopwatchLabel = new System.Windows.Forms.Label();
            this.startStopwatchButton = new System.Windows.Forms.Button();
            this.stopStopwatchButton = new System.Windows.Forms.Button();
            this.stopwatchTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Tahoma", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.DisplayLabel.Location = new System.Drawing.Point(2, -3);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(294, 80);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "00:00:00";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(312, 12);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(117, 24);
            this.AboutButton.TabIndex = 1;
            this.AboutButton.Text = "О программе";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(312, 42);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(117, 23);
            this.SettingsButton.TabIndex = 2;
            this.SettingsButton.Text = "Настройки";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(312, 72);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(117, 23);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ClockTimer
            // 
            this.ClockTimer.Enabled = true;
            this.ClockTimer.Interval = 1000;
            this.ClockTimer.Tick += new System.EventHandler(this.ClockTimer_Tick);
            // 
            // startTimerButton
            // 
            this.startTimerButton.Location = new System.Drawing.Point(312, 101);
            this.startTimerButton.Name = "startTimerButton";
            this.startTimerButton.Size = new System.Drawing.Size(117, 27);
            this.startTimerButton.TabIndex = 0;
            this.startTimerButton.Text = "Start Timer";
            this.startTimerButton.UseVisualStyleBackColor = true;
            this.startTimerButton.Click += new System.EventHandler(this.StartTimerButton_Click);
            // 
            // stopwatchLabel
            // 
            this.stopwatchLabel.AutoSize = true;
            this.stopwatchLabel.Location = new System.Drawing.Point(104, 149);
            this.stopwatchLabel.Name = "stopwatchLabel";
            this.stopwatchLabel.Size = new System.Drawing.Size(49, 13);
            this.stopwatchLabel.TabIndex = 4;
            this.stopwatchLabel.Text = "00:00:00";
            this.stopwatchLabel.Click += new System.EventHandler(this.stopwatchLabel_Click);
            // 
            // startStopwatchButton
            // 
            this.startStopwatchButton.Location = new System.Drawing.Point(312, 134);
            this.startStopwatchButton.Name = "startStopwatchButton";
            this.startStopwatchButton.Size = new System.Drawing.Size(117, 28);
            this.startStopwatchButton.TabIndex = 5;
            this.startStopwatchButton.Text = "Start Stopwatch";
            this.startStopwatchButton.UseVisualStyleBackColor = true;
            this.startStopwatchButton.Click += new System.EventHandler(this.StartStopwatchButton_Click);
            // 
            // stopStopwatchButton
            // 
            this.stopStopwatchButton.Location = new System.Drawing.Point(312, 168);
            this.stopStopwatchButton.Name = "stopStopwatchButton";
            this.stopStopwatchButton.Size = new System.Drawing.Size(117, 25);
            this.stopStopwatchButton.TabIndex = 6;
            this.stopStopwatchButton.Text = "Stop Stopwatch";
            this.stopStopwatchButton.UseVisualStyleBackColor = true;
            this.stopStopwatchButton.Click += new System.EventHandler(this.StopStopwatchButton_Click);
            // 
            // stopwatchTimer
            // 
            this.stopwatchTimer.Interval = 1000;
            this.stopwatchTimer.Tick += new System.EventHandler(this.StopwatchTimer_Tick);
            // 
            // ClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 248);
            this.Controls.Add(this.stopStopwatchButton);
            this.Controls.Add(this.startStopwatchButton);
            this.Controls.Add(this.stopwatchLabel);
            this.Controls.Add(this.startTimerButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.DisplayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClockForm";
            this.Text = "Будильник";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.Button startTimerButton;
        private System.Windows.Forms.Label stopwatchLabel; // Добавлено
        private System.Windows.Forms.Button startStopwatchButton; // Добавлено
        private System.Windows.Forms.Button stopStopwatchButton; // Добавлено
        private System.Windows.Forms.Timer stopwatchTimer; // Добавлено
    }
}
