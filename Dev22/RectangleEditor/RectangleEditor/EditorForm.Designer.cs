﻿namespace RectangleEditor
{
    partial class EditorForm
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
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DoubleBuffered = true;
            this.Name = "EditorForm";
            this.Text = "Редактор прямоугольника";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.EditorForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EditorForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EditorForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EditorForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

