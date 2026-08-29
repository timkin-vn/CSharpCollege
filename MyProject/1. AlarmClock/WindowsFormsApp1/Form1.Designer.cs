namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Settings = new System.Windows.Forms.Button();
            this.Display = new System.Windows.Forms.Label();
            this.Stop = new System.Windows.Forms.Button();
            this.Option = new System.Windows.Forms.Button();
            this.ClockTime = new System.Windows.Forms.Timer(this.components);
            this.Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Settings
            // 
            this.Settings.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Settings.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Settings.Location = new System.Drawing.Point(81, 199);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(158, 41);
            this.Settings.TabIndex = 0;
            this.Settings.Text = "Настройки";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.button1_Click);
            // 
            // Display
            // 
            this.Display.BackColor = System.Drawing.Color.Red;
            this.Display.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Display.ForeColor = System.Drawing.Color.DarkMagenta;
            this.Display.Location = new System.Drawing.Point(12, 9);
            this.Display.Name = "Display";
            this.Display.Size = new System.Drawing.Size(308, 116);
            this.Display.TabIndex = 1;
            this.Display.Text = "00:00:00";
            this.Display.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Display.Click += new System.EventHandler(this.Display_Click);
            // 
            // Stop
            // 
            this.Stop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Stop.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Stop.Location = new System.Drawing.Point(81, 137);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(158, 41);
            this.Stop.TabIndex = 2;
            this.Stop.Text = "Стоп";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.button2_Click);
            // 
            // Option
            // 
            this.Option.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Option.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Option.Location = new System.Drawing.Point(81, 259);
            this.Option.Name = "Option";
            this.Option.Size = new System.Drawing.Size(158, 41);
            this.Option.TabIndex = 3;
            this.Option.Text = "Описание";
            this.Option.UseVisualStyleBackColor = true;
            this.Option.Click += new System.EventHandler(this.Option_Click);
            // 
            // ClockTime
            // 
            this.ClockTime.Enabled = true;
            this.ClockTime.Interval = 1000;
            this.ClockTime.Tick += new System.EventHandler(this.ClockTime_Tick);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Exit.ForeColor = System.Drawing.Color.Red;
            this.Exit.Location = new System.Drawing.Point(81, 337);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(158, 41);
            this.Exit.TabIndex = 4;
            this.Exit.Text = "Выход";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 390);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Option);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Display);
            this.Controls.Add(this.Settings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Будильник";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Label Display;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Option;
        private System.Windows.Forms.Timer ClockTime;
        private System.Windows.Forms.Button Exit;
    }
}

