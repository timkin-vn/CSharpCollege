namespace RectangleEditor
{
    partial class RectangleEditorForm
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
            this.SuspendLayout();
            // 
            // RectangleEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "RectangleEditorForm";
            this.Text = "Редактор прямоугольника";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RectangleEditorForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RectangleEditorForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RectangleEditorForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RectangleEditorForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

