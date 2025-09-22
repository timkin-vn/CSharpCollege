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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphEditorForm));
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
            this.CreateRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ChangeZBtn = new System.Windows.Forms.ToolStripSplitButton();
            this.MoveForwardBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveBackBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ChangeColorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BorderColorBtn = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.FileCreateMenuItem.Size = new System.Drawing.Size(162, 22);
            this.FileCreateMenuItem.Text = "Создать";
            this.FileCreateMenuItem.Click += new System.EventHandler(this.FileCreateMenuItem_Click);
            // 
            // FileOpenMenuItem
            // 
            this.FileOpenMenuItem.Name = "FileOpenMenuItem";
            this.FileOpenMenuItem.Size = new System.Drawing.Size(162, 22);
            this.FileOpenMenuItem.Text = "Открыть...";
            this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.Size = new System.Drawing.Size(162, 22);
            this.FileSaveMenuItem.Text = "Сохранить";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileSaveAsMenuItem
            // 
            this.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
            this.FileSaveAsMenuItem.Size = new System.Drawing.Size(162, 22);
            this.FileSaveAsMenuItem.Text = "Сохранить как...";
            this.FileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // FileExportMenuItem
            // 
            this.FileExportMenuItem.Name = "FileExportMenuItem";
            this.FileExportMenuItem.Size = new System.Drawing.Size(162, 22);
            this.FileExportMenuItem.Text = "Экспорт...";
            this.FileExportMenuItem.Click += new System.EventHandler(this.FileExportMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(162, 22);
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
            // CreateRectangleButton
            // 
            this.CreateRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateRectangleButton.Name = "CreateRectangleButton";
            this.CreateRectangleButton.Size = new System.Drawing.Size(54, 22);
            this.CreateRectangleButton.Text = "Создать";
            this.CreateRectangleButton.Click += new System.EventHandler(this.CreateRectangleButton_Click);
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
            // ChangeZBtn
            // 
            this.ChangeZBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ChangeZBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveForwardBtn,
            this.MoveBackBtn});
            this.ChangeZBtn.Image = ((System.Drawing.Image)(resources.GetObject("ChangeZBtn.Image")));
            this.ChangeZBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChangeZBtn.Name = "ChangeZBtn";
            this.ChangeZBtn.Size = new System.Drawing.Size(133, 22);
            this.ChangeZBtn.Text = "Изменение порядка";
            // 
            // MoveForwardBtn
            // 
            this.MoveForwardBtn.Name = "MoveForwardBtn";
            this.MoveForwardBtn.Size = new System.Drawing.Size(118, 22);
            this.MoveForwardBtn.Text = "Ближе";
            this.MoveForwardBtn.Click += new System.EventHandler(this.MoveForwardBtn_Click);
            // 
            // MoveBackBtn
            // 
            this.MoveBackBtn.Name = "MoveBackBtn";
            this.MoveBackBtn.Size = new System.Drawing.Size(118, 22);
            this.MoveBackBtn.Text = "Дальше";
            this.MoveBackBtn.Click += new System.EventHandler(this.MoveBackBtn_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateRectangleButton,
            this.DeleteRectangleButton,
            this.toolStripSeparator1,
            this.ChangeZBtn,
            this.toolStripSeparator2,
            this.ChangeColorButton,
            this.toolStripSeparator3,
            this.BorderColorBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ChangeColorButton
            // 
            this.ChangeColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ChangeColorButton.Image = ((System.Drawing.Image)(resources.GetObject("ChangeColorButton.Image")));
            this.ChangeColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChangeColorButton.Name = "ChangeColorButton";
            this.ChangeColorButton.Size = new System.Drawing.Size(83, 22);
            this.ChangeColorButton.Text = "Цвет фигуры";
            this.ChangeColorButton.Click += new System.EventHandler(this.ChangeRectangleColor_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // BorderColorBtn
            // 
            this.BorderColorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BorderColorBtn.Image = ((System.Drawing.Image)(resources.GetObject("BorderColorBtn.Image")));
            this.BorderColorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BorderColorBtn.Name = "BorderColorBtn";
            this.BorderColorBtn.Size = new System.Drawing.Size(84, 22);
            this.BorderColorBtn.Text = "Цвет контура";
            this.BorderColorBtn.Click += new System.EventHandler(this.BorderColorBtn_Click);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.ToolStripButton CreateRectangleButton;
        private System.Windows.Forms.ToolStripButton DeleteRectangleButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton ChangeZBtn;
        private System.Windows.Forms.ToolStripMenuItem MoveForwardBtn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ChangeColorButton;
        private System.Windows.Forms.ToolStripMenuItem MoveBackBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton BorderColorBtn;
    }
}
