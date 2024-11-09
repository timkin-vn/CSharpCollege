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
            this.AwakePictureBox = new System.Windows.Forms.PictureBox();
            this.AwakeButton = new System.Windows.Forms.Button();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AwakeMessageLabel
            // 
            this.AwakeMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AwakeMessageLabel.Location = new System.Drawing.Point(16, 16);
            this.AwakeMessageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AwakeMessageLabel.Name = "AwakeMessageLabel";
            this.AwakeMessageLabel.Size = new System.Drawing.Size(828, 91);
            this.AwakeMessageLabel.TabIndex = 0;
            this.AwakeMessageLabel.Text = "Сообщение";
            this.AwakeMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AwakeMessageLabel.Click += new System.EventHandler(this.AwakeMessageLabel_Click);
            // 
            // AwakePictureBox
            // 
            this.AwakePictureBox.Location = new System.Drawing.Point(16, 111);
            this.AwakePictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.AwakePictureBox.Name = "AwakePictureBox";
            this.AwakePictureBox.Size = new System.Drawing.Size(828, 527);
            this.AwakePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.AwakePictureBox.TabIndex = 1;
            this.AwakePictureBox.TabStop = false;
           
            // 
            // AwakeButton
            // 
            this.AwakeButton.Location = new System.Drawing.Point(369, 645);
            this.AwakeButton.Margin = new System.Windows.Forms.Padding(4);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(124, 28);
            this.AwakeButton.TabIndex = 2;
            this.AwakeButton.Text = "Я проснулся";
            this.AwakeButton.UseVisualStyleBackColor = true;
            this.AwakeButton.Click += new System.EventHandler(this.AwakeButton_Click);
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 688);
            this.ControlBox = false;
            this.Controls.Add(this.AwakeButton);
            this.Controls.Add(this.AwakePictureBox);
            this.Controls.Add(this.AwakeMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.Text = "Просыпайся!";
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label AwakeMessageLabel;
        private System.Windows.Forms.PictureBox AwakePictureBox;
        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.Timer AwakeTimer;
        private System.Windows.Forms.ImageList imageList1;
    }
}