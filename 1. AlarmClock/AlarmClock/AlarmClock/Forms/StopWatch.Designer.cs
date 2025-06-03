namespace AlarmClock.Forms
{
    partial class StopWatch
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
            this.components = new System.ComponentModel.Container();
            this.StopWatchLabel = new System.Windows.Forms.Label();
            this.StopWatchStartPauseButton = new System.Windows.Forms.Button();
            this.StopWatchResetButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.StopWatchIntervalButton = new System.Windows.Forms.Button();
            this.StopWatchIntervalListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // StopWatchLabel
            // 
            this.StopWatchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopWatchLabel.Location = new System.Drawing.Point(57, 22);
            this.StopWatchLabel.Name = "StopWatchLabel";
            this.StopWatchLabel.Size = new System.Drawing.Size(144, 26);
            this.StopWatchLabel.TabIndex = 1;
            this.StopWatchLabel.Text = "00:00:000";
            // 
            // StopWatchStartPauseButton
            // 
            this.StopWatchStartPauseButton.Location = new System.Drawing.Point(238, 105);
            this.StopWatchStartPauseButton.Name = "StopWatchStartPauseButton";
            this.StopWatchStartPauseButton.Size = new System.Drawing.Size(75, 23);
            this.StopWatchStartPauseButton.TabIndex = 2;
            this.StopWatchStartPauseButton.Text = "Старт/Стоп";
            this.StopWatchStartPauseButton.UseVisualStyleBackColor = true;
            this.StopWatchStartPauseButton.Click += new System.EventHandler(this.StopWatchStartPauseButton_Click);
            // 
            // StopWatchResetButton
            // 
            this.StopWatchResetButton.Location = new System.Drawing.Point(238, 134);
            this.StopWatchResetButton.Name = "StopWatchResetButton";
            this.StopWatchResetButton.Size = new System.Drawing.Size(75, 23);
            this.StopWatchResetButton.TabIndex = 3;
            this.StopWatchResetButton.Text = "Сброс";
            this.StopWatchResetButton.UseVisualStyleBackColor = true;
            this.StopWatchResetButton.Click += new System.EventHandler(this.StopWatchResetButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StopWatchIntervalButton
            // 
            this.StopWatchIntervalButton.Location = new System.Drawing.Point(238, 76);
            this.StopWatchIntervalButton.Name = "StopWatchIntervalButton";
            this.StopWatchIntervalButton.Size = new System.Drawing.Size(75, 23);
            this.StopWatchIntervalButton.TabIndex = 4;
            this.StopWatchIntervalButton.Text = "Интервал";
            this.StopWatchIntervalButton.UseVisualStyleBackColor = true;
            this.StopWatchIntervalButton.Click += new System.EventHandler(this.StopWatchIntervalButton_Click);
            // 
            // StopWatchIntervalListBox
            // 
            this.StopWatchIntervalListBox.FormattingEnabled = true;
            this.StopWatchIntervalListBox.Location = new System.Drawing.Point(12, 51);
            this.StopWatchIntervalListBox.Name = "StopWatchIntervalListBox";
            this.StopWatchIntervalListBox.Size = new System.Drawing.Size(210, 134);
            this.StopWatchIntervalListBox.TabIndex = 5;
            // 
            // StopWatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 192);
            this.Controls.Add(this.StopWatchIntervalListBox);
            this.Controls.Add(this.StopWatchIntervalButton);
            this.Controls.Add(this.StopWatchResetButton);
            this.Controls.Add(this.StopWatchStartPauseButton);
            this.Controls.Add(this.StopWatchLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StopWatch";
            this.Text = "Секундомер";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label StopWatchLabel;
        private System.Windows.Forms.Button StopWatchStartPauseButton;
        private System.Windows.Forms.Button StopWatchResetButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button StopWatchIntervalButton;
        private System.Windows.Forms.ListBox StopWatchIntervalListBox;
    }
}