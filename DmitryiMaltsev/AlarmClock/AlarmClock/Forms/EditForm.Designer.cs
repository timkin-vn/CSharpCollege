namespace AlarmClock.Forms
{
    partial class EditForm
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.IsAlarmActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.AlarmMessageTextBox = new System.Windows.Forms.TextBox();
            this.AlarmTimeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(190, 146);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(100, 28);
            this.CancelButton.TabIndex = 15;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(61, 146);
            this.OkButton.Margin = new System.Windows.Forms.Padding(4);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 28);
            this.OkButton.TabIndex = 14;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // IsAlarmActiveCheckBox
            // 
            this.IsAlarmActiveCheckBox.AutoSize = true;
            this.IsAlarmActiveCheckBox.Location = new System.Drawing.Point(17, 89);
            this.IsAlarmActiveCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.IsAlarmActiveCheckBox.Name = "IsAlarmActiveCheckBox";
            this.IsAlarmActiveCheckBox.Size = new System.Drawing.Size(160, 20);
            this.IsAlarmActiveCheckBox.TabIndex = 12;
            this.IsAlarmActiveCheckBox.Text = "Будильник включен";
            this.IsAlarmActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // AlarmMessageTextBox
            // 
            this.AlarmMessageTextBox.Location = new System.Drawing.Point(180, 49);
            this.AlarmMessageTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.AlarmMessageTextBox.Name = "AlarmMessageTextBox";
            this.AlarmMessageTextBox.Size = new System.Drawing.Size(132, 22);
            this.AlarmMessageTextBox.TabIndex = 11;
            // 
            // AlarmTimeTextBox
            // 
            this.AlarmTimeTextBox.Location = new System.Drawing.Point(180, 17);
            this.AlarmTimeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.AlarmTimeTextBox.Name = "AlarmTimeTextBox";
            this.AlarmTimeTextBox.Size = new System.Drawing.Size(132, 22);
            this.AlarmTimeTextBox.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Напоминание:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Время срабатывания:";
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 192);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.IsAlarmActiveCheckBox);
            this.Controls.Add(this.AlarmMessageTextBox);
            this.Controls.Add(this.AlarmTimeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.CheckBox IsAlarmActiveCheckBox;
        private System.Windows.Forms.TextBox AlarmMessageTextBox;
        private System.Windows.Forms.TextBox AlarmTimeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}