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
            this.AwakeMessageLabel = new System.Windows.Forms.Label();
            this.AwakeButton = new System.Windows.Forms.Button();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // AwakeMessageLabel
            // 
            this.AwakeMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.AwakeMessageLabel.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AwakeMessageLabel.ForeColor = System.Drawing.Color.White;
            this.AwakeMessageLabel.Location = new System.Drawing.Point(12, 74);
            this.AwakeMessageLabel.Name = "AwakeMessageLabel";
            this.AwakeMessageLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AwakeMessageLabel.Size = new System.Drawing.Size(337, 190);
            this.AwakeMessageLabel.TabIndex = 0;
            this.AwakeMessageLabel.Text = "Будильник сработал!";
            this.AwakeMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AwakeButton
            // 
            this.AwakeButton.BackColor = System.Drawing.Color.Black;
            this.AwakeButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AwakeButton.ForeColor = System.Drawing.Color.White;
            this.AwakeButton.Location = new System.Drawing.Point(100, 389);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(159, 51);
            this.AwakeButton.TabIndex = 2;
            this.AwakeButton.Text = "Я проснулся";
            this.AwakeButton.UseVisualStyleBackColor = false;
            this.AwakeButton.Click += new System.EventHandler(this.AwakeButton_Click);
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(361, 559);
            this.ControlBox = false;
            this.Controls.Add(this.AwakeButton);
            this.Controls.Add(this.AwakeMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Просыпайся!";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label AwakeMessageLabel;
        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.Timer AwakeTimer;
    }
}