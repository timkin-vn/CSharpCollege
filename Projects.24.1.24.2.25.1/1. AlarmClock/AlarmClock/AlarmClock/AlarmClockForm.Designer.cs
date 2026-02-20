namespace AlarmClock
{
    partial class AlarmClockForm
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
            this.SettingsButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.AboutButton = new System.Windows.Forms.Button();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.SecondsLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.timerSeconds = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.DisplayLabel.Location = new System.Drawing.Point(12, 9);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(323, 81);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "00:00:00";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(341, 9);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(126, 23);
            this.SettingsButton.TabIndex = 1;
            this.SettingsButton.Text = "Настройки...";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(341, 67);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(126, 23);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(341, 38);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(126, 23);
            this.AboutButton.TabIndex = 3;
            this.AboutButton.Text = "О программе...";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // ClockTimer
            // 
            this.ClockTimer.Enabled = true;
            this.ClockTimer.Interval = 1000;
            this.ClockTimer.Tick += new System.EventHandler(this.ClockTimer_Tick);
            // 
            // SecondsLabel
            // 
            this.SecondsLabel.BackColor = System.Drawing.Color.Black;
            this.SecondsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SecondsLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.SecondsLabel.Location = new System.Drawing.Point(12, 126);
            this.SecondsLabel.Name = "SecondsLabel";
            this.SecondsLabel.Size = new System.Drawing.Size(323, 81);
            this.SecondsLabel.TabIndex = 4;
            this.SecondsLabel.Text = "00:00:00";
            this.SecondsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(341, 126);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(126, 23);
            this.StartButton.TabIndex = 5;
            this.StartButton.Text = "Запустить";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(341, 155);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(126, 23);
            this.StopButton.TabIndex = 6;
            this.StopButton.Text = "Остановить";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(341, 184);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(126, 23);
            this.ResetButton.TabIndex = 7;
            this.ResetButton.Text = "Сброс";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // timerSeconds
            // 
            this.timerSeconds.Interval = 1000;
            this.timerSeconds.Tick += new System.EventHandler(this.timerSeconds_Tick);
            // 
            // AlarmClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 218);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.SecondsLabel);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.DisplayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AlarmClockForm";
            this.Text = "Будильник";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.Label SecondsLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Timer timerSeconds;
    }
}

