namespace AlarmClock
{
    partial class ClockForm
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
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.AboutButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.AddItem = new System.Windows.Forms.Button();
            this.ChangeItems = new System.Windows.Forms.Button();
            this.DeleteItems = new System.Windows.Forms.Button();
            this.ListAlarmGrid = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListAlarmGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.BackColor = System.Drawing.Color.Black;
            this.DisplayLabel.Font = new System.Drawing.Font("Tahoma", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DisplayLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.DisplayLabel.Location = new System.Drawing.Point(17, 16);
            this.DisplayLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(391, 102);
            this.DisplayLabel.TabIndex = 0;
            this.DisplayLabel.Text = "00:00:00";
            this.DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(416, 15);
            this.AboutButton.Margin = new System.Windows.Forms.Padding(4);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(156, 28);
            this.AboutButton.TabIndex = 1;
            this.AboutButton.Text = "О программе";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(416, 52);
            this.SettingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(156, 28);
            this.SettingsButton.TabIndex = 2;
            this.SettingsButton.Text = "Настройки";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(416, 89);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(156, 28);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ClockTimer
            // 
            this.ClockTimer.Enabled = true;
            this.ClockTimer.Interval = 1000;
            this.ClockTimer.Tick += new System.EventHandler(this.ClockTimer_Tick);
            // 
            // AddItem
            // 
            this.AddItem.Location = new System.Drawing.Point(82, 160);
            this.AddItem.Name = "AddItem";
            this.AddItem.Size = new System.Drawing.Size(87, 23);
            this.AddItem.TabIndex = 5;
            this.AddItem.Text = "Добавить";
            this.AddItem.UseVisualStyleBackColor = true;
            this.AddItem.Click += new System.EventHandler(this.AddItem_Click);
            // 
            // ChangeItems
            // 
            this.ChangeItems.Location = new System.Drawing.Point(257, 160);
            this.ChangeItems.Name = "ChangeItems";
            this.ChangeItems.Size = new System.Drawing.Size(81, 23);
            this.ChangeItems.TabIndex = 6;
            this.ChangeItems.Text = "Изменить";
            this.ChangeItems.UseVisualStyleBackColor = true;
            this.ChangeItems.Click += new System.EventHandler(this.ChangeItems_Click);
            // 
            // DeleteItems
            // 
            this.DeleteItems.Location = new System.Drawing.Point(416, 160);
            this.DeleteItems.Name = "DeleteItems";
            this.DeleteItems.Size = new System.Drawing.Size(79, 23);
            this.DeleteItems.TabIndex = 7;
            this.DeleteItems.Text = "Удалить";
            this.DeleteItems.UseVisualStyleBackColor = true;
            this.DeleteItems.Click += new System.EventHandler(this.DeleteItems_Click);
            // 
            // ListAlarmGrid
            // 
            this.ListAlarmGrid.AllowUserToAddRows = false;
            this.ListAlarmGrid.AllowUserToDeleteRows = false;
            this.ListAlarmGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListAlarmGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Message,
            this.Active});
            this.ListAlarmGrid.Location = new System.Drawing.Point(34, 189);
            this.ListAlarmGrid.MultiSelect = false;
            this.ListAlarmGrid.Name = "ListAlarmGrid";
            this.ListAlarmGrid.ReadOnly = true;
            this.ListAlarmGrid.RowHeadersWidth = 51;
            this.ListAlarmGrid.RowTemplate.Height = 24;
            this.ListAlarmGrid.Size = new System.Drawing.Size(538, 132);
            this.ListAlarmGrid.TabIndex = 8;
            // 
            // Time
            // 
            this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Time.HeaderText = "Time";
            this.Time.MinimumWidth = 6;
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Message
            // 
            this.Message.HeaderText = "Message";
            this.Message.MinimumWidth = 6;
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            this.Message.Width = 125;
            // 
            // Active
            // 
            this.Active.HeaderText = "Active";
            this.Active.MinimumWidth = 6;
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            this.Active.Width = 125;
            // 
            // ClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 333);
            this.Controls.Add(this.ListAlarmGrid);
            this.Controls.Add(this.DeleteItems);
            this.Controls.Add(this.ChangeItems);
            this.Controls.Add(this.AddItem);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.DisplayLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClockForm";
            this.Text = "Будильник";
            this.Load += new System.EventHandler(this.ClockForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListAlarmGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.Button AddItem;
        private System.Windows.Forms.Button ChangeItems;
        private System.Windows.Forms.Button DeleteItems;
        private System.Windows.Forms.DataGridView ListAlarmGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
    }
}

