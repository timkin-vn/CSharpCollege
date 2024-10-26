
namespace AlarmClock.Forms
{
    partial class main_menu
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
            this.alarm = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Postnoe_maslo = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // alarm
            // 
            this.alarm.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.alarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.alarm.Location = new System.Drawing.Point(295, 96);
            this.alarm.Name = "alarm";
            this.alarm.Size = new System.Drawing.Size(162, 67);
            this.alarm.TabIndex = 0;
            this.alarm.Text = "будильник";
            this.alarm.UseVisualStyleBackColor = false;
            this.alarm.Click += new System.EventHandler(this.alarm_Click);
            // 
            // Postnoe_maslo
            // 
            this.Postnoe_maslo.AutoSize = true;
            this.Postnoe_maslo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Postnoe_maslo.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Postnoe_maslo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Postnoe_maslo.Location = new System.Drawing.Point(130, 9);
            this.Postnoe_maslo.Name = "Postnoe_maslo";
            this.Postnoe_maslo.Size = new System.Drawing.Size(482, 51);
            this.Postnoe_maslo.TabIndex = 1;
            this.Postnoe_maslo.Text = "Управление временем";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(640, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "About us";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // main_menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::AlarmClock.Properties.Resources.haker2;
            this.ClientSize = new System.Drawing.Size(727, 395);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Postnoe_maslo);
            this.Controls.Add(this.alarm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "main_menu";
            this.Text = "Управление временем";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button alarm;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label Postnoe_maslo;
        private System.Windows.Forms.Button button3;
    }
}