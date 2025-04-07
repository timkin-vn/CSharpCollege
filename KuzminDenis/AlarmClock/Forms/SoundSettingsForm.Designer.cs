namespace AlarmClock.Forms
{
    partial class SoundSettingsForm
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
            this.comboBoxMusic = new System.Windows.Forms.ComboBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxMusic
            // 
            this.comboBoxMusic.FormattingEnabled = true;
            this.comboBoxMusic.Location = new System.Drawing.Point(12, 56);
            this.comboBoxMusic.Name = "comboBoxMusic";
            this.comboBoxMusic.Size = new System.Drawing.Size(219, 21);
            this.comboBoxMusic.TabIndex = 0;
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddButton.Location = new System.Drawing.Point(237, 56);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(34, 21);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "...";
            this.AddButton.UseVisualStyleBackColor = true;
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(100, 116);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(85, 23);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Прослушать";
            this.PlayButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(186, 185);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(85, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(12, 185);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(85, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "ОК";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // SoundSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 220);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.comboBoxMusic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SoundSettingsForm";
            this.Text = "SoundSettingsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMusic;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button OKButton;
    }
}