namespace AlarmClock.Forms
{
    partial class AwakeForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox awakePictureBox;
        private System.Windows.Forms.Button awakeButton;
        private System.Windows.Forms.Timer awakeTimer;  

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
            this.components = new System.ComponentModel.Container();
            this.awakePictureBox = new System.Windows.Forms.PictureBox();
            this.awakeButton = new System.Windows.Forms.Button();
            this.awakeTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.awakePictureBox)).BeginInit();
            this.SuspendLayout();

            this.awakePictureBox.Location = new System.Drawing.Point(12, 12);
            this.awakePictureBox.Name = "awakePictureBox";
            this.awakePictureBox.Size = new System.Drawing.Size(573, 333);
            this.awakePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.awakePictureBox.TabIndex = 0;
            this.awakePictureBox.TabStop = false;
            this.awakePictureBox.Click += new System.EventHandler(this.AwakePictureBox_Click);

            this.awakeButton.Location = new System.Drawing.Point(240, 351);
            this.awakeButton.Name = "awakeButton";
            this.awakeButton.Size = new System.Drawing.Size(113, 23);
            this.awakeButton.TabIndex = 1;
            this.awakeButton.Text = "Я проснулся";
            this.awakeButton.UseVisualStyleBackColor = true;

            this.awakeTimer.Enabled = true;
            this.awakeTimer.Interval = 5000;
            this.awakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 386);
            this.Controls.Add(this.awakeButton);
            this.Controls.Add(this.awakePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.Text = "Будильник";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AwakeForm_FormClosed);
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.awakePictureBox)).EndInit();
            this.ResumeLayout(false);
        }
    }
}