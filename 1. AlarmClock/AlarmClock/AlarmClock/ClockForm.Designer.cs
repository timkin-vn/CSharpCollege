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
            this.StopwatchStartButton = new System.Windows.Forms.Button();
            this.StopwatchResetButton = new System.Windows.Forms.Button();
            this.StopwatchTimer = new System.Windows.Forms.Timer(this.components);
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
            this.StopwatchLabel.ForeColor = System.Drawing.Color.SkyBlue;
            this.StopwatchLabel.Location = new System.Drawing.Point(12, 105);
            this.StopwatchLabel.Name = "StopwatchLabel";
            this.StopwatchLabel.Size = new System.Drawing.Size(289, 48);
            this.StopwatchLabel.TabIndex = 4;
            this.StopwatchLabel.Text = "00:00:00.000";
            this.StopwatchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StopwatchStartButton
            // 
            this.StopwatchStartButton.Location = new System.Drawing.Point(307, 105);
            this.StopwatchStartButton.Name = "StopwatchStartButton";
            this.StopwatchStartButton.Size = new System.Drawing.Size(100, 23);
            this.StopwatchStartButton.TabIndex = 5;
            this.StopwatchStartButton.Text = "Старт";
            this.StopwatchStartButton.UseVisualStyleBackColor = true;
            this.StopwatchStartButton.Click += new System.EventHandler(this.StopwatchStartButton_Click);
            // 
            // StopwatchResetButton
            // 
            this.StopwatchResetButton.Enabled = false;
            this.StopwatchResetButton.Location = new System.Drawing.Point(307, 130);
            this.StopwatchResetButton.Name = "StopwatchResetButton";
            this.StopwatchResetButton.Size = new System.Drawing.Size(100, 23);
            this.StopwatchResetButton.TabIndex = 6;
            this.StopwatchResetButton.Text = "Сброс";
            this.StopwatchResetButton.UseVisualStyleBackColor = true;
            this.StopwatchResetButton.Click += new System.EventHandler(this.StopwatchResetButton_Click);
            // 
            // StopwatchTimer
            // 
            this.StopwatchTimer.Interval = 50;
            this.StopwatchTimer.Tick += new System.EventHandler(this.StopwatchTimer_Tick);
            // 
            // ClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 165);
            this.Controls.Add(this.StopwatchResetButton);
            this.Controls.Add(this.StopwatchStartButton);
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
        private System.Windows.Forms.Button StopwatchStartButton;
        private System.Windows.Forms.Button StopwatchResetButton;
        private System.Windows.Forms.Timer StopwatchTimer;
    }
}