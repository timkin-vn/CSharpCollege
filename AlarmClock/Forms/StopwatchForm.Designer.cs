using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    partial class StopwatchForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label TimeLabel;
        private Button StartButton;
        private Button PauseButton;
        private Button ResetButton;
        private Button LapButton;
        private Button CloseButton;
        private ListBox LapListBox;
        private Label TitleLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TimeLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.LapButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.LapListBox = new System.Windows.Forms.ListBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TimeLabel.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.TimeLabel.Location = new System.Drawing.Point(92, 71);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(186, 36);
            this.TimeLabel.TabIndex = 1;
            this.TimeLabel.Text = "00:00:00.00";
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.White;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.StartButton.Location = new System.Drawing.Point(27, 111);
            this.StartButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(151, 38);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "Старт";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.BackColor = System.Drawing.Color.Lavender;
            this.PauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PauseButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.PauseButton.Location = new System.Drawing.Point(186, 111);
            this.PauseButton.Margin = new System.Windows.Forms.Padding(4);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(166, 37);
            this.PauseButton.TabIndex = 3;
            this.PauseButton.Text = "Пауза";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.Lavender;
            this.ResetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResetButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ResetButton.Location = new System.Drawing.Point(186, 160);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(4);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(166, 37);
            this.ResetButton.TabIndex = 4;
            this.ResetButton.Text = "Сброс";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // LapButton
            // 
            this.LapButton.BackColor = System.Drawing.Color.Lavender;
            this.LapButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LapButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.LapButton.Location = new System.Drawing.Point(27, 160);
            this.LapButton.Margin = new System.Windows.Forms.Padding(4);
            this.LapButton.Name = "LapButton";
            this.LapButton.Size = new System.Drawing.Size(151, 37);
            this.LapButton.TabIndex = 5;
            this.LapButton.Text = "Круг";
            this.LapButton.UseVisualStyleBackColor = true;
            this.LapButton.Click += new System.EventHandler(this.LapButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Lavender;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.CloseButton.Location = new System.Drawing.Point(252, 419);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(100, 37);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "Закрыть";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // LapListBox
            // 
            this.LapListBox.FormattingEnabled = true;
            this.LapListBox.ItemHeight = 16;
            this.LapListBox.Location = new System.Drawing.Point(27, 209);
            this.LapListBox.Margin = new System.Windows.Forms.Padding(4);
            this.LapListBox.Name = "LapListBox";
            this.LapListBox.Size = new System.Drawing.Size(325, 196);
            this.LapListBox.TabIndex = 7;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TitleLabel.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.TitleLabel.Location = new System.Drawing.Point(115, 9);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(138, 25);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Секундомер";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StopwatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(379, 469);
            this.Controls.Add(this.LapListBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.LapButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.TitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StopwatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stopwatch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}