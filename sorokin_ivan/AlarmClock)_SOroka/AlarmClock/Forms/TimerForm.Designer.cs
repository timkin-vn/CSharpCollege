namespace AlarmClock.Forms
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
            this.TimerTime = new System.Windows.Forms.Timer(this.components);
            this.TimerLabel = new System.Windows.Forms.Label();
            this.Addminet = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.LaunchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoEllipsis = true;
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TimerLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.TimerLabel.Font = new System.Drawing.Font("Segoe UI Emoji", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.TimerLabel.Location = new System.Drawing.Point(261, 124);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(205, 63);
            this.TimerLabel.TabIndex = 0;
            this.TimerLabel.Text = "00:00:00";
            this.TimerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Addminet
            // 
            this.Addminet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Addminet.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Addminet.Location = new System.Drawing.Point(3, 222);
            this.Addminet.Name = "Addminet";
            this.Addminet.Size = new System.Drawing.Size(308, 55);
            this.Addminet.TabIndex = 1;
            this.Addminet.Text = "Добавте минуту ";
            this.Addminet.UseVisualStyleBackColor = true;
            this.Addminet.Click += new System.EventHandler(this.Addminet_Click);
            // 
            // StopButton
            // 
            this.StopButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.StopButton.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopButton.Location = new System.Drawing.Point(499, 222);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(308, 55);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Стоп ";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // LaunchButton
            // 
            this.LaunchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LaunchButton.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchButton.Location = new System.Drawing.Point(272, 298);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(237, 42);
            this.LaunchButton.TabIndex = 3;
            this.LaunchButton.Text = "Запуск";
            this.LaunchButton.UseVisualStyleBackColor = true;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // TimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LaunchButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.Addminet);
            this.Controls.Add(this.TimerLabel);
            this.Name = "TimerForm";
            this.Text = "TimerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer TimerTime;
        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Button Addminet;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button LaunchButton;
    }
}