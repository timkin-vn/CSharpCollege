
namespace AlarmClock.Forms
{
    partial class Timers
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
            this.headline = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SEcond = new System.Windows.Forms.NumericUpDown();
            this.Minute = new System.Windows.Forms.NumericUpDown();
            this.hour = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.SEcond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour)).BeginInit();
            this.SuspendLayout();
            // 
            // headline
            // 
            this.headline.AutoSize = true;
            this.headline.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.headline.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headline.Location = new System.Drawing.Point(101, 9);
            this.headline.Name = "headline";
            this.headline.Size = new System.Drawing.Size(128, 37);
            this.headline.TabIndex = 3;
            this.headline.Text = "Таймер";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(87, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "ЧЧ ММ СС";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(117, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 27);
            this.button1.TabIndex = 5;
            this.button1.Text = "Отсчет Пашел";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SEcond
            // 
            this.SEcond.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SEcond.Location = new System.Drawing.Point(191, 85);
            this.SEcond.Name = "SEcond";
            this.SEcond.Size = new System.Drawing.Size(50, 29);
            this.SEcond.TabIndex = 6;
            // 
            // Minute
            // 
            this.Minute.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Minute.Location = new System.Drawing.Point(146, 85);
            this.Minute.Name = "Minute";
            this.Minute.Size = new System.Drawing.Size(39, 29);
            this.Minute.TabIndex = 7;
            // 
            // hour
            // 
            this.hour.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hour.Location = new System.Drawing.Point(93, 85);
            this.hour.Name = "hour";
            this.hour.Size = new System.Drawing.Size(47, 29);
            this.hour.TabIndex = 8;
            // 
            // Timer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(342, 233);
            this.Controls.Add(this.hour);
            this.Controls.Add(this.Minute);
            this.Controls.Add(this.SEcond);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.headline);
            this.Name = "Timer";
            this.Text = "Timer";
            ((System.ComponentModel.ISupportInitialize)(this.SEcond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label headline;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown SEcond;
        private System.Windows.Forms.NumericUpDown Minute;
        private System.Windows.Forms.NumericUpDown hour;
    }
}