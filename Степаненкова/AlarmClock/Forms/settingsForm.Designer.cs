namespace Alarm_clock_C_.Forms
{
    partial class settingsForm
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
            this.OKbutton = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.IsAlarmActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.IsSoundActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.MediaLabel = new System.Windows.Forms.Label();
            this.ChoiceSounds = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время срабатывания: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Сообщение при срабатывании:";
            // 
            // AlarmTimeTextBox
            // 
            this.AlarmTimeTextBox.Location = new System.Drawing.Point(178, 3);
            this.AlarmTimeTextBox.Name = "AlarmTimeTextBox";
            this.AlarmTimeTextBox.Size = new System.Drawing.Size(169, 20);
            this.AlarmTimeTextBox.TabIndex = 2;
            // 
            // AlarmMessageTextBox
            // 
            this.AlarmMessageTextBox.Location = new System.Drawing.Point(178, 29);
            this.AlarmMessageTextBox.Name = "AlarmMessageTextBox";
            this.AlarmMessageTextBox.Size = new System.Drawing.Size(169, 20);
            this.AlarmMessageTextBox.TabIndex = 3;
            // 
            // OKbutton
            // 
            this.OKbutton.Location = new System.Drawing.Point(191, 122);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 4;
            this.OKbutton.Text = "ОК";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(272, 122);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Отмена";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // IsAlarmActiveCheckBox
            // 
            this.IsAlarmActiveCheckBox.AutoSize = true;
            this.IsAlarmActiveCheckBox.Location = new System.Drawing.Point(12, 99);
            this.IsAlarmActiveCheckBox.Name = "IsAlarmActiveCheckBox";
            this.IsAlarmActiveCheckBox.Size = new System.Drawing.Size(126, 17);
            this.IsAlarmActiveCheckBox.TabIndex = 6;
            this.IsAlarmActiveCheckBox.Text = "Будильник включен";
            this.IsAlarmActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // IsSoundActiveCheckBox
            // 
            this.IsSoundActiveCheckBox.AutoSize = true;
            this.IsSoundActiveCheckBox.Location = new System.Drawing.Point(12, 122);
            this.IsSoundActiveCheckBox.Name = "IsSoundActiveCheckBox";
            this.IsSoundActiveCheckBox.Size = new System.Drawing.Size(96, 17);
            this.IsSoundActiveCheckBox.TabIndex = 7;
            this.IsSoundActiveCheckBox.Text = "Звук включен";
            this.IsSoundActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // MediaLabel
            // 
            this.MediaLabel.AutoSize = true;
            this.MediaLabel.Location = new System.Drawing.Point(12, 62);
            this.MediaLabel.Name = "MediaLabel";
            this.MediaLabel.Size = new System.Drawing.Size(131, 13);
            this.MediaLabel.TabIndex = 8;
            this.MediaLabel.Text = "Звук при срабатывании:";
            // 
            // ChoiceSounds
            // 
            this.ChoiceSounds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChoiceSounds.FormattingEnabled = true;
            this.ChoiceSounds.Location = new System.Drawing.Point(178, 59);
            this.ChoiceSounds.Name = "ChoiceSounds";
            this.ChoiceSounds.Size = new System.Drawing.Size(169, 21);
            this.ChoiceSounds.TabIndex = 9;
            // 
            // settingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 157);
            this.Controls.Add(this.ChoiceSounds);
            this.Controls.Add(this.MediaLabel);
            this.Controls.Add(this.IsSoundActiveCheckBox);
            this.Controls.Add(this.IsAlarmActiveCheckBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.AlarmMessageTextBox);
            this.Controls.Add(this.AlarmTimeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "settingsForm";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.settingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AlarmTimeTextBox;
        private System.Windows.Forms.TextBox AlarmMessageTextBox;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.CheckBox IsAlarmActiveCheckBox;
        private System.Windows.Forms.CheckBox IsSoundActiveCheckBox;
        private System.Windows.Forms.Label MediaLabel;
        private System.Windows.Forms.ComboBox ChoiceSounds;
    }
}