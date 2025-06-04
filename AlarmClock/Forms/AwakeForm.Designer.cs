
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
            this.AwakeMessage = new System.Windows.Forms.Label();
            this.AwakeTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AwakeButton
            // 
            this.AwakeButton.BackColor = System.Drawing.Color.Lavender;
            this.AwakeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.AwakeButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.AwakeButton.Location = new System.Drawing.Point(7, 158);
            this.AwakeButton.Margin = new System.Windows.Forms.Padding(4);
            this.AwakeButton.Name = "AwakeButton";
            this.AwakeButton.Size = new System.Drawing.Size(290, 46);
            this.AwakeButton.TabIndex = 0;
            this.AwakeButton.Text = "Выключить будильник";
            this.AwakeButton.UseVisualStyleBackColor = false;
            this.AwakeButton.Click += new System.EventHandler(this.AwakeButton_Click);
            // 
            // AwakeMessage
            // 
            this.AwakeMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.AwakeMessage.Location = new System.Drawing.Point(8, 9);
            this.AwakeMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AwakeMessage.Name = "AwakeMessage";
            this.AwakeMessage.Size = new System.Drawing.Size(288, 89);
            this.AwakeMessage.TabIndex = 1;
            this.AwakeMessage.Text = "Сработал будильник!";
            this.AwakeMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AwakeMessage.Click += new System.EventHandler(this.AwakeMessage_Click);
            // 
            // AwakeTimer
            // 
            this.AwakeTimer.Enabled = true;
            this.AwakeTimer.Interval = 5000;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(8, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Время сейчас:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Lavender;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button1.Location = new System.Drawing.Point(7, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(289, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "Отложить на 5 минут";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(9, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(289, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "00:00";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // AwakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(309, 306);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AwakeMessage);
            this.Controls.Add(this.AwakeButton);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AwakeForm";
            this.Text = "Просыпайся!";
            this.Load += new System.EventHandler(this.AwakeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AwakeButton;
        private System.Windows.Forms.Label AwakeMessage;
        private System.Windows.Forms.Timer AwakeTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    }
}
