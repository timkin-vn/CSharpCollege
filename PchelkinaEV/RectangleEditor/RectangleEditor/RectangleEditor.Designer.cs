namespace RectangleEditor
{
    partial class RectangleEditor
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
            // RectangleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "RectangleEditor";
            this.Text = "Редактор прямоугольника";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RectangleEditor_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RectangleEditor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RectangleEditor_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RectangleEditor_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

