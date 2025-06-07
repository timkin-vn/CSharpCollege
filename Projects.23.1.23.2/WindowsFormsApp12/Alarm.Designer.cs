namespace WindowsFormsApp12
{
    partial class Alarm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.buttonSetAlarm = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonSnooze = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxTime
            // 
            this.textBoxTime.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.textBoxTime.Location = new System.Drawing.Point(224, 172);
            this.textBoxTime.Multiline = true;
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(377, 55);
            this.textBoxTime.TabIndex = 0;
            // 
            // buttonSetAlarm
            // 
            this.buttonSetAlarm.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonSetAlarm.Location = new System.Drawing.Point(224, 248);
            this.buttonSetAlarm.Name = "buttonSetAlarm";
            this.buttonSetAlarm.Size = new System.Drawing.Size(377, 63);
            this.buttonSetAlarm.TabIndex = 1;
            this.buttonSetAlarm.Text = "Установить будильник";
            this.buttonSetAlarm.UseVisualStyleBackColor = false;
            this.buttonSetAlarm.Click += new System.EventHandler(this.buttonSetAlarm_Click_1);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(219, 392);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(89, 25);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Статус: ";
            this.labelStatus.Click += new System.EventHandler(this.labelStatus_Click);
            // 
            // buttonSnooze
            // 
            this.buttonSnooze.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonSnooze.Location = new System.Drawing.Point(224, 328);
            this.buttonSnooze.Name = "buttonSnooze";
            this.buttonSnooze.Size = new System.Drawing.Size(377, 61);
            this.buttonSnooze.TabIndex = 3;
            this.buttonSnooze.Text = "Отложить";
            this.buttonSnooze.UseVisualStyleBackColor = false;
            this.buttonSnooze.Click += new System.EventHandler(this.buttonSnooze_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(279, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Введите время будильника: ";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(224, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 86);
            this.label1.TabIndex = 6;
            this.label1.Text = "00:00:00";
            // 
            // Alarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSnooze);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonSetAlarm);
            this.Controls.Add(this.textBoxTime);
            this.Name = "Alarm";
            this.Text = "Будильник";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Button buttonSetAlarm;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonSnooze;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

