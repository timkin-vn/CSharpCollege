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
            this.IsAlarmOnCheckBox = new System.Windows.Forms.CheckBox();
            this.IsSoundOnCheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время срабатывания";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(15, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Сообщение при срабатывании";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlarmTimeTextBox
            // 
            this.AlarmTimeTextBox.Location = new System.Drawing.Point(260, 34);
            this.AlarmTimeTextBox.Name = "AlarmTimeTextBox";
            this.AlarmTimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.AlarmTimeTextBox.TabIndex = 2;
            // 
            // AlarmMessageTextBox
            // 
            this.AlarmMessageTextBox.Location = new System.Drawing.Point(260, 64);
            this.AlarmMessageTextBox.Name = "AlarmMessageTextBox";
            this.AlarmMessageTextBox.Size = new System.Drawing.Size(194, 20);
            this.AlarmMessageTextBox.TabIndex = 3;
            // 
            // IsAlarmOnCheckBox
            // 
            this.IsAlarmOnCheckBox.AutoSize = true;
            this.IsAlarmOnCheckBox.Location = new System.Drawing.Point(18, 106);
            this.IsAlarmOnCheckBox.Name = "IsAlarmOnCheckBox";
            this.IsAlarmOnCheckBox.Size = new System.Drawing.Size(131, 17);
            this.IsAlarmOnCheckBox.TabIndex = 4;
            this.IsAlarmOnCheckBox.Text = "Включить будильник";
            this.IsAlarmOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // IsSoundOnCheckBox
            // 
            this.IsSoundOnCheckBox.AutoSize = true;
            this.IsSoundOnCheckBox.Location = new System.Drawing.Point(18, 129);
            this.IsSoundOnCheckBox.Name = "IsSoundOnCheckBox";
            this.IsSoundOnCheckBox.Size = new System.Drawing.Size(163, 17);
            this.IsSoundOnCheckBox.TabIndex = 5;
            this.IsSoundOnCheckBox.Text = "Включить звуковой сигнал";
            this.IsSoundOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(298, 129);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "ОК";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(379, 129);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(466, 161);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.IsSoundOnCheckBox);
            this.Controls.Add(this.IsAlarmOnCheckBox);
            this.Controls.Add(this.AlarmMessageTextBox);
            this.Controls.Add(this.AlarmTimeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AlarmTimeTextBox;
        private System.Windows.Forms.TextBox AlarmMessageTextBox;
        private System.Windows.Forms.CheckBox IsAlarmOnCheckBox;
        private System.Windows.Forms.CheckBox IsSoundOnCheckBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
    }
}