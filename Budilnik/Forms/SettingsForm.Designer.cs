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
            this.IsSoundActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.AlarmListBox = new System.Windows.Forms.ListBox();
            this.AddAlarmButton = new System.Windows.Forms.Button();
            this.DeleteAlarmButton = new System.Windows.Forms.Button();
            this.NewAlarmTimeTextBox = new System.Windows.Forms.TextBox();
            this.NewAlarmTimeLabel = new System.Windows.Forms.Label();
            this.AlarmMessageTextBox = new System.Windows.Forms.TextBox();
            this.AlarmMessageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IsSoundActiveCheckBox
            // 
            this.IsSoundActiveCheckBox.AutoSize = true;
            this.IsSoundActiveCheckBox.Location = new System.Drawing.Point(24, 23);
            this.IsSoundActiveCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.IsSoundActiveCheckBox.Name = "IsSoundActiveCheckBox";
            this.IsSoundActiveCheckBox.Size = new System.Drawing.Size(301, 29);
            this.IsSoundActiveCheckBox.TabIndex = 5;
            this.IsSoundActiveCheckBox.Text = "Звуковой сигнал включен";
            this.IsSoundActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(692, 635);
            this.OkButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(150, 44);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(540, 635);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(150, 44);
            this.CancelBtn.TabIndex = 7;
            this.CancelBtn.Text = "Отмена";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // AlarmListBox
            // 
            this.AlarmListBox.FormattingEnabled = true;
            this.AlarmListBox.ItemHeight = 25;
            this.AlarmListBox.Location = new System.Drawing.Point(24, 269);
            this.AlarmListBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.AlarmListBox.Name = "AlarmListBox";
            this.AlarmListBox.Size = new System.Drawing.Size(966, 354);
            this.AlarmListBox.TabIndex = 8;
            // 
            // AddAlarmButton
            // 
            this.AddAlarmButton.Location = new System.Drawing.Point(530, 208);
            this.AddAlarmButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.AddAlarmButton.Name = "AddAlarmButton";
            this.AddAlarmButton.Size = new System.Drawing.Size(150, 44);
            this.AddAlarmButton.TabIndex = 9;
            this.AddAlarmButton.Text = "Добавить";
            this.AddAlarmButton.UseVisualStyleBackColor = true;
            this.AddAlarmButton.Click += new System.EventHandler(this.AddAlarmButton_Click);
            // 
            // DeleteAlarmButton
            // 
            this.DeleteAlarmButton.Location = new System.Drawing.Point(844, 635);
            this.DeleteAlarmButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.DeleteAlarmButton.Name = "DeleteAlarmButton";
            this.DeleteAlarmButton.Size = new System.Drawing.Size(150, 44);
            this.DeleteAlarmButton.TabIndex = 11;
            this.DeleteAlarmButton.Text = "Удалить";
            this.DeleteAlarmButton.UseVisualStyleBackColor = true;
            this.DeleteAlarmButton.Click += new System.EventHandler(this.RemoveAlarmButton_Click);
            // 
            // NewAlarmTimeTextBox
            // 
            this.NewAlarmTimeTextBox.Location = new System.Drawing.Point(298, 212);
            this.NewAlarmTimeTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.NewAlarmTimeTextBox.Name = "NewAlarmTimeTextBox";
            this.NewAlarmTimeTextBox.Size = new System.Drawing.Size(196, 31);
            this.NewAlarmTimeTextBox.TabIndex = 12;
            // 
            // NewAlarmTimeLabel
            // 
            this.NewAlarmTimeLabel.AutoSize = true;
            this.NewAlarmTimeLabel.Location = new System.Drawing.Point(24, 217);
            this.NewAlarmTimeLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.NewAlarmTimeLabel.Name = "NewAlarmTimeLabel";
            this.NewAlarmTimeLabel.Size = new System.Drawing.Size(270, 25);
            this.NewAlarmTimeLabel.TabIndex = 13;
            this.NewAlarmTimeLabel.Text = "Новое время будильника:";
            // 
            // AlarmMessageTextBox
            // 
            this.AlarmMessageTextBox.Location = new System.Drawing.Point(298, 81);
            this.AlarmMessageTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.AlarmMessageTextBox.Name = "AlarmMessageTextBox";
            this.AlarmMessageTextBox.Size = new System.Drawing.Size(692, 31);
            this.AlarmMessageTextBox.TabIndex = 14;
            // 
            // AlarmMessageLabel
            // 
            this.AlarmMessageLabel.AutoSize = true;
            this.AlarmMessageLabel.Location = new System.Drawing.Point(24, 129);
            this.AlarmMessageLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.AlarmMessageLabel.Name = "AlarmMessageLabel";
            this.AlarmMessageLabel.Size = new System.Drawing.Size(186, 25);
            this.AlarmMessageLabel.TabIndex = 15;
            this.AlarmMessageLabel.Text = "Текст сообщения";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(1018, 694);
            this.Controls.Add(this.AlarmMessageLabel);
            this.Controls.Add(this.AlarmMessageTextBox);
            this.Controls.Add(this.NewAlarmTimeLabel);
            this.Controls.Add(this.NewAlarmTimeTextBox);
            this.Controls.Add(this.DeleteAlarmButton);
            this.Controls.Add(this.AddAlarmButton);
            this.Controls.Add(this.AlarmListBox);
            this.Controls.Add(this.IsSoundActiveCheckBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Настройки будильника";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox IsSoundActiveCheckBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ListBox AlarmListBox;
        private System.Windows.Forms.Button AddAlarmButton;
        private System.Windows.Forms.Button DeleteAlarmButton;
        private System.Windows.Forms.TextBox NewAlarmTimeTextBox;
        private System.Windows.Forms.Label NewAlarmTimeLabel;
        private System.Windows.Forms.TextBox AlarmMessageTextBox;
        private System.Windows.Forms.Label AlarmMessageLabel;
    }
}