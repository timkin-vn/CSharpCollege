namespace AlarmClock
{
    partial class Events
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
            this.EventList = new System.Windows.Forms.CheckedListBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.EventTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // EventList
            // 
            this.EventList.FormattingEnabled = true;
            this.EventList.Location = new System.Drawing.Point(12, 12);
            this.EventList.Name = "EventList";
            this.EventList.Size = new System.Drawing.Size(371, 418);
            this.EventList.TabIndex = 0;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(492, 96);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(226, 27);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // EventTextBox
            // 
            this.EventTextBox.Location = new System.Drawing.Point(406, 14);
            this.EventTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EventTextBox.Name = "EventTextBox";
            this.EventTextBox.Size = new System.Drawing.Size(381, 26);
            this.EventTextBox.TabIndex = 3;
            this.EventTextBox.TextChanged += new System.EventHandler(this.EventTextBox_TextChanged);
            // 
            // Events
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EventTextBox);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.EventList);
            this.Name = "Events";
            this.Text = "Events";
            this.Load += new System.EventHandler(this.Events_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox EventList;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox EventTextBox;
    }
}