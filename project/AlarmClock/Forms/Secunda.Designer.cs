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
            this.laiblSec = new System.Windows.Forms.Label();
            this.buttonStar = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.timerSec = new System.Windows.Forms.Timer(this.components);
            this.buttonSbros = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // laiblSec
            // 
            this.laiblSec.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.laiblSec.Font = new System.Drawing.Font("Consolas", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laiblSec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.laiblSec.Location = new System.Drawing.Point(12, 23);
            this.laiblSec.Name = "laiblSec";
            this.laiblSec.Size = new System.Drawing.Size(519, 106);
            this.laiblSec.TabIndex = 0;
            this.laiblSec.Text = "00:00:00";
            this.laiblSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.laiblSec.Click += new System.EventHandler(this.laiblSec_Click);
            // 
            // buttonStar
            // 
            this.buttonStar.BackColor = System.Drawing.Color.Turquoise;
            this.buttonStar.Location = new System.Drawing.Point(138, 145);
            this.buttonStar.Name = "buttonStar";
            this.buttonStar.Size = new System.Drawing.Size(272, 56);
            this.buttonStar.TabIndex = 1;
            this.buttonStar.Text = "Старт";
            this.buttonStar.UseVisualStyleBackColor = false;
            this.buttonStar.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.Turquoise;
            this.buttonStop.Location = new System.Drawing.Point(138, 207);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(272, 49);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Стоп";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // timerSec
            // 
            this.timerSec.Interval = 1000;
            this.timerSec.Tick += new System.EventHandler(this.timerSec_Tick);
            // 
            // buttonSbros
            // 
            this.buttonSbros.BackColor = System.Drawing.Color.Turquoise;
            this.buttonSbros.Location = new System.Drawing.Point(138, 262);
            this.buttonSbros.Name = "buttonSbros";
            this.buttonSbros.Size = new System.Drawing.Size(272, 54);
            this.buttonSbros.TabIndex = 6;
            this.buttonSbros.Text = "Сброс";
            this.buttonSbros.UseVisualStyleBackColor = false;
            this.buttonSbros.Click += new System.EventHandler(this.buttonSbros_Click);
            // 
            // X
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(543, 328);
            this.Controls.Add(this.buttonSbros);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStar);
            this.Controls.Add(this.laiblSec);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "X";
            this.Text = "Таймер";
            this.Load += new System.EventHandler(this.X_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laiblSec;
        private System.Windows.Forms.Button buttonStar;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Timer timerSec;
        private System.Windows.Forms.Button buttonSbros;
    }
}