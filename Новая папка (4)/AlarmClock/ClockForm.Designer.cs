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
            this.grpStopwatch = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblStopwatchTime = new System.Windows.Forms.Label();
            this.grpStopwatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.DisplayLabel.Location = new System.Drawing.Point(13, 13);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(293, 83);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "00:00:00";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(312, 12);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(117, 23);
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
            // grpStopwatch
            // 
            this.grpStopwatch.Controls.Add(this.btnReset);
            this.grpStopwatch.Controls.Add(this.btnStop);
            this.grpStopwatch.Controls.Add(this.btnStart);
            this.grpStopwatch.Controls.Add(this.lblStopwatchTime);
            this.grpStopwatch.Location = new System.Drawing.Point(464, 8);
            this.grpStopwatch.Name = "grpStopwatch";
            this.grpStopwatch.Size = new System.Drawing.Size(200, 150);
            this.grpStopwatch.TabIndex = 4;
            this.grpStopwatch.TabStop = false;
            this.grpStopwatch.Text = "секундомер";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(20, 95);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(150, 25);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Сброс";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(100, 60);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(70, 25);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(20, 60);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(70, 25);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // lblStopwatchTime
            // 
            this.lblStopwatchTime.AutoSize = true;
            this.lblStopwatchTime.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblStopwatchTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStopwatchTime.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStopwatchTime.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblStopwatchTime.Location = new System.Drawing.Point(20, 20);
            this.lblStopwatchTime.Name = "lblStopwatchTime";
            this.lblStopwatchTime.Size = new System.Drawing.Size(92, 24);
            this.lblStopwatchTime.TabIndex = 0;
            this.lblStopwatchTime.Text = "00:00:00";
            this.lblStopwatchTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStopwatchTime.Click += new System.EventHandler(this.lblStopwatchTime_Click);
            // 
            // ClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 164);
            this.Controls.Add(this.grpStopwatch);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.DisplayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClockForm";
            this.Text = "Будильник";
            this.grpStopwatch.ResumeLayout(false);
            this.grpStopwatch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.GroupBox grpStopwatch;
        private System.Windows.Forms.Label lblStopwatchTime;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
    }
}

