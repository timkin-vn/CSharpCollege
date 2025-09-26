namespace Alarm_clock_C_.Forms
{
    partial class TimerForm
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
            this.TimerLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.TimerTextBox = new System.Windows.Forms.TextBox();
            this.TimerTime = new System.Windows.Forms.Label();
            this.timerT = new System.Windows.Forms.Timer(this.components);
            this.StopTimer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Location = new System.Drawing.Point(12, 13);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TimerLabel.Size = new System.Drawing.Size(89, 13);
            this.TimerLabel.TabIndex = 0;
            this.TimerLabel.Text = "Время таймера:";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(15, 182);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(180, 23);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "Запустить";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // TimerTextBox
            // 
            this.TimerTextBox.Location = new System.Drawing.Point(107, 10);
            this.TimerTextBox.Name = "TimerTextBox";
            this.TimerTextBox.Size = new System.Drawing.Size(288, 20);
            this.TimerTextBox.TabIndex = 5;
            // 
            // TimerTime
            // 
            this.TimerTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 65.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TimerTime.Location = new System.Drawing.Point(12, 52);
            this.TimerTime.Name = "TimerTime";
            this.TimerTime.Size = new System.Drawing.Size(390, 98);
            this.TimerTime.TabIndex = 6;
            this.TimerTime.Text = "00:00:00";
            this.TimerTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerT
            // 
            this.timerT.Interval = 1000;
            this.timerT.Tick += new System.EventHandler(this.timerT_Tick);
            // 
            // StopTimer
            // 
            this.StopTimer.Location = new System.Drawing.Point(201, 182);
            this.StopTimer.Name = "StopTimer";
            this.StopTimer.Size = new System.Drawing.Size(194, 23);
            this.StopTimer.TabIndex = 7;
            this.StopTimer.Text = "Остановить";
            this.StopTimer.UseVisualStyleBackColor = true;
            this.StopTimer.Click += new System.EventHandler(this.StopTimer_Click);
            // 
            // TimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 217);
            this.Controls.Add(this.StopTimer);
            this.Controls.Add(this.TimerTime);
            this.Controls.Add(this.TimerTextBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.TimerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TimerForm";
            this.Text = "Таймер";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox TimerTextBox;
        private System.Windows.Forms.Label TimerTime;
        private System.Windows.Forms.Timer timerT;
        private System.Windows.Forms.Button StopTimer;
    }
}