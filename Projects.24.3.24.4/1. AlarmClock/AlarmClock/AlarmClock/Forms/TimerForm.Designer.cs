namespace AlarmClock.Forms
{
    partial class TimerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
            {
                if (_countdownTimer != null)
                    _countdownTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TitleLabel = new System.Windows.Forms.Label();
            this.HoursUpDown = new System.Windows.Forms.NumericUpDown();
            this.MinutesUpDown = new System.Windows.Forms.NumericUpDown();
            this.SecondsUpDown = new System.Windows.Forms.NumericUpDown();
            this.HoursLabel = new System.Windows.Forms.Label();
            this.MinutesLabel = new System.Windows.Forms.Label();
            this.SecondsLabel = new System.Windows.Forms.Label();
            this.IsSoundCheckBox = new System.Windows.Forms.CheckBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.CountdownLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.HoursUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinutesUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsUpDown)).BeginInit();
            this.SuspendLayout();
            //
            // TitleLabel
            //
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(270, 24);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Установите время";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // HoursUpDown
            //
            this.HoursUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HoursUpDown.Location = new System.Drawing.Point(25, 40);
            this.HoursUpDown.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            this.HoursUpDown.Name = "HoursUpDown";
            this.HoursUpDown.Size = new System.Drawing.Size(70, 38);
            this.HoursUpDown.TabIndex = 1;
            this.HoursUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // MinutesUpDown
            //
            this.MinutesUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinutesUpDown.Location = new System.Drawing.Point(119, 40);
            this.MinutesUpDown.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            this.MinutesUpDown.Name = "MinutesUpDown";
            this.MinutesUpDown.Size = new System.Drawing.Size(70, 38);
            this.MinutesUpDown.TabIndex = 2;
            this.MinutesUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // SecondsUpDown
            //
            this.SecondsUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SecondsUpDown.Location = new System.Drawing.Point(213, 40);
            this.SecondsUpDown.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            this.SecondsUpDown.Name = "SecondsUpDown";
            this.SecondsUpDown.Size = new System.Drawing.Size(70, 38);
            this.SecondsUpDown.TabIndex = 3;
            this.SecondsUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // HoursLabel
            //
            this.HoursLabel.AutoSize = true;
            this.HoursLabel.Location = new System.Drawing.Point(38, 81);
            this.HoursLabel.Name = "HoursLabel";
            this.HoursLabel.Size = new System.Drawing.Size(41, 13);
            this.HoursLabel.TabIndex = 4;
            this.HoursLabel.Text = "Часы";
            //
            // MinutesLabel
            //
            this.MinutesLabel.AutoSize = true;
            this.MinutesLabel.Location = new System.Drawing.Point(128, 81);
            this.MinutesLabel.Name = "MinutesLabel";
            this.MinutesLabel.Size = new System.Drawing.Size(44, 13);
            this.MinutesLabel.TabIndex = 5;
            this.MinutesLabel.Text = "Минуты";
            //
            // SecondsLabel
            //
            this.SecondsLabel.AutoSize = true;
            this.SecondsLabel.Location = new System.Drawing.Point(220, 81);
            this.SecondsLabel.Name = "SecondsLabel";
            this.SecondsLabel.Size = new System.Drawing.Size(52, 13);
            this.SecondsLabel.TabIndex = 6;
            this.SecondsLabel.Text = "Секунды";
            //
            // IsSoundCheckBox
            //
            this.IsSoundCheckBox.AutoSize = true;
            this.IsSoundCheckBox.Checked = true;
            this.IsSoundCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsSoundCheckBox.Location = new System.Drawing.Point(66, 100);
            this.IsSoundCheckBox.Name = "IsSoundCheckBox";
            this.IsSoundCheckBox.Size = new System.Drawing.Size(158, 17);
            this.IsSoundCheckBox.TabIndex = 7;
            this.IsSoundCheckBox.Text = "Звуковой сигнал включен";
            this.IsSoundCheckBox.UseVisualStyleBackColor = true;
            //
            // StartButton
            //
            this.StartButton.Location = new System.Drawing.Point(78, 125);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 8;
            this.StartButton.Text = "Старт";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            //
            // CountdownLabel
            //
            this.CountdownLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.CountdownLabel.ForeColor = System.Drawing.Color.OrangeRed;
            this.CountdownLabel.Location = new System.Drawing.Point(12, 40);
            this.CountdownLabel.Name = "CountdownLabel";
            this.CountdownLabel.Size = new System.Drawing.Size(270, 60);
            this.CountdownLabel.TabIndex = 10;
            this.CountdownLabel.Text = "00:00:00";
            this.CountdownLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CountdownLabel.Visible = false;
            //
            // StopButton
            //
            this.StopButton.Location = new System.Drawing.Point(119, 110);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 30);
            this.StopButton.TabIndex = 9;
            this.StopButton.Text = "Стоп";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Visible = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            //
            // TimerForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 155);
            this.Controls.Add(this.CountdownLabel);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.IsSoundCheckBox);
            this.Controls.Add(this.SecondsLabel);
            this.Controls.Add(this.MinutesLabel);
            this.Controls.Add(this.HoursLabel);
            this.Controls.Add(this.SecondsUpDown);
            this.Controls.Add(this.MinutesUpDown);
            this.Controls.Add(this.HoursUpDown);
            this.Controls.Add(this.TitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimerForm";
            this.Text = "Таймер";
            this.Load += new System.EventHandler(this.TimerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HoursUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinutesUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.NumericUpDown HoursUpDown;
        private System.Windows.Forms.NumericUpDown MinutesUpDown;
        private System.Windows.Forms.NumericUpDown SecondsUpDown;
        private System.Windows.Forms.Label HoursLabel;
        private System.Windows.Forms.Label MinutesLabel;
        private System.Windows.Forms.Label SecondsLabel;
        private System.Windows.Forms.CheckBox IsSoundCheckBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label CountdownLabel;
        private System.Windows.Forms.Label TitleLabel;
    }
}
