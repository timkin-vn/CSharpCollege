namespace Будильник.Forms
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
            this.AwakePictureBox = new System.Windows.Forms.PictureBox();
            this.AwakeButton = new System.Windows.Forms.Button();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AwakePictureBox
            // 
            this.AwakePictureBox.Location = new System.Drawing.Point(12, 12);
            this.AwakePictureBox.Name = "AwakePictureBox";
            this.AwakePictureBox.Size = new System.Drawing.Size(776, 426);
            this.AwakePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AwakePictureBox.TabIndex = 0;
            this.AwakePictureBox.TabStop = false;
            this.AwakePictureBox.Click += new System.EventHandler(this.AwakePictureBox_Click);
            // 
            // AwakeButton
            // 
            this.AwakeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AwakeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AwakeButton.Location = new System.Drawing.Point(12, 463);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(272, 63);
            this.AwakeButton.TabIndex = 1;
            this.AwakeButton.Text = "Я проснулся";
            this.AwakeButton.UseVisualStyleBackColor = true;
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            this.AwakeTimer.Tick += new System.EventHandler(this.AwakeTimer_Tick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(548, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 63);
            this.button1.TabIndex = 2;
            this.button1.Text = "Отложить на 5 минут ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 538);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AwakeButton);
            this.Controls.Add(this.AwakePictureBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AwakeForm_FormClosed);
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AwakePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox AwakePictureBox;
        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.Timer AwakeTimer;
        private System.Windows.Forms.Button button1;
    }
}