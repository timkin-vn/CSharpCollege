namespace MouseEvents
{
    partial class MouseEventsForm
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
            this.SuspendLayout();
            // 
            // MouseEventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DoubleBuffered = true;
            this.Name = "MouseEventsForm";
            this.Text = "События мыши";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MouseEventsForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseEventsForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseEventsForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseEventsForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

