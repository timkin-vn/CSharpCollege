namespace AlarmClock.Forms
{
    partial class StopwatchForm
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
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.StopwatchTimer = new System.Windows.Forms.Timer(this.components);
            this.StopwatchText = new System.Windows.Forms.Label();
            this.ButtonLap = new System.Windows.Forms.Button();
            this.StopwatchListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // StopWatchLabel
            // 
            this.StopWatchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopWatchLabel.Location = new System.Drawing.Point(113, 9);
            this.StopWatchLabel.Name = "StopWatchLabel";
            this.StopWatchLabel.Size = new System.Drawing.Size(177, 39);
            this.StopWatchLabel.TabIndex = 0;
            this.StopWatchLabel.Text = "Секундомер";
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(56, 295);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(64, 37);
            this.ButtonStart.TabIndex = 1;
            this.ButtonStart.Text = "Старт";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.Location = new System.Drawing.Point(134, 295);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(64, 37);
            this.ButtonStop.TabIndex = 2;
            this.ButtonStop.Text = "Стоп";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ButtonReset
            // 
            this.ButtonReset.Location = new System.Drawing.Point(212, 295);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(64, 37);
            this.ButtonReset.TabIndex = 3;
            this.ButtonReset.Text = "Сброс";
            this.ButtonReset.UseVisualStyleBackColor = true;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // StopwatchTimer
            // 
            this.StopwatchTimer.Interval = 1000;
            this.StopwatchTimer.Tick += new System.EventHandler(this.StopwatchTimer_Tick);
            // 
            // StopwatchText
            // 
            this.StopwatchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 54.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopwatchText.Location = new System.Drawing.Point(42, 66);
            this.StopwatchText.Name = "StopwatchText";
            this.StopwatchText.Size = new System.Drawing.Size(327, 107);
            this.StopwatchText.TabIndex = 4;
            this.StopwatchText.Text = "00:00:00";
            // 
            // ButtonLap
            // 
            this.ButtonLap.Location = new System.Drawing.Point(291, 295);
            this.ButtonLap.Name = "ButtonLap";
            this.ButtonLap.Size = new System.Drawing.Size(64, 37);
            this.ButtonLap.TabIndex = 5;
            this.ButtonLap.Text = "Интервал";
            this.ButtonLap.UseVisualStyleBackColor = true;
            this.ButtonLap.Click += new System.EventHandler(this.ButtonLap_Click);
            // 
            // StopwatchListBox
            // 
            this.StopwatchListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopwatchListBox.FormattingEnabled = true;
            this.StopwatchListBox.ItemHeight = 24;
            this.StopwatchListBox.Location = new System.Drawing.Point(56, 164);
            this.StopwatchListBox.Name = "StopwatchListBox";
            this.StopwatchListBox.Size = new System.Drawing.Size(299, 100);
            this.StopwatchListBox.TabIndex = 6;
            // 
            // StopwatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 368);
            this.Controls.Add(this.StopwatchListBox);
            this.Controls.Add(this.ButtonLap);
            this.Controls.Add(this.StopwatchText);
            this.Controls.Add(this.ButtonReset);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.StopWatchLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StopwatchForm";
            this.Text = "Секундомер";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label StopWatchLabel;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.Button ButtonReset;
        private System.Windows.Forms.Timer StopwatchTimer;
        private System.Windows.Forms.Label StopwatchText;
        private System.Windows.Forms.Button ButtonLap;
        private System.Windows.Forms.ListBox StopwatchListBox;
    }
}