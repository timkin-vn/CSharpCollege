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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CreateRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.FillColorComboBox = new System.Windows.Forms.ToolStripSplitButton();
            this.FillColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GardientMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
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
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateRectangleButton,
            this.DeleteRectangleButton,
            this.toolStripSeparator1,
            this.toolStripSplitButton2,
            this.FillColorComboBox,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1067, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CreateRectangleButton
            // 
            this.CreateRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("CreateRectangleButton.Image")));
            this.CreateRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateRectangleButton.Name = "CreateRectangleButton";
            this.CreateRectangleButton.Size = new System.Drawing.Size(68, 24);
            this.CreateRectangleButton.Text = "Создать";
            this.CreateRectangleButton.Click += new System.EventHandler(this.CreateRectangleButton_Click);
            // 
            // DeleteRectangleButton
            // 
            this.DeleteRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRectangleButton.Image")));
            this.DeleteRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRectangleButton.Name = "DeleteRectangleButton";
            this.DeleteRectangleButton.Size = new System.Drawing.Size(69, 24);
            this.DeleteRectangleButton.Text = "Удалить";
            this.DeleteRectangleButton.Click += new System.EventHandler(this.DeleteRectangleButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(120, 24);
            this.toolStripSplitButton2.Text = "Цвет контура";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(184, 26);
            this.toolStripMenuItem3.Text = "Цвет контура";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // FillColorComboBox
            // 
            this.FillColorComboBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FillColorComboBox.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FillColorMenuItem,
            this.GardientMenuItem,
            this.textureMenuItem});
            this.FillColorComboBox.Image = ((System.Drawing.Image)(resources.GetObject("FillColorComboBox.Image")));
            this.FillColorComboBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FillColorComboBox.Name = "FillColorComboBox";
            this.FillColorComboBox.Size = new System.Drawing.Size(84, 24);
            this.FillColorComboBox.Text = "Заливка";
            // 
            // FillColorMenuItem
            // 
            this.FillColorMenuItem.Name = "FillColorMenuItem";
            this.FillColorMenuItem.Size = new System.Drawing.Size(225, 26);
            this.FillColorMenuItem.Text = "Сплошная заливка";
            this.FillColorMenuItem.Click += new System.EventHandler(this.FillColorMenuItem_Click);
            // 
            // GardientMenuItem
            // 
            this.GardientMenuItem.Name = "GardientMenuItem";
            this.GardientMenuItem.Size = new System.Drawing.Size(225, 26);
            this.GardientMenuItem.Text = "Градиент";
            this.GardientMenuItem.Click += new System.EventHandler(this.gradientMenuItem_Click);
            // 
            // textureMenuItem
            // 
            this.textureMenuItem.Name = "textureMenuItem";
            this.textureMenuItem.Size = new System.Drawing.Size(225, 26);
            this.textureMenuItem.Text = "Заливка картинкой";
            this.textureMenuItem.Click += new System.EventHandler(this.textureMenuItem_Click);
            // 
            // toolStripDropDownButton1
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
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(84, 24);
            this.toolStripDropDownButton1.Text = "Порядок";
            // 
            // MoveForwardMenuItem
            // 
            this.MoveForwardMenuItem.Name = "MoveForwardMenuItem";
            this.MoveForwardMenuItem.Size = new System.Drawing.Size(206, 26);
            this.MoveForwardMenuItem.Text = "Ближе";
            this.MoveForwardMenuItem.Click += new System.EventHandler(this.MoveForwardMenuItem_Click);
            // 
            // MoveBackwardMenuItem
            // 
            this.MoveBackwardMenuItem.Name = "MoveBackwardMenuItem";
            this.MoveBackwardMenuItem.Size = new System.Drawing.Size(206, 26);
            this.MoveBackwardMenuItem.Text = "Дальше";
            this.MoveBackwardMenuItem.Click += new System.EventHandler(this.MoveBackwardMenuItem_Click);
            // 
            // MoveToFrontMenuItem
            // 
            this.MoveToFrontMenuItem.Name = "MoveToFrontMenuItem";
            this.MoveToFrontMenuItem.Size = new System.Drawing.Size(206, 26);
            this.MoveToFrontMenuItem.Text = "Самый близкий ";
            this.MoveToFrontMenuItem.Click += new System.EventHandler(this.MoveToFrontMenuItem_Click);
            // 
            // MoveToBackMenuItem
            // 
            this.MoveToBackMenuItem.Name = "MoveToBackMenuItem";
            this.MoveToBackMenuItem.Size = new System.Drawing.Size(206, 26);
            this.MoveToBackMenuItem.Text = "Самый дальний";
            this.MoveToBackMenuItem.Click += new System.EventHandler(this.MoveToBackMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
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
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // FileCreateMenuItem
            // 
            this.FileCreateMenuItem.Name = "FileCreateMenuItem";
            this.FileCreateMenuItem.Size = new System.Drawing.Size(201, 26);
            this.FileCreateMenuItem.Text = "Создать";
            this.FileCreateMenuItem.Click += new System.EventHandler(this.FileCreateMenuItem_Click);
            // 
            // FileOpenMenuItem
            // 
            this.FileOpenMenuItem.Name = "FileOpenMenuItem";
            this.FileOpenMenuItem.Size = new System.Drawing.Size(201, 26);
            this.FileOpenMenuItem.Text = "Открыть...";
            this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.Size = new System.Drawing.Size(201, 26);
            this.FileSaveMenuItem.Text = "Сохранить";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileSaveAsMenuItem
            // 
            this.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
            this.FileSaveAsMenuItem.Size = new System.Drawing.Size(201, 26);
            this.FileSaveAsMenuItem.Text = "Сохранить как...";
            this.FileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(198, 6);
            // 
            // FileExportMenuItem
            // 
            this.FileExportMenuItem.Name = "FileExportMenuItem";
            this.FileExportMenuItem.Size = new System.Drawing.Size(201, 26);
            this.FileExportMenuItem.Text = "Экспорт...";
            this.FileExportMenuItem.Click += new System.EventHandler(this.FileExportMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 6);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(201, 26);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.ToolStripSplitButton FillColorComboBox;
        private System.Windows.Forms.ToolStripMenuItem FillColorMenuItem;
        private System.Windows.Forms.ColorDialog FillColorDialog;
        private System.Windows.Forms.ColorDialog BorderColorDialog;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem MoveForwardMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem MoveBackwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToFrontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToBackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GardientMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textureMenuItem;
    }
}

