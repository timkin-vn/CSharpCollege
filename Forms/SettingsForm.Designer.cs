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
            this.IsSoundActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время срабатывания:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Сообщение при срабатывании:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // AlarmTimeTextBox
            // 
            this.AlarmTimeTextBox.BackColor = System.Drawing.Color.Silver;
            this.AlarmTimeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AlarmTimeTextBox.Location = new System.Drawing.Point(193, 25);
            this.AlarmTimeTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AlarmTimeTextBox.Multiline = true;
            this.AlarmTimeTextBox.Name = "AlarmTimeTextBox";
            this.AlarmTimeTextBox.Size = new System.Drawing.Size(81, 22);
            this.AlarmTimeTextBox.TabIndex = 2;
            this.AlarmTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AlarmMessageTextBox
            // 
            this.AlarmMessageTextBox.BackColor = System.Drawing.Color.Silver;
            this.AlarmMessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AlarmMessageTextBox.Location = new System.Drawing.Point(264, 67);
            this.AlarmMessageTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AlarmMessageTextBox.Multiline = true;
            this.AlarmMessageTextBox.Name = "AlarmMessageTextBox";
            this.AlarmMessageTextBox.Size = new System.Drawing.Size(172, 22);
            this.AlarmMessageTextBox.TabIndex = 3;
            this.AlarmMessageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IsSoundActiveCheckBox
            // 
            this.IsSoundActiveCheckBox.AutoSize = true;
            this.IsSoundActiveCheckBox.Font = new System.Drawing.Font("Rockwell Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsSoundActiveCheckBox.Location = new System.Drawing.Point(14, 123);
            this.IsSoundActiveCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IsSoundActiveCheckBox.Name = "IsSoundActiveCheckBox";
            this.IsSoundActiveCheckBox.Size = new System.Drawing.Size(158, 26);
            this.IsSoundActiveCheckBox.TabIndex = 5;
            this.IsSoundActiveCheckBox.Text = "Звуковой сигнал";
            this.IsSoundActiveCheckBox.UseVisualStyleBackColor = true;
            this.IsSoundActiveCheckBox.CheckedChanged += new System.EventHandler(this.IsSoundActiveCheckBox_CheckedChanged);
            // 
            // OkButton
            // 
            this.OkButton.BackColor = System.Drawing.Color.Silver;
            this.OkButton.Font = new System.Drawing.Font("Rockwell Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.Location = new System.Drawing.Point(14, 216);
            this.OkButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(154, 54);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "Применить";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.RosyBrown;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Font = new System.Drawing.Font("Rockwell Condensed", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.Location = new System.Drawing.Point(326, 216);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(119, 54);
            this.CancelBtn.TabIndex = 7;
            this.CancelBtn.Text = "Отмена";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(459, 283);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.IsSoundActiveCheckBox);
            this.Controls.Add(this.AlarmMessageTextBox);
            this.Controls.Add(this.AlarmTimeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Rockwell Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private System.Windows.Forms.CheckBox IsSoundActiveCheckBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelBtn;
    }
}