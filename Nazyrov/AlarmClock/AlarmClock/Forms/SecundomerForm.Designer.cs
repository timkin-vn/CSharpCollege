namespace AlarmClock.Forms
{
    partial class TimerForm
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
            this.DisplayLabelSecundomer = new System.Windows.Forms.Label();
            this.Secundomer = new System.Windows.Forms.Timer(this.components);
            this.SecundomerOn = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.SecundomerOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DisplayLabelSecundomer
            // 
            this.DisplayLabelSecundomer.BackColor = System.Drawing.Color.Black;
            this.DisplayLabelSecundomer.Font = new System.Drawing.Font("Tahoma", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabelSecundomer.ForeColor = System.Drawing.Color.GreenYellow;
            this.DisplayLabelSecundomer.Location = new System.Drawing.Point(12, 9);
            this.DisplayLabelSecundomer.Name = "DisplayLabelSecundomer";
            this.DisplayLabelSecundomer.Size = new System.Drawing.Size(293, 83);
            this.DisplayLabelSecundomer.TabIndex = 1;
            this.DisplayLabelSecundomer.Text = "00:00:00";
            this.DisplayLabelSecundomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Secundomer
            // 
            this.Secundomer.Interval = 1000;
            this.Secundomer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SecundomerOn
            // 
            this.SecundomerOn.Location = new System.Drawing.Point(318, 26);
            this.SecundomerOn.Name = "SecundomerOn";
            this.SecundomerOn.Size = new System.Drawing.Size(93, 23);
            this.SecundomerOn.TabIndex = 2;
            this.SecundomerOn.Text = "Включить";
            this.SecundomerOn.UseVisualStyleBackColor = true;
            this.SecundomerOn.Click += new System.EventHandler(this.TimerOnOff_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(318, 84);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(93, 23);
            this.Back.TabIndex = 3;
            this.Back.Text = "Назад";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // SecundomerOff
            // 
            this.SecundomerOff.Location = new System.Drawing.Point(318, 55);
            this.SecundomerOff.Name = "SecundomerOff";
            this.SecundomerOff.Size = new System.Drawing.Size(93, 23);
            this.SecundomerOff.TabIndex = 4;
            this.SecundomerOff.Text = "Выключить";
            this.SecundomerOff.UseVisualStyleBackColor = true;
            this.SecundomerOff.Click += new System.EventHandler(this.button1_Click);
            // 
            // TimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 148);
            this.Controls.Add(this.SecundomerOff);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.SecundomerOn);
            this.Controls.Add(this.DisplayLabelSecundomer);
            this.Name = "TimerForm";
            this.Text = "Таймер";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DisplayLabelSecundomer;
        private System.Windows.Forms.Timer Secundomer;
        private System.Windows.Forms.Button SecundomerOn;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button SecundomerOff;
    }
}