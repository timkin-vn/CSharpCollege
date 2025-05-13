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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listKrugi = new System.Windows.Forms.ListBox();
            this.buttonSbros = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // laiblSec
            // 
            this.laiblSec.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.laiblSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laiblSec.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.laiblSec.Location = new System.Drawing.Point(59, 9);
            this.laiblSec.Name = "laiblSec";
            this.laiblSec.Size = new System.Drawing.Size(214, 60);
            this.laiblSec.TabIndex = 0;
            this.laiblSec.Text = "00:00:00";
            this.laiblSec.Click += new System.EventHandler(this.laiblSec_Click);
            // 
            // buttonStar
            // 
            this.buttonStar.BackColor = System.Drawing.Color.DimGray;
            this.buttonStar.Location = new System.Drawing.Point(344, 84);
            this.buttonStar.Name = "buttonStar";
            this.buttonStar.Size = new System.Drawing.Size(105, 34);
            this.buttonStar.TabIndex = 1;
            this.buttonStar.Text = "Старт";
            this.buttonStar.UseVisualStyleBackColor = false;
            this.buttonStar.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonStop.Location = new System.Drawing.Point(344, 133);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(105, 34);
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.IndianRed;
            this.button1.Location = new System.Drawing.Point(344, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "Выход";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.Location = new System.Drawing.Point(12, 184);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 20);
            this.button2.TabIndex = 4;
            this.button2.Text = "Круг";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listKrugi
            // 
            this.listKrugi.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.listKrugi.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listKrugi.FormattingEnabled = true;
            this.listKrugi.ItemHeight = 21;
            this.listKrugi.Location = new System.Drawing.Point(12, 84);
            this.listKrugi.Name = "listKrugi";
            this.listKrugi.Size = new System.Drawing.Size(315, 88);
            this.listKrugi.TabIndex = 5;
            this.listKrugi.SelectedIndexChanged += new System.EventHandler(this.listKrugi_SelectedIndexChanged);
            // 
            // buttonSbros
            // 
            this.buttonSbros.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonSbros.Location = new System.Drawing.Point(173, 184);
            this.buttonSbros.Name = "buttonSbros";
            this.buttonSbros.Size = new System.Drawing.Size(154, 20);
            this.buttonSbros.TabIndex = 6;
            this.buttonSbros.Text = "Сброс";
            this.buttonSbros.UseVisualStyleBackColor = false;
            this.buttonSbros.Click += new System.EventHandler(this.buttonSbros_Click);
            // 
            // X
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(459, 214);
            this.Controls.Add(this.buttonSbros);
            this.Controls.Add(this.listKrugi);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStar);
            this.Controls.Add(this.laiblSec);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "X";
            this.Text = "Secunda";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laiblSec;
        private System.Windows.Forms.Button buttonStar;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Timer timerSec;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listKrugi;
        private System.Windows.Forms.Button buttonSbros;
    }
}