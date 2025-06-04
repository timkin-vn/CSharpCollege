
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время срабатывания:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Сообщение при срабатывании:";
            // 
            // AlarmTimeTextBox
            // 
            this.AlarmTimeTextBox.BackColor = System.Drawing.Color.Lavender;
            this.AlarmTimeTextBox.Location = new System.Drawing.Point(172, 17);
            this.AlarmTimeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AlarmTimeTextBox.Name = "AlarmTimeTextBox";
            this.AlarmTimeTextBox.Size = new System.Drawing.Size(216, 22);
            this.AlarmTimeTextBox.TabIndex = 2;
            // 
            // AlarmMessageTextBox
            // 
            this.AlarmMessageTextBox.BackColor = System.Drawing.Color.Lavender;
            this.AlarmMessageTextBox.Location = new System.Drawing.Point(13, 70);
            this.AlarmMessageTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AlarmMessageTextBox.Name = "AlarmMessageTextBox";
            this.AlarmMessageTextBox.Size = new System.Drawing.Size(375, 22);
            this.AlarmMessageTextBox.TabIndex = 3;
            // 
            // IsAlarmActiveCheckBox
            // 
            this.IsAlarmActiveCheckBox.AutoSize = true;
            this.IsAlarmActiveCheckBox.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.IsAlarmActiveCheckBox.Location = new System.Drawing.Point(13, 100);
            this.IsAlarmActiveCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IsAlarmActiveCheckBox.Name = "IsAlarmActiveCheckBox";
            this.IsAlarmActiveCheckBox.Size = new System.Drawing.Size(160, 20);
            this.IsAlarmActiveCheckBox.TabIndex = 4;
            this.IsAlarmActiveCheckBox.Text = "Будильник включен";
            this.IsAlarmActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // IsSoundActiveCheckBox
            // 
            this.IsSoundActiveCheckBox.AutoSize = true;
            this.IsSoundActiveCheckBox.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.IsSoundActiveCheckBox.Location = new System.Drawing.Point(13, 128);
            this.IsSoundActiveCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IsSoundActiveCheckBox.Name = "IsSoundActiveCheckBox";
            this.IsSoundActiveCheckBox.Size = new System.Drawing.Size(201, 20);
            this.IsSoundActiveCheckBox.TabIndex = 5;
            this.IsSoundActiveCheckBox.Text = "Звуковой сигнал включен";
            this.IsSoundActiveCheckBox.UseVisualStyleBackColor = true;
            this.IsSoundActiveCheckBox.CheckedChanged += new System.EventHandler(this.IsSoundActiveCheckBox_CheckedChanged);
            // 
            // OkButton
            // 
            this.OkButton.BackColor = System.Drawing.Color.Lavender;
            this.OkButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.OkButton.Location = new System.Drawing.Point(13, 222);
            this.OkButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(176, 28);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.Lavender;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.ForeColor = System.Drawing.Color.MidnightBlue;
            this.CancelBtn.Location = new System.Drawing.Point(197, 222);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(191, 28);
            this.CancelBtn.TabIndex = 7;
            this.CancelBtn.Text = "Отмена";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.Lavender;
            this.comboBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "По умолчанию",
            "Выбрать мелодию"});
            this.comboBox1.Location = new System.Drawing.Point(12, 191);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(377, 24);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "По умолчанию";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(152, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Звук сигнала";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(401, 261);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.IsSoundActiveCheckBox);
            this.Controls.Add(this.IsAlarmActiveCheckBox);
            this.Controls.Add(this.AlarmMessageTextBox);
            this.Controls.Add(this.AlarmTimeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
    }
}
