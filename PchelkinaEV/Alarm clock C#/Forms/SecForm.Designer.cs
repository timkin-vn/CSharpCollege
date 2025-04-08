namespace Alarm_clock_C_.Forms
{
    partial class SecForm
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
            this.StartSec = new System.Windows.Forms.Button();
            this.StopSec = new System.Windows.Forms.Button();
            this.SecLabel = new System.Windows.Forms.Label();
            this.SecTimer = new System.Windows.Forms.Timer(this.components);
            this.ClearSec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartSec
            // 
            this.StartSec.Location = new System.Drawing.Point(12, 145);
            this.StartSec.Name = "StartSec";
            this.StartSec.Size = new System.Drawing.Size(144, 23);
            this.StartSec.TabIndex = 0;
            this.StartSec.Text = "Запустить";
            this.StartSec.UseVisualStyleBackColor = true;
            this.StartSec.Click += new System.EventHandler(this.StartSec_Click);
            // 
            // StopSec
            // 
            this.StopSec.Location = new System.Drawing.Point(162, 145);
            this.StopSec.Name = "StopSec";
            this.StopSec.Size = new System.Drawing.Size(152, 23);
            this.StopSec.TabIndex = 1;
            this.StopSec.Text = "Остановить";
            this.StopSec.UseVisualStyleBackColor = true;
            this.StopSec.Click += new System.EventHandler(this.StopSec_Click);
            // 
            // SecLabel
            // 
            this.SecLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SecLabel.Location = new System.Drawing.Point(12, 21);
            this.SecLabel.Name = "SecLabel";
            this.SecLabel.Size = new System.Drawing.Size(438, 104);
            this.SecLabel.TabIndex = 0;
            this.SecLabel.Text = "00:00:00";
            this.SecLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SecTimer
            // 
            this.SecTimer.Interval = 10;
            this.SecTimer.Tick += new System.EventHandler(this.SecTimer_Tick);
            // 
            // ClearSec
            // 
            this.ClearSec.Location = new System.Drawing.Point(320, 145);
            this.ClearSec.Name = "ClearSec";
            this.ClearSec.Size = new System.Drawing.Size(130, 23);
            this.ClearSec.TabIndex = 2;
            this.ClearSec.Text = "Очистить";
            this.ClearSec.UseVisualStyleBackColor = true;
            this.ClearSec.Click += new System.EventHandler(this.ClearSec_Click);
            // 
            // SecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 178);
            this.Controls.Add(this.ClearSec);
            this.Controls.Add(this.SecLabel);
            this.Controls.Add(this.StopSec);
            this.Controls.Add(this.StartSec);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SecForm";
            this.Text = "SecForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartSec;
        private System.Windows.Forms.Button StopSec;
        private System.Windows.Forms.Label SecLabel;
        private System.Windows.Forms.Timer SecTimer;
        private System.Windows.Forms.Button ClearSec;
    }
}