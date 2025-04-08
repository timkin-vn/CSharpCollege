namespace AlarmClock.Forms
{
    partial class CoutdownTimerForm
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
            this.CoutdownTimerTextBoxHours = new System.Windows.Forms.TextBox();
            this.CoutdownTimerTextBoxMinutes = new System.Windows.Forms.TextBox();
            this.CoutdownTimerTextBoxSeconds = new System.Windows.Forms.TextBox();
            this.CoutdownTimerLabel = new System.Windows.Forms.Label();
            this.StopwatchTimerTimeLabel = new System.Windows.Forms.Label();
            this.CoutndownTimerStartButton = new System.Windows.Forms.Button();
            this.CoutdownTimerTimer = new System.Windows.Forms.Timer(this.components);
            this.CoutdownTimerTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CoutdownTimerTextBoxHours
            // 
            this.CoutdownTimerTextBoxHours.Location = new System.Drawing.Point(256, 265);
            this.CoutdownTimerTextBoxHours.Name = "CoutdownTimerTextBoxHours";
            this.CoutdownTimerTextBoxHours.Size = new System.Drawing.Size(38, 20);
            this.CoutdownTimerTextBoxHours.TabIndex = 0;
            // 
            // CoutdownTimerTextBoxMinutes
            // 
            this.CoutdownTimerTextBoxMinutes.Location = new System.Drawing.Point(300, 265);
            this.CoutdownTimerTextBoxMinutes.Name = "CoutdownTimerTextBoxMinutes";
            this.CoutdownTimerTextBoxMinutes.Size = new System.Drawing.Size(38, 20);
            this.CoutdownTimerTextBoxMinutes.TabIndex = 1;
            // 
            // CoutdownTimerTextBoxSeconds
            // 
            this.CoutdownTimerTextBoxSeconds.Location = new System.Drawing.Point(344, 265);
            this.CoutdownTimerTextBoxSeconds.Name = "CoutdownTimerTextBoxSeconds";
            this.CoutdownTimerTextBoxSeconds.Size = new System.Drawing.Size(38, 20);
            this.CoutdownTimerTextBoxSeconds.TabIndex = 2;
            // 
            // CoutdownTimerLabel
            // 
            this.CoutdownTimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CoutdownTimerLabel.Location = new System.Drawing.Point(113, 9);
            this.CoutdownTimerLabel.Name = "CoutdownTimerLabel";
            this.CoutdownTimerLabel.Size = new System.Drawing.Size(142, 44);
            this.CoutdownTimerLabel.TabIndex = 3;
            this.CoutdownTimerLabel.Text = "Таймер";
            // 
            // StopwatchTimerTimeLabel
            // 
            this.StopwatchTimerTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopwatchTimerTimeLabel.Location = new System.Drawing.Point(3, 264);
            this.StopwatchTimerTimeLabel.Name = "StopwatchTimerTimeLabel";
            this.StopwatchTimerTimeLabel.Size = new System.Drawing.Size(252, 21);
            this.StopwatchTimerTimeLabel.TabIndex = 4;
            this.StopwatchTimerTimeLabel.Text = "Введите время [часы] [минуты] [секунды]\r\n\r\n";
            // 
            // CoutndownTimerStartButton
            // 
            this.CoutndownTimerStartButton.Location = new System.Drawing.Point(128, 328);
            this.CoutndownTimerStartButton.Name = "CoutndownTimerStartButton";
            this.CoutndownTimerStartButton.Size = new System.Drawing.Size(127, 44);
            this.CoutndownTimerStartButton.TabIndex = 5;
            this.CoutndownTimerStartButton.Text = "Старт";
            this.CoutndownTimerStartButton.UseVisualStyleBackColor = true;
            this.CoutndownTimerStartButton.Click += new System.EventHandler(this.CoutndownTimerStartButton_Click);
            // 
            // CoutdownTimerTimer
            // 
            this.CoutdownTimerTimer.Interval = 1000;
            this.CoutdownTimerTimer.Tick += new System.EventHandler(this.CoutdownTimerTimer_Tick);
            // 
            // CoutdownTimerTimeLabel
            // 
            this.CoutdownTimerTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CoutdownTimerTimeLabel.Location = new System.Drawing.Point(63, 116);
            this.CoutdownTimerTimeLabel.Name = "CoutdownTimerTimeLabel";
            this.CoutdownTimerTimeLabel.Size = new System.Drawing.Size(298, 69);
            this.CoutdownTimerTimeLabel.TabIndex = 6;
            this.CoutdownTimerTimeLabel.Text = "00:00:00";
            // 
            // CoutdownTimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 400);
            this.Controls.Add(this.CoutdownTimerTimeLabel);
            this.Controls.Add(this.CoutndownTimerStartButton);
            this.Controls.Add(this.StopwatchTimerTimeLabel);
            this.Controls.Add(this.CoutdownTimerLabel);
            this.Controls.Add(this.CoutdownTimerTextBoxSeconds);
            this.Controls.Add(this.CoutdownTimerTextBoxMinutes);
            this.Controls.Add(this.CoutdownTimerTextBoxHours);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CoutdownTimerForm";
            this.Text = "Таймер";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CoutdownTimerTextBoxHours;
        private System.Windows.Forms.TextBox CoutdownTimerTextBoxMinutes;
        private System.Windows.Forms.TextBox CoutdownTimerTextBoxSeconds;
        private System.Windows.Forms.Label CoutdownTimerLabel;
        private System.Windows.Forms.Label StopwatchTimerTimeLabel;
        private System.Windows.Forms.Button CoutndownTimerStartButton;
        private System.Windows.Forms.Timer CoutdownTimerTimer;
        private System.Windows.Forms.Label CoutdownTimerTimeLabel;
    }
}