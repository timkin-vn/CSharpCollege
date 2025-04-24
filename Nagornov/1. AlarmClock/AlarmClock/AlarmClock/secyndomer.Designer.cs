namespace AlarmClock.Forms
{
    partial class X
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
            this.timerSec = new System.Windows.Forms.Timer(this.components);
            this.laiblSec = new System.Windows.Forms.Label();
            this.buttonStar = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timelast = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timerSec
            // 
            this.timerSec.Interval = 1000;
            this.timerSec.Tick += new System.EventHandler(this.timerSec_Tick_1);
            // 
            // laiblSec
            // 
            this.laiblSec.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.laiblSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laiblSec.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.laiblSec.Location = new System.Drawing.Point(44, 47);
            this.laiblSec.Name = "laiblSec";
            this.laiblSec.Size = new System.Drawing.Size(214, 60);
            this.laiblSec.TabIndex = 1;
            this.laiblSec.Text = "00:00:00";
            // 
            // buttonStar
            // 
            this.buttonStar.BackColor = System.Drawing.Color.Transparent;
            this.buttonStar.Location = new System.Drawing.Point(301, 47);
            this.buttonStar.Name = "buttonStar";
            this.buttonStar.Size = new System.Drawing.Size(150, 34);
            this.buttonStar.TabIndex = 2;
            this.buttonStar.Text = "Старт";
            this.buttonStar.UseVisualStyleBackColor = false;
            this.buttonStar.Click += new System.EventHandler(this.buttonStar_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.White;
            this.buttonStop.Location = new System.Drawing.Point(301, 97);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(150, 31);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.Text = "Стоп";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(301, 147);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 28);
            this.button2.TabIndex = 5;
            this.button2.Text = "Узнать результат";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // timelast
            // 
            this.timelast.BackColor = System.Drawing.SystemColors.HighlightText;
            this.timelast.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timelast.FormattingEnabled = true;
            this.timelast.ItemHeight = 21;
            this.timelast.Location = new System.Drawing.Point(37, 133);
            this.timelast.Name = "timelast";
            this.timelast.Size = new System.Drawing.Size(245, 88);
            this.timelast.TabIndex = 6;
            this.timelast.SelectedIndexChanged += new System.EventHandler(this.timelast_SelectedIndexChanged_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(301, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "Выход";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // X
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 244);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.timelast);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStar);
            this.Controls.Add(this.laiblSec);
            this.Name = "X";
            this.Text = "secyndomer";
            this.Load += new System.EventHandler(this.X_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerSec;
        private System.Windows.Forms.Label laiblSec;
        private System.Windows.Forms.Button buttonStar;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox timelast;
        private System.Windows.Forms.Button button1;
    }
}