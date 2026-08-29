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
            this.AwakeButton = new System.Windows.Forms.Button();
            this.AwakeLabel = new System.Windows.Forms.Label();
            this.AwakePicture = new System.Windows.Forms.PictureBox();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // AwakeButton
            // 
            this.AwakeButton.Location = new System.Drawing.Point(113, 415);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(107, 23);
            this.AwakeButton.TabIndex = 0;
            this.AwakeButton.Text = "Я проснулся";
            this.AwakeButton.UseVisualStyleBackColor = true;
            this.AwakeButton.Click += new System.EventHandler(this.AwakeButton_Click);
            // 
            // AwakeLabel
            // 
            this.AwakeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AwakeLabel.Location = new System.Drawing.Point(12, 9);
            this.AwakeLabel.Name = "AwakeLabel";
            this.AwakeLabel.Size = new System.Drawing.Size(307, 75);
            this.AwakeLabel.TabIndex = 1;
            this.AwakeLabel.Text = "Просыпайся!";
            this.AwakeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AwakePicture
            // 
            this.AwakePicture.Location = new System.Drawing.Point(12, 87);
            this.AwakePicture.Name = "AwakePicture";
            this.AwakePicture.Size = new System.Drawing.Size(307, 322);
            this.AwakePicture.TabIndex = 2;
            this.AwakePicture.TabStop = false;
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 450);
            this.ControlBox = false;
            this.Controls.Add(this.AwakePicture);
            this.Controls.Add(this.AwakeLabel);
            this.Controls.Add(this.AwakeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.Text = "Просыпайся!";
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.Label AwakeLabel;
        private System.Windows.Forms.PictureBox AwakePicture;
        private System.Windows.Forms.Timer AwakeTimer;
    }
}