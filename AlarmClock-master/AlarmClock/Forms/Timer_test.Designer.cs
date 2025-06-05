namespace AlarmClock.Forms
{
    partial class Timer_test
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
            this.Timer_m = new System.Windows.Forms.Label();
            this.startTime = new System.Windows.Forms.Button();
            this.Timer2 = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.Minutes = new System.Windows.Forms.Label();
            this.seconds = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer_m
            // 
            this.Timer_m.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Timer_m.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Timer_m.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Timer_m.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Timer_m.Location = new System.Drawing.Point(123, 20);
            this.Timer_m.Name = "Timer_m";
            this.Timer_m.Size = new System.Drawing.Size(194, 63);
            this.Timer_m.TabIndex = 0;
            this.Timer_m.Text = "00:00:00";
            this.Timer_m.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Timer_m.Click += new System.EventHandler(this.Timer_m_Click);
            // 
            // startTime
            // 
            this.startTime.BackColor = System.Drawing.Color.Black;
            this.startTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.startTime.Location = new System.Drawing.Point(187, 125);
            this.startTime.Name = "startTime";
            this.startTime.Size = new System.Drawing.Size(75, 23);
            this.startTime.TabIndex = 1;
            this.startTime.Text = "Пуск";
            this.startTime.UseVisualStyleBackColor = false;
            this.startTime.Click += new System.EventHandler(this.startTime_Click);
            // 
            // Timer2
            // 
            this.Timer2.Interval = 1000;
            this.Timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(172, 104);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown1.TabIndex = 2;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(277, 104);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown2.TabIndex = 3;
            // 
            // Minutes
            // 
            this.Minutes.AutoSize = true;
            this.Minutes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Minutes.Location = new System.Drawing.Point(120, 106);
            this.Minutes.Name = "Minutes";
            this.Minutes.Size = new System.Drawing.Size(46, 13);
            this.Minutes.TabIndex = 4;
            this.Minutes.Text = "Минуты";
            // 
            // seconds
            // 
            this.seconds.AutoSize = true;
            this.seconds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.seconds.Location = new System.Drawing.Point(228, 109);
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(43, 13);
            this.seconds.TabIndex = 5;
            this.seconds.Text = "Секунд";
            // 
            // Timer_test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(448, 159);
            this.Controls.Add(this.seconds);
            this.Controls.Add(this.Minutes);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.startTime);
            this.Controls.Add(this.Timer_m);
            this.Name = "Timer_test";
            this.Text = "Timer";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Timer_m;
        private System.Windows.Forms.Button startTime;
        private System.Windows.Forms.Timer Timer2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label Minutes;
        private System.Windows.Forms.Label seconds;
    }
}