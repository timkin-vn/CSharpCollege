namespace AlarmClock {
    partial class frmAlarmMain {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlarmMain));
            this.tmrAlarm = new System.Windows.Forms.Timer(this.components);
            this.pnlClock = new System.Windows.Forms.Panel();
            this.clbAlarms = new System.Windows.Forms.CheckedListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.dtpAlarm = new System.Windows.Forms.DateTimePicker();
            this.cbxSound = new System.Windows.Forms.CheckBox();
            this.lblDigitalClock = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.cbxSelectAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tmrAlarm
            // 
            this.tmrAlarm.Interval = 1000;
            this.tmrAlarm.Tick += new System.EventHandler(this.tmrAlarm_Tick);
            // 
            // pnlClock
            // 
            this.pnlClock.Location = new System.Drawing.Point(336, 34);
            this.pnlClock.Margin = new System.Windows.Forms.Padding(2);
            this.pnlClock.Name = "pnlClock";
            this.pnlClock.Size = new System.Drawing.Size(225, 184);
            this.pnlClock.TabIndex = 0;
            // 
            // clbAlarms
            // 
            this.clbAlarms.CheckOnClick = true;
            this.clbAlarms.FormattingEnabled = true;
            this.clbAlarms.Location = new System.Drawing.Point(12, 34);
            this.clbAlarms.Name = "clbAlarms";
            this.clbAlarms.Size = new System.Drawing.Size(319, 184);
            this.clbAlarms.TabIndex = 2;
            this.clbAlarms.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAlarms_ItemCheck);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(256, 227);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(154, 8);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(177, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Удалить выбранные элементы";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // dtpAlarm
            // 
            this.dtpAlarm.CustomFormat = "";
            this.dtpAlarm.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpAlarm.Location = new System.Drawing.Point(12, 230);
            this.dtpAlarm.Name = "dtpAlarm";
            this.dtpAlarm.ShowUpDown = true;
            this.dtpAlarm.Size = new System.Drawing.Size(161, 20);
            this.dtpAlarm.TabIndex = 5;
            // 
            // cbxSound
            // 
            this.cbxSound.AutoSize = true;
            this.cbxSound.Location = new System.Drawing.Point(179, 231);
            this.cbxSound.Name = "cbxSound";
            this.cbxSound.Size = new System.Drawing.Size(72, 17);
            this.cbxSound.TabIndex = 6;
            this.cbxSound.Text = "Звук 🔔";
            this.cbxSound.UseVisualStyleBackColor = true;
            // 
            // lblDigitalClock
            // 
            this.lblDigitalClock.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDigitalClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDigitalClock.Location = new System.Drawing.Point(337, 8);
            this.lblDigitalClock.Name = "lblDigitalClock";
            this.lblDigitalClock.Size = new System.Drawing.Size(223, 22);
            this.lblDigitalClock.TabIndex = 7;
            this.lblDigitalClock.Text = "__:__:__";
            this.lblDigitalClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(336, 227);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(225, 23);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Остановить будильник";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cbxSelectAll
            // 
            this.cbxSelectAll.AutoSize = true;
            this.cbxSelectAll.Enabled = false;
            this.cbxSelectAll.Location = new System.Drawing.Point(12, 12);
            this.cbxSelectAll.Name = "cbxSelectAll";
            this.cbxSelectAll.Size = new System.Drawing.Size(69, 17);
            this.cbxSelectAll.TabIndex = 9;
            this.cbxSelectAll.Text = "Выбрать всё";
            this.cbxSelectAll.UseVisualStyleBackColor = true;
            this.cbxSelectAll.Click += new System.EventHandler(this.cbxSelectAll_Click);
            // 
            // frmAlarmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 262);
            this.Controls.Add(this.cbxSelectAll);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblDigitalClock);
            this.Controls.Add(this.cbxSound);
            this.Controls.Add(this.dtpAlarm);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.clbAlarms);
            this.Controls.Add(this.pnlClock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmAlarmMain";
            this.Text = "Будильник";
            this.Load += new System.EventHandler(this.frmAlarmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrAlarm;
        private System.Windows.Forms.Panel pnlClock;
        private System.Windows.Forms.CheckedListBox clbAlarms;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DateTimePicker dtpAlarm;
        private System.Windows.Forms.CheckBox cbxSound;
        private System.Windows.Forms.Label lblDigitalClock;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox cbxSelectAll;
    }
}

