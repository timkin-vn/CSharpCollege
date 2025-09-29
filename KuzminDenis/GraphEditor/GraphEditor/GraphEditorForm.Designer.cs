﻿namespace GraphEditor
{
    partial class GraphEditorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphEditorForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CreateRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.FillColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidthMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();

            // создаём новые пункты меню
            this.MoveForwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveBackwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToFrontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToBackMenuItem = new System.Windows.Forms.ToolStripMenuItem();

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
            this.FillColorDialog = new System.Windows.Forms.ColorDialog();
            this.BorderColorDialog = new System.Windows.Forms.ColorDialog();

            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();

            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.CreateRectangleButton,
        this.DeleteRectangleButton,
        this.toolStripSeparator1,
        this.toolStripSplitButton1,
        this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CreateRectangleButton
            // 
            this.CreateRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("CreateRectangleButton.Image")));
            this.CreateRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateRectangleButton.Name = "CreateRectangleButton";
            this.CreateRectangleButton.Size = new System.Drawing.Size(54, 22);
            this.CreateRectangleButton.Text = "Создать";
            this.CreateRectangleButton.Click += new System.EventHandler(this.CreateRectangleButton_Click);
            // 
            // DeleteRectangleButton
            // 
            this.DeleteRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRectangleButton.Image")));
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
            // toolStripSplitButton1 (Фигура)
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.FillColorMenuItem,
        this.BorderColorMenuItem,
        this.BorderWidthMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(63, 22);
            this.toolStripSplitButton1.Text = "Фигура";
            // 
            // FillColorMenuItem
            // 
            this.FillColorMenuItem.Name = "FillColorMenuItem";
            this.FillColorMenuItem.Size = new System.Drawing.Size(202, 22);
            this.FillColorMenuItem.Text = "Цвет заливки фигуры...";
            this.FillColorMenuItem.Click += new System.EventHandler(this.FillColorMenuItem_Click);
            // 
            // BorderColorMenuItem
            // 
            this.BorderColorMenuItem.Name = "BorderColorMenuItem";
            this.BorderColorMenuItem.Size = new System.Drawing.Size(202, 22);
            this.BorderColorMenuItem.Text = "Цвет контура...";
            this.BorderColorMenuItem.Click += new System.EventHandler(this.BorderColorMenuItem_Click);
            // 
            // BorderWidthMenuItem
            // 
            this.BorderWidthMenuItem.Name = "BorderWidthMenuItem";
            this.BorderWidthMenuItem.Size = new System.Drawing.Size(202, 22);
            this.BorderWidthMenuItem.Text = "Толщина контура...";
            this.BorderWidthMenuItem.Click += new System.EventHandler(this.BorderWidthMenuItem_Click);
            // 
            // toolStripDropDownButton1 (Порядок)
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.MoveForwardMenuItem,
        this.MoveBackwardMenuItem,
        this.MoveToFrontMenuItem,
        this.MoveToBackMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButton1.Text = "Порядок";
            // 
            // MoveForwardMenuItem
            // 
            this.MoveForwardMenuItem.Name = "MoveForwardMenuItem";
            this.MoveForwardMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MoveForwardMenuItem.Text = "Ближе";
            this.MoveForwardMenuItem.Click += new System.EventHandler(this.MoveForwardMenuItem_Click);
            // 
            // MoveBackwardMenuItem
            // 
            this.MoveBackwardMenuItem.Name = "MoveBackwardMenuItem";
            this.MoveBackwardMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MoveBackwardMenuItem.Text = "Дальше";
            this.MoveBackwardMenuItem.Click += new System.EventHandler(this.MoveBackwardMenuItem_Click);
            // 
            // MoveToFrontMenuItem
            // 
            this.MoveToFrontMenuItem.Name = "MoveToFrontMenuItem";
            this.MoveToFrontMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MoveToFrontMenuItem.Text = "В начало";
            this.MoveToFrontMenuItem.Click += new System.EventHandler(this.MoveToFrontMenuItem_Click);
            // 
            // MoveToBackMenuItem
            // 
            this.MoveToBackMenuItem.Name = "MoveToBackMenuItem";
            this.MoveToBackMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MoveToBackMenuItem.Text = "В конец";
            this.MoveToBackMenuItem.Click += new System.EventHandler(this.MoveToBackMenuItem_Click);
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
            this.FileOpenDialog.Filter = "Файлы рисунка|*.pict|Все файлы|*.*";
            // 
            // FileSaveDialog
            // 
            this.FileSaveDialog.Filter = "Файлы рисунка|*.pict|Все файлы|*.*";
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
        private System.Windows.Forms.ToolStripButton DeleteRectangleButton;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem FillColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidthMenuItem;
        private System.Windows.Forms.ColorDialog FillColorDialog;
        private System.Windows.Forms.ColorDialog BorderColorDialog;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem MoveForwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveBackwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToFrontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToBackMenuItem;
    }
}
