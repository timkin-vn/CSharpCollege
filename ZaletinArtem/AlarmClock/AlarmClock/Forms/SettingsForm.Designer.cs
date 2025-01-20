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
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.hourInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.minuteInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(52, 117);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "ОК";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(171, 117);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // hourInput
            // 
            this.hourInput.BackColor = System.Drawing.Color.Black;
            this.hourInput.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold);
            this.hourInput.ForeColor = System.Drawing.Color.White;
            this.hourInput.Location = new System.Drawing.Point(52, 33);
            this.hourInput.Name = "hourInput";
            this.hourInput.Size = new System.Drawing.Size(75, 66);
            this.hourInput.TabIndex = 8;
            this.hourInput.Text = "00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(128, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 59);
            this.label1.TabIndex = 9;
            this.label1.Text = ":";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minuteInput
            // 
            this.minuteInput.BackColor = System.Drawing.Color.Black;
            this.minuteInput.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold);
            this.minuteInput.ForeColor = System.Drawing.Color.White;
            this.minuteInput.Location = new System.Drawing.Point(171, 33);
            this.minuteInput.Name = "minuteInput";
            this.minuteInput.Size = new System.Drawing.Size(75, 66);
            this.minuteInput.TabIndex = 10;
            this.minuteInput.Text = "00";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(298, 166);
            this.Controls.Add(this.minuteInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hourInput);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки будильника";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox hourInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox minuteInput;
    }
}