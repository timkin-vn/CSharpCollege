namespace AlarmClock.Forms
{
    partial class AlarmListForm
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
            this.AlarmsListBox = new System.Windows.Forms.ListBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AlarmsListBox
            // 
            this.AlarmsListBox.FormattingEnabled = true;
            this.AlarmsListBox.ItemHeight = 16;
            this.AlarmsListBox.Location = new System.Drawing.Point(24, 90);
            this.AlarmsListBox.Name = "AlarmsListBox";
            this.AlarmsListBox.Size = new System.Drawing.Size(251, 132);
            this.AlarmsListBox.TabIndex = 0;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(165, 39);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(110, 30);
            this.DeleteButton.TabIndex = 1;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AlarmListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 264);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AlarmsListBox);
            this.Name = "AlarmListForm";
            this.Text = "AlarmListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AlarmsListBox;
        private System.Windows.Forms.Button DeleteButton;
    }
}