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
            this.CountdownMinutesNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.StartCountdownButton = new System.Windows.Forms.Button();
            this.StopCountdownButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CountdownMinutesNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.BlueViolet;
            this.DisplayLabel.Location = new System.Drawing.Point(24, 17);
            this.DisplayLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(656, 160);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "00:00:00";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DisplayLabel.Click += new System.EventHandler(this.DisplayLabel_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(706, 17);
            this.SettingsButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(216, 44);
            this.SettingsButton.TabIndex = 1;
            this.SettingsButton.Text = "Настройки...";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(706, 75);
            this.AboutButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(216, 44);
            this.AboutButton.TabIndex = 2;
            this.AboutButton.Text = "О программе...";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(706, 133);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(216, 44);
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
            // CountdownMinutesNumeric
            // 
            this.CountdownMinutesNumeric.Location = new System.Drawing.Point(179, 207);
            this.CountdownMinutesNumeric.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.CountdownMinutesNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CountdownMinutesNumeric.Name = "CountdownMinutesNumeric";
            this.CountdownMinutesNumeric.Size = new System.Drawing.Size(88, 31);
            this.CountdownMinutesNumeric.TabIndex = 4;
            this.CountdownMinutesNumeric.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Минут: ";
            // 
            // StartCountdownButton
            // 
            this.StartCountdownButton.Location = new System.Drawing.Point(27, 264);
            this.StartCountdownButton.Name = "StartCountdownButton";
            this.StartCountdownButton.Size = new System.Drawing.Size(99, 42);
            this.StartCountdownButton.TabIndex = 6;
            this.StartCountdownButton.Text = "Старт";
            this.StartCountdownButton.UseVisualStyleBackColor = true;
            this.StartCountdownButton.Click += new System.EventHandler(this.StartCountdownButton_Click);
            // 
            // StopCountdownButton
            // 
            this.StopCountdownButton.Location = new System.Drawing.Point(179, 265);
            this.StopCountdownButton.Name = "StopCountdownButton";
            this.StopCountdownButton.Size = new System.Drawing.Size(88, 41);
            this.StopCountdownButton.TabIndex = 7;
            this.StopCountdownButton.Text = "Стоп";
            this.StopCountdownButton.UseVisualStyleBackColor = true;
            this.StopCountdownButton.Click += new System.EventHandler(this.StopCountdownButton_Click);
            // 
            // ClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 321);
            this.Controls.Add(this.StopCountdownButton);
            this.Controls.Add(this.StartCountdownButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CountdownMinutesNumeric);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.DisplayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.Name = "ClockForm";
            this.Text = "Будильник";
            this.Load += new System.EventHandler(this.ClockForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CountdownMinutesNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.NumericUpDown CountdownMinutesNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartCountdownButton;
        private System.Windows.Forms.Button StopCountdownButton;
    }
}

