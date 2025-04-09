namespace AlarmClock.Forms
{
    partial class secyndforma
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
            this.timelabel = new System.Windows.Forms.Label();
            this.startbtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.stopbtn = new System.Windows.Forms.Button();
            this.sercalbtn = new System.Windows.Forms.Button();
            this.clerbtn = new System.Windows.Forms.Button();
            this.sec = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timelabel
            // 
            this.timelabel.AutoSize = true;
            this.timelabel.BackColor = System.Drawing.SystemColors.Menu;
            this.timelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timelabel.ForeColor = System.Drawing.SystemColors.InfoText;
            this.timelabel.Location = new System.Drawing.Point(77, 6);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(120, 31);
            this.timelabel.TabIndex = 0;
            this.timelabel.Text = "00:00:00";
            this.timelabel.Click += new System.EventHandler(this.timelabel_Click);
            // 
            // startbtn
            // 
            this.startbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.startbtn.Location = new System.Drawing.Point(3, 122);
            this.startbtn.Name = "startbtn";
            this.startbtn.Size = new System.Drawing.Size(75, 23);
            this.startbtn.TabIndex = 1;
            this.startbtn.Text = "Start";
            this.startbtn.UseVisualStyleBackColor = true;
            this.startbtn.Click += new System.EventHandler(this.startbtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.ForeColor = System.Drawing.SystemColors.Menu;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(49, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(178, 76);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // stopbtn
            // 
            this.stopbtn.Location = new System.Drawing.Point(3, 151);
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.Size = new System.Drawing.Size(75, 23);
            this.stopbtn.TabIndex = 3;
            this.stopbtn.Text = "Stop";
            this.stopbtn.UseVisualStyleBackColor = true;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // sercalbtn
            // 
            this.sercalbtn.Location = new System.Drawing.Point(198, 122);
            this.sercalbtn.Name = "sercalbtn";
            this.sercalbtn.Size = new System.Drawing.Size(75, 23);
            this.sercalbtn.TabIndex = 4;
            this.sercalbtn.Text = "Save";
            this.sercalbtn.UseVisualStyleBackColor = true;
            this.sercalbtn.Click += new System.EventHandler(this.sercalbtn_Click);
            // 
            // clerbtn
            // 
            this.clerbtn.Location = new System.Drawing.Point(198, 151);
            this.clerbtn.Name = "clerbtn";
            this.clerbtn.Size = new System.Drawing.Size(75, 23);
            this.clerbtn.TabIndex = 5;
            this.clerbtn.Text = "Clear";
            this.clerbtn.UseVisualStyleBackColor = true;
            this.clerbtn.Click += new System.EventHandler(this.clerbtn_Click);
            // 
            // sec
            // 
            this.sec.Tick += new System.EventHandler(this.sec_Tick);
            // 
            // secyndforma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(291, 210);
            this.Controls.Add(this.clerbtn);
            this.Controls.Add(this.sercalbtn);
            this.Controls.Add(this.stopbtn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.startbtn);
            this.Controls.Add(this.timelabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "secyndforma";
            this.Text = "Секундомер";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.Button startbtn;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button stopbtn;
        private System.Windows.Forms.Button sercalbtn;
        private System.Windows.Forms.Button clerbtn;
        private System.Windows.Forms.Timer sec;
    }
}