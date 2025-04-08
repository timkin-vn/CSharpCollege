namespace AlarmClock.Forms
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AlarmTimeTextBox = new System.Windows.Forms.TextBox();
            this.AlarmMessageTextBox = new System.Windows.Forms.TextBox();
            this.IsAlarmActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.IsSoundActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время срабатывания:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Сообщение при срабатывании:";
            // 
            // AlarmTimeTextBox
            // 
            this.AlarmTimeTextBox.Location = new System.Drawing.Point(183, 12);
            this.AlarmTimeTextBox.Name = "AlarmTimeTextBox";
            this.AlarmTimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.AlarmTimeTextBox.TabIndex = 2;
            // 
            // AlarmMessageTextBox
            // 
            this.AlarmMessageTextBox.Location = new System.Drawing.Point(183, 38);
            this.AlarmMessageTextBox.Name = "AlarmMessageTextBox";
            this.AlarmMessageTextBox.Size = new System.Drawing.Size(233, 20);
            this.AlarmMessageTextBox.TabIndex = 3;
            // 
            // IsAlarmActiveCheckBox
            // 
            this.IsAlarmActiveCheckBox.AutoSize = true;
            this.IsAlarmActiveCheckBox.Location = new System.Drawing.Point(12, 74);
            this.IsAlarmActiveCheckBox.Name = "IsAlarmActiveCheckBox";
            this.IsAlarmActiveCheckBox.Size = new System.Drawing.Size(126, 17);
            this.IsAlarmActiveCheckBox.TabIndex = 4;
            this.IsAlarmActiveCheckBox.Text = "Будильник включен";
            this.IsAlarmActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // IsSoundActiveCheckBox
            // 
            this.IsSoundActiveCheckBox.AutoSize = true;
            this.IsSoundActiveCheckBox.Location = new System.Drawing.Point(12, 97);
            this.IsSoundActiveCheckBox.Name = "IsSoundActiveCheckBox";
            this.IsSoundActiveCheckBox.Size = new System.Drawing.Size(158, 17);
            this.IsSoundActiveCheckBox.TabIndex = 5;
            this.IsSoundActiveCheckBox.Text = "Звуковой сигнал включен";
            this.IsSoundActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(260, 124);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(341, 124);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 7;
            this.CancelBtn.Text = "Отмена";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(428, 159);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.IsSoundActiveCheckBox);
            this.Controls.Add(this.IsAlarmActiveCheckBox);
            this.Controls.Add(this.AlarmMessageTextBox);
            this.Controls.Add(this.AlarmTimeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Настройки будильника";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AlarmTimeTextBox;
        private System.Windows.Forms.TextBox AlarmMessageTextBox;
        private System.Windows.Forms.CheckBox IsAlarmActiveCheckBox;
        private System.Windows.Forms.CheckBox IsSoundActiveCheckBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelBtn;
    }
}