namespace WindowsFormsApp1
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.KK = new System.Windows.Forms.Label();
            this.AlarmTime = new System.Windows.Forms.TextBox();
            this.AlarmMessage = new System.Windows.Forms.TextBox();
            this.ClockButte = new System.Windows.Forms.CheckBox();
            this.SoudButt = new System.Windows.Forms.CheckBox();
            this.OK = new System.Windows.Forms.Button();
            this.OTMENA = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время срабатывания:";
            // 
            // KK
            // 
            this.KK.AutoSize = true;
            this.KK.Location = new System.Drawing.Point(12, 53);
            this.KK.Name = "KK";
            this.KK.Size = new System.Drawing.Size(68, 13);
            this.KK.TabIndex = 1;
            this.KK.Text = "Сообщение:";
            // 
            // AlarmTime
            // 
            this.AlarmTime.Location = new System.Drawing.Point(139, 16);
            this.AlarmTime.Name = "AlarmTime";
            this.AlarmTime.Size = new System.Drawing.Size(100, 20);
            this.AlarmTime.TabIndex = 2;
            // 
            // AlarmMessage
            // 
            this.AlarmMessage.Location = new System.Drawing.Point(136, 46);
            this.AlarmMessage.Name = "AlarmMessage";
            this.AlarmMessage.Size = new System.Drawing.Size(100, 20);
            this.AlarmMessage.TabIndex = 3;
            // 
            // ClockButte
            // 
            this.ClockButte.AutoSize = true;
            this.ClockButte.Location = new System.Drawing.Point(14, 86);
            this.ClockButte.Name = "ClockButte";
            this.ClockButte.Size = new System.Drawing.Size(132, 17);
            this.ClockButte.TabIndex = 4;
            this.ClockButte.Text = "Будильник включён?";
            this.ClockButte.UseVisualStyleBackColor = true;
            // 
            // SoudButt
            // 
            this.SoudButt.AutoSize = true;
            this.SoudButt.Location = new System.Drawing.Point(15, 109);
            this.SoudButt.Name = "SoudButt";
            this.SoudButt.Size = new System.Drawing.Size(107, 17);
            this.SoudButt.TabIndex = 5;
            this.SoudButt.Text = "Включить звук?";
            this.SoudButt.UseVisualStyleBackColor = true;
            this.SoudButt.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(15, 144);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 6;
            this.OK.Text = "Ок";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.button1_Click);
            // 
            // OTMENA
            // 
            this.OTMENA.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OTMENA.Location = new System.Drawing.Point(161, 144);
            this.OTMENA.Name = "OTMENA";
            this.OTMENA.Size = new System.Drawing.Size(75, 23);
            this.OTMENA.TabIndex = 7;
            this.OTMENA.Text = "Отмена";
            this.OTMENA.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 177);
            this.Controls.Add(this.OTMENA);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.SoudButt);
            this.Controls.Add(this.ClockButte);
            this.Controls.Add(this.AlarmMessage);
            this.Controls.Add(this.AlarmTime);
            this.Controls.Add(this.KK);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label KK;
        private System.Windows.Forms.TextBox AlarmTime;
        private System.Windows.Forms.TextBox AlarmMessage;
        private System.Windows.Forms.CheckBox ClockButte;
        private System.Windows.Forms.CheckBox SoudButt;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button OTMENA;
    }
}