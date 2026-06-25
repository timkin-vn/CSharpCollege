namespace GraphEditor
{
    partial class GraphEditorForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CreateRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.ShapeDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ShapeRectangleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShapeSquareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShapeCircleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShapeHexagonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.FillColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.MoveForwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveBackwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveToFrontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToBackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DarkThemeButton = new System.Windows.Forms.ToolStripButton();
            this.FillColorDialog = new System.Windows.Forms.ColorDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileCreateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileExportDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateRectangleButton,
            this.ShapeDropDownButton,
            this.DeleteRectangleButton,
            this.toolStripSeparator1,
            this.toolStripSplitButton1,
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this.DarkThemeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CreateRectangleButton
            // 
            this.CreateRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateRectangleButton.Name = "CreateRectangleButton";
            this.CreateRectangleButton.Size = new System.Drawing.Size(136, 22);
            this.CreateRectangleButton.Text = "Создать: Прямоугольник";
            this.CreateRectangleButton.Click += new System.EventHandler(this.CreateRectangleButton_Click);
            // 
            // ShapeDropDownButton
            // 
            this.ShapeDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ShapeDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShapeRectangleMenuItem,
            this.ShapeSquareMenuItem,
            this.ShapeCircleMenuItem,
            this.ShapeHexagonMenuItem});
            this.ShapeDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShapeDropDownButton.Name = "ShapeDropDownButton";
            this.ShapeDropDownButton.Size = new System.Drawing.Size(62, 22);
            this.ShapeDropDownButton.Text = "Фигура";
            // 
            // ShapeRectangleMenuItem
            // 
            this.ShapeRectangleMenuItem.CheckOnClick = true;
            this.ShapeRectangleMenuItem.Name = "ShapeRectangleMenuItem";
            this.ShapeRectangleMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ShapeRectangleMenuItem.Text = "Прямоугольник";
            this.ShapeRectangleMenuItem.Click += new System.EventHandler(this.ShapeRectangleMenuItem_Click);
            // 
            // ShapeSquareMenuItem
            // 
            this.ShapeSquareMenuItem.CheckOnClick = true;
            this.ShapeSquareMenuItem.Name = "ShapeSquareMenuItem";
            this.ShapeSquareMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ShapeSquareMenuItem.Text = "Квадрат";
            this.ShapeSquareMenuItem.Click += new System.EventHandler(this.ShapeSquareMenuItem_Click);
            // 
            // ShapeCircleMenuItem
            // 
            this.ShapeCircleMenuItem.CheckOnClick = true;
            this.ShapeCircleMenuItem.Name = "ShapeCircleMenuItem";
            this.ShapeCircleMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ShapeCircleMenuItem.Text = "Круг";
            this.ShapeCircleMenuItem.Click += new System.EventHandler(this.ShapeCircleMenuItem_Click);
            // 
            // ShapeHexagonMenuItem
            // 
            this.ShapeHexagonMenuItem.CheckOnClick = true;
            this.ShapeHexagonMenuItem.Name = "ShapeHexagonMenuItem";
            this.ShapeHexagonMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ShapeHexagonMenuItem.Text = "Шестиугольник";
            this.ShapeHexagonMenuItem.Click += new System.EventHandler(this.ShapeHexagonMenuItem_Click);
            // 
            // DeleteRectangleButton
            // 
            this.DeleteRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRectangleButton.Name = "DeleteRectangleButton";
            this.DeleteRectangleButton.Size = new System.Drawing.Size(55, 22);
            this.DeleteRectangleButton.Text = "Удалить";
            this.DeleteRectangleButton.Click += new System.EventHandler(this.DeleteRectangleButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FillColorMenuItem});
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripSplitButton1.Text = "Цвет";
            // 
            // FillColorMenuItem
            // 
            this.FillColorMenuItem.Name = "FillColorMenuItem";
            this.FillColorMenuItem.Size = new System.Drawing.Size(147, 22);
            this.FillColorMenuItem.Text = "Цвет заливки";
            this.FillColorMenuItem.Click += new System.EventHandler(this.FillColorMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveForwardMenuItem,
            this.MoveBackwardMenuItem,
            this.toolStripSeparator2,
            this.MoveToFrontMenuItem,
            this.MoveToBackMenuItem});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButton1.Text = "Порядок";
            // 
            // MoveForwardMenuItem
            // 
            this.MoveForwardMenuItem.Name = "MoveForwardMenuItem";
            this.MoveForwardMenuItem.Size = new System.Drawing.Size(184, 22);
            this.MoveForwardMenuItem.Text = "На шаг вперёд";
            this.MoveForwardMenuItem.Click += new System.EventHandler(this.MoveForwardMenuItem_Click);
            // 
            // MoveBackwardMenuItem
            // 
            this.MoveBackwardMenuItem.Name = "MoveBackwardMenuItem";
            this.MoveBackwardMenuItem.Size = new System.Drawing.Size(184, 22);
            this.MoveBackwardMenuItem.Text = "На шаг назад";
            this.MoveBackwardMenuItem.Click += new System.EventHandler(this.MoveBackwardMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // MoveToFrontMenuItem
            // 
            this.MoveToFrontMenuItem.Name = "MoveToFrontMenuItem";
            this.MoveToFrontMenuItem.Size = new System.Drawing.Size(184, 22);
            this.MoveToFrontMenuItem.Text = "На передний план";
            this.MoveToFrontMenuItem.Click += new System.EventHandler(this.MoveToFrontMenuItem_Click);
            // 
            // MoveToBackMenuItem
            // 
            this.MoveToBackMenuItem.Name = "MoveToBackMenuItem";
            this.MoveToBackMenuItem.Size = new System.Drawing.Size(184, 22);
            this.MoveToBackMenuItem.Text = "На задний план";
            this.MoveToBackMenuItem.Click += new System.EventHandler(this.MoveToBackMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // DarkThemeButton
            // 
            this.DarkThemeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DarkThemeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DarkThemeButton.Name = "DarkThemeButton";
            this.DarkThemeButton.Size = new System.Drawing.Size(79, 22);
            this.DarkThemeButton.Text = "Тёмная тема";
            this.DarkThemeButton.Click += new System.EventHandler(this.DarkThemeButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileCreateMenuItem,
            this.FileOpenMenuItem,
            this.FileSaveMenuItem,
            this.FileSaveAsMenuItem,
            this.toolStripMenuItem1,
            this.FileExportMenuItem,
            this.toolStripMenuItem2,
            this.FileExitMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // FileCreateMenuItem
            // 
            this.FileCreateMenuItem.Name = "FileCreateMenuItem";
            this.FileCreateMenuItem.Size = new System.Drawing.Size(163, 22);
            this.FileCreateMenuItem.Text = "Создать";
            this.FileCreateMenuItem.Click += new System.EventHandler(this.FileCreateMenuItem_Click);
            // 
            // FileOpenMenuItem
            // 
            this.FileOpenMenuItem.Name = "FileOpenMenuItem";
            this.FileOpenMenuItem.Size = new System.Drawing.Size(163, 22);
            this.FileOpenMenuItem.Text = "Открыть...";
            this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.Size = new System.Drawing.Size(163, 22);
            this.FileSaveMenuItem.Text = "Сохранить";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileSaveAsMenuItem
            // 
            this.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
            this.FileSaveAsMenuItem.Size = new System.Drawing.Size(163, 22);
            this.FileSaveAsMenuItem.Text = "Сохранить как...";
            this.FileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 6);
            // 
            // FileExportMenuItem
            // 
            this.FileExportMenuItem.Name = "FileExportMenuItem";
            this.FileExportMenuItem.Size = new System.Drawing.Size(163, 22);
            this.FileExportMenuItem.Text = "Экспорт...";
            this.FileExportMenuItem.Click += new System.EventHandler(this.FileExportMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 6);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(163, 22);
            this.FileExitMenuItem.Text = "Выход";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // FileOpenDialog
            // 
            this.FileOpenDialog.Filter = "Файлы проекта рисунка|*.piczip";
            // 
            // FileSaveDialog
            // 
            this.FileSaveDialog.Filter = "Файлы проекта рисунка|*.piczip";
            // 
            // FileExportDialog
            // 
            this.FileExportDialog.Filter = "Файлы PNG|*.png";
            // 
            // GraphEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GraphEditorForm";
            this.Text = "Графический редактор";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphEditorForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CreateRectangleButton;
        private System.Windows.Forms.ToolStripDropDownButton ShapeDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem ShapeRectangleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShapeSquareMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShapeCircleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShapeHexagonMenuItem;
        private System.Windows.Forms.ToolStripButton DeleteRectangleButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem FillColorMenuItem;
        private System.Windows.Forms.ColorDialog FillColorDialog;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem MoveForwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveBackwardMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MoveToFrontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToBackMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton DarkThemeButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileCreateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem FileExportMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.OpenFileDialog FileOpenDialog;
        private System.Windows.Forms.SaveFileDialog FileSaveDialog;
        private System.Windows.Forms.SaveFileDialog FileExportDialog;
    }
}
