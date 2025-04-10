namespace AlarmClock.Forms
{
    partial class Stopwatch
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
            this.StartTimer = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Label();
            this.Restar = new System.Windows.Forms.Button();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // StartTimer
            // 
            this.StartTimer.BackColor = System.Drawing.Color.Black;
            this.StartTimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartTimer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.StartTimer.Location = new System.Drawing.Point(119, 101);
            this.StartTimer.Name = "StartTimer";
            this.StartTimer.Size = new System.Drawing.Size(75, 23);
            this.StartTimer.TabIndex = 0;
            this.StartTimer.Text = "Старт";
            this.StartTimer.UseVisualStyleBackColor = false;
            this.StartTimer.Click += new System.EventHandler(this.StartTimer_Click);
            // 
            // Time
            // 
            this.Time.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Time.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Time.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Time.Location = new System.Drawing.Point(119, 9);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(198, 75);
            this.Time.TabIndex = 1;
            this.Time.Text = "00:00:00.00";
            this.Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Time.Click += new System.EventHandler(this.label1_Click);
            // 
            // Restar
            // 
            this.Restar.BackColor = System.Drawing.Color.Black;
            this.Restar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Restar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Restar.Location = new System.Drawing.Point(242, 101);
            this.Restar.Name = "Restar";
            this.Restar.Size = new System.Drawing.Size(75, 23);
            this.Restar.TabIndex = 2;
            this.Restar.Text = "Сброс";
            this.Restar.UseVisualStyleBackColor = false;
            this.Restar.Click += new System.EventHandler(this.Restar_Click);
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.Interval = 10;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Stopwatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(435, 136);
            this.Controls.Add(this.Restar);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.StartTimer);
            this.Name = "Stopwatch";
            this.Text = "Timer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartTimer;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Button Restar;
        private System.Windows.Forms.Timer Timer1;
    }
}