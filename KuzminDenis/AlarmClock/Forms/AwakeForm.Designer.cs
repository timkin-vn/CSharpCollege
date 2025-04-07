namespace AlarmClock.Forms
{
    partial class AwakeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AwakeForm));
            this.AwakeButton = new System.Windows.Forms.Button();
            this.AwakeMessage = new System.Windows.Forms.Label();
            this.AwakePictureBox = new System.Windows.Forms.PictureBox();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.PostponeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AwakeButton
            // 
            this.AwakeButton.Location = new System.Drawing.Point(258, 341);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(75, 23);
            this.AwakeButton.TabIndex = 0;
            this.AwakeButton.Text = "Выключить";
            this.AwakeButton.UseVisualStyleBackColor = true;
            this.AwakeButton.Click += new System.EventHandler(this.AwakeButton_Click);
            // 
            // AwakeMessage
            // 
            this.AwakeMessage.BackColor = System.Drawing.Color.Black;
            this.AwakeMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AwakeMessage.ForeColor = System.Drawing.Color.Lime;
            this.AwakeMessage.Location = new System.Drawing.Point(12, 9);
            this.AwakeMessage.Name = "AwakeMessage";
            this.AwakeMessage.Size = new System.Drawing.Size(362, 80);
            this.AwakeMessage.TabIndex = 1;
            this.AwakeMessage.Text = "Wake up Samurai";
            this.AwakeMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AwakePictureBox
            // 
            this.AwakePictureBox.Location = new System.Drawing.Point(12, 108);
            this.AwakePictureBox.Name = "AwakePictureBox";
            this.AwakePictureBox.Size = new System.Drawing.Size(359, 227);
            this.AwakePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AwakePictureBox.TabIndex = 2;
            this.AwakePictureBox.TabStop = false;
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);
            // 
            // PostponeButton
            // 
            this.PostponeButton.Location = new System.Drawing.Point(52, 341);
            this.PostponeButton.Name = "PostponeButton";
            this.PostponeButton.Size = new System.Drawing.Size(75, 23);
            this.PostponeButton.TabIndex = 3;
            this.PostponeButton.Text = "Отложить";
            this.PostponeButton.UseVisualStyleBackColor = true;
            this.PostponeButton.Click += new System.EventHandler(this.PostponeButton_Click);
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(386, 376);
            this.ControlBox = false;
            this.Controls.Add(this.PostponeButton);
            this.Controls.Add(this.AwakePictureBox);
            this.Controls.Add(this.AwakeMessage);
            this.Controls.Add(this.AwakeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.Text = "Будильник сработал";
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.Label AwakeMessage;
        private System.Windows.Forms.PictureBox AwakePictureBox;
        private System.Windows.Forms.Timer AwakeTimer;
        private System.Windows.Forms.Button PostponeButton;
    }
}