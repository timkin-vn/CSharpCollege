namespace AlarmClock
{
    partial class StopwatchForm
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
            this.labelTime = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnLap = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnBackToClock = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(377, 9);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(49, 13);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "00:00:00";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(168, 76);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnLap
            // 
            this.btnLap.Location = new System.Drawing.Point(334, 76);
            this.btnLap.Name = "btnLap";
            this.btnLap.Size = new System.Drawing.Size(131, 23);
            this.btnLap.TabIndex = 2;
            this.btnLap.Text = "Промежуток";
            this.btnLap.UseVisualStyleBackColor = true;
            this.btnLap.Click += new System.EventHandler(this.btnLap_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(512, 76);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(118, 23);
            this.btnPause.TabIndex = 3;
            this.btnPause.Text = "Стоп";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnBackToClock
            // 
            this.btnBackToClock.Location = new System.Drawing.Point(168, 317);
            this.btnBackToClock.Name = "btnBackToClock";
            this.btnBackToClock.Size = new System.Drawing.Size(112, 23);
            this.btnBackToClock.TabIndex = 4;
            this.btnBackToClock.Text = "Будильник";
            this.btnBackToClock.UseVisualStyleBackColor = true;
            this.btnBackToClock.Click += new System.EventHandler(this.btnBackToClock_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(168, 105);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(112, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Сброс";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // StopwatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnBackToClock);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnLap);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.labelTime);
            this.Name = "StopwatchForm";
            this.Text = "StopwatchForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnLap;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnBackToClock;
        private System.Windows.Forms.Button btnReset;
    }
}