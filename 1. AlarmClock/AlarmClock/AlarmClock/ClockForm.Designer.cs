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
            this.SettingsButton = new System.Windows.Forms.Button();
            this.AboutButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.StopwatchLabel = new System.Windows.Forms.Label();
            this.StartStopwatchButton = new System.Windows.Forms.Button();
            this.ResetStopwatchButton = new System.Windows.Forms.Button();
            this.stopwatchTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.SkyBlue;
            this.DisplayLabel.Location = new System.Drawing.Point(12, 9);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(289, 83);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "00:00:00";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(307, 8);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(100, 23);
            this.SettingsButton.TabIndex = 1;
            this.SettingsButton.Text = "Настройки...";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(307, 38);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(100, 23);
            this.AboutButton.TabIndex = 2;
            this.AboutButton.Text = "О программе...";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(307, 68);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(100, 23);
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
            // StopwatchLabel
            // 
            this.StopwatchLabel.BackColor = System.Drawing.Color.Black;
            this.StopwatchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopwatchLabel.ForeColor = System.Drawing.Color.LightGreen;
            this.StopwatchLabel.Location = new System.Drawing.Point(12, 95);
            this.StopwatchLabel.Name = "StopwatchLabel";
            this.StopwatchLabel.Size = new System.Drawing.Size(289, 40);
            this.StopwatchLabel.TabIndex = 4;
            this.StopwatchLabel.Text = "00:00:00";
            this.StopwatchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartStopwatchButton
            // 
            this.StartStopwatchButton.Location = new System.Drawing.Point(307, 95);
            this.StartStopwatchButton.Name = "StartStopwatchButton";
            this.StartStopwatchButton.Size = new System.Drawing.Size(100, 23);
            this.StartStopwatchButton.TabIndex = 5;
            this.StartStopwatchButton.Text = "Старт";
            this.StartStopwatchButton.UseVisualStyleBackColor = true;
            this.StartStopwatchButton.Click += new System.EventHandler(this.StartStopwatchButton_Click);
            // 
            // ResetStopwatchButton
            // 
            this.ResetStopwatchButton.Location = new System.Drawing.Point(307, 124);
            this.ResetStopwatchButton.Name = "ResetStopwatchButton";
            this.ResetStopwatchButton.Size = new System.Drawing.Size(100, 23);
            this.ResetStopwatchButton.TabIndex = 6;
            this.ResetStopwatchButton.Text = "Сброс";
            this.ResetStopwatchButton.UseVisualStyleBackColor = true;
            this.ResetStopwatchButton.Click += new System.EventHandler(this.ResetStopwatchButton_Click);
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
            this.ClientSize = new System.Drawing.Size(419, 160);
            this.Controls.Add(this.ResetStopwatchButton);
            this.Controls.Add(this.StartStopwatchButton);
            this.Controls.Add(this.StopwatchLabel);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.DisplayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ClockForm";
            this.Text = "Будильник";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.Label StopwatchLabel;
        private System.Windows.Forms.Button StartStopwatchButton;
        private System.Windows.Forms.Button ResetStopwatchButton;
        private System.Windows.Forms.Timer stopwatchTimer;
    }
}