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
<<<<<<< Updated upstream
            this.TimerDisplayLabel = new System.Windows.Forms.Label();
            this.StartTimerButton = new System.Windows.Forms.Button();
            this.TimerStopButton = new System.Windows.Forms.Button();
            this.TimerPayzaButton = new System.Windows.Forms.Button();
            this.ResetTimerButton = new System.Windows.Forms.Button();
            this.SetTimerButton = new System.Windows.Forms.Button();
            this.TimerTimer_Tick = new System.Windows.Forms.Timer(this.components);
=======
            this.Labletimer = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.StopwatchTimer = new System.Windows.Forms.Timer(this.components);
>>>>>>> Stashed changes
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.DisplayLabel.Location = new System.Drawing.Point(18, 14);
            this.DisplayLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(484, 125);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "00:00:00";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DisplayLabel.Click += new System.EventHandler(this.DisplayLabel_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(512, 14);
            this.SettingsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(189, 35);
            this.SettingsButton.TabIndex = 1;
            this.SettingsButton.Text = "Настройки...";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(512, 103);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(189, 35);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(512, 58);
            this.AboutButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(189, 35);
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
<<<<<<< Updated upstream
            // TimerDisplayLabel
            // 
            this.TimerDisplayLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TimerDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TimerDisplayLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.TimerDisplayLabel.Location = new System.Drawing.Point(18, 150);
            this.TimerDisplayLabel.Name = "TimerDisplayLabel";
            this.TimerDisplayLabel.Size = new System.Drawing.Size(484, 115);
            this.TimerDisplayLabel.TabIndex = 4;
            this.TimerDisplayLabel.Text = "00:00:00";
            this.TimerDisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TimerDisplayLabel.Click += new System.EventHandler(this.TimerDisplayLabel_Click);
            // 
            // StartTimerButton
            // 
            this.StartTimerButton.BackColor = System.Drawing.SystemColors.Info;
            this.StartTimerButton.Location = new System.Drawing.Point(21, 271);
            this.StartTimerButton.Name = "StartTimerButton";
            this.StartTimerButton.Size = new System.Drawing.Size(189, 34);
            this.StartTimerButton.TabIndex = 5;
            this.StartTimerButton.Text = "Запуск";
            this.StartTimerButton.UseVisualStyleBackColor = false;
            this.StartTimerButton.Click += new System.EventHandler(this.StartTimerButton_Click);
            // 
            // TimerStopButton
            // 
            this.TimerStopButton.BackColor = System.Drawing.SystemColors.Info;
            this.TimerStopButton.Location = new System.Drawing.Point(240, 271);
            this.TimerStopButton.Name = "TimerStopButton";
            this.TimerStopButton.Size = new System.Drawing.Size(189, 34);
            this.TimerStopButton.TabIndex = 6;
            this.TimerStopButton.Text = "Отмена";
            this.TimerStopButton.UseVisualStyleBackColor = false;
            this.TimerStopButton.Click += new System.EventHandler(this.TimerStopButton_Click);
            // 
            // TimerPayzaButton
            // 
            this.TimerPayzaButton.BackColor = System.Drawing.SystemColors.Info;
            this.TimerPayzaButton.Location = new System.Drawing.Point(517, 165);
            this.TimerPayzaButton.Name = "TimerPayzaButton";
            this.TimerPayzaButton.Size = new System.Drawing.Size(189, 34);
            this.TimerPayzaButton.TabIndex = 7;
            this.TimerPayzaButton.Text = "Пауза";
            this.TimerPayzaButton.UseVisualStyleBackColor = false;
            this.TimerPayzaButton.Click += new System.EventHandler(this.TimerPayzaButton_Click);
            // 
            // ResetTimerButton
            // 
            this.ResetTimerButton.BackColor = System.Drawing.SystemColors.Info;
            this.ResetTimerButton.Location = new System.Drawing.Point(517, 219);
            this.ResetTimerButton.Name = "ResetTimerButton";
            this.ResetTimerButton.Size = new System.Drawing.Size(189, 33);
            this.ResetTimerButton.TabIndex = 8;
            this.ResetTimerButton.Text = "Сброс";
            this.ResetTimerButton.UseVisualStyleBackColor = false;
            this.ResetTimerButton.Click += new System.EventHandler(this.ResetTimerButton_Click);
            // 
            // SetTimerButton
            // 
            this.SetTimerButton.BackColor = System.Drawing.SystemColors.Info;
            this.SetTimerButton.Location = new System.Drawing.Point(517, 271);
            this.SetTimerButton.Name = "SetTimerButton";
            this.SetTimerButton.Size = new System.Drawing.Size(189, 33);
            this.SetTimerButton.TabIndex = 9;
            this.SetTimerButton.Text = "Настройки";
            this.SetTimerButton.UseVisualStyleBackColor = false;
            this.SetTimerButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // TimerTimer_Tick
            // 
            this.TimerTimer_Tick.Interval = 1000;
            this.TimerTimer_Tick.Tick += new System.EventHandler(this.TimerTimer_Tick_Tick);
=======
            // Labletimer
            // 
            this.Labletimer.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Labletimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Labletimer.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Labletimer.Location = new System.Drawing.Point(20, 149);
            this.Labletimer.Name = "Labletimer";
            this.Labletimer.Size = new System.Drawing.Size(482, 103);
            this.Labletimer.TabIndex = 4;
            this.Labletimer.Text = "00:00.00";
            this.Labletimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Labletimer.Click += new System.EventHandler(this.Labletimer_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(508, 164);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(189, 38);
            this.StartButton.TabIndex = 5;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(508, 208);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(189, 44);
            this.StopButton.TabIndex = 6;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StopwatchTimer
            // 
            this.StopwatchTimer.Interval = 10;
            this.StopwatchTimer.Tick += new System.EventHandler(this.StopwatchTimer_Tick);
>>>>>>> Stashed changes
            // 
            // AlarmClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< Updated upstream
            this.ClientSize = new System.Drawing.Size(718, 321);
            this.Controls.Add(this.SetTimerButton);
            this.Controls.Add(this.ResetTimerButton);
            this.Controls.Add(this.TimerPayzaButton);
            this.Controls.Add(this.TimerStopButton);
            this.Controls.Add(this.StartTimerButton);
            this.Controls.Add(this.TimerDisplayLabel);
=======
            this.ClientSize = new System.Drawing.Size(718, 274);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.Labletimer);
>>>>>>> Stashed changes
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.DisplayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
<<<<<<< Updated upstream
        private System.Windows.Forms.Label TimerDisplayLabel;
        private System.Windows.Forms.Button StartTimerButton;
        private System.Windows.Forms.Button TimerStopButton;
        private System.Windows.Forms.Button TimerPayzaButton;
        private System.Windows.Forms.Button ResetTimerButton;
        private System.Windows.Forms.Button SetTimerButton;
        private System.Windows.Forms.Timer TimerTimer_Tick;
=======
        private System.Windows.Forms.Label Labletimer;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Timer StopwatchTimer;
>>>>>>> Stashed changes
    }
}

