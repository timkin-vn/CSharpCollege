namespace GraphEditor {
    sealed partial class GraphEditorForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphEditorForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CreateRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.FillColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.MoveForwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileCreateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExportSvgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExportJsonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileExportDialog = new System.Windows.Forms.SaveFileDialog();
            this.FillColorDialog = new System.Windows.Forms.ColorDialog();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            
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
            
            this.SetTextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetTextMenuItem.Name = "SetTextMenuItem";
            this.SetTextMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SetTextMenuItem.Text = "Задать текст...";
            this.SetTextMenuItem.Click += new System.EventHandler(this.SetTextMenuItem_Click);

            this.GroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupMenuItem.Name = "GroupMenuItem";
            this.GroupMenuItem.Size = new System.Drawing.Size(180, 22);
            this.GroupMenuItem.Text = "Сгруппировать";
            this.GroupMenuItem.Click += new System.EventHandler(this.GroupMenuItem_Click);

            this.UngroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UngroupMenuItem.Name = "UngroupMenuItem";
            this.UngroupMenuItem.Size = new System.Drawing.Size(180, 22);
            this.UngroupMenuItem.Text = "Разгруппировать";
            this.UngroupMenuItem.Click += new System.EventHandler(this.UngroupMenuItem_Click);
            
            this.CreateRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CreateRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("CreateRectangleButton.Image")));
            this.CreateRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateRectangleButton.Name = "CreateRectangleButton";
            this.CreateRectangleButton.Size = new System.Drawing.Size(54, 22);
            this.CreateRectangleButton.Text = "Создать";
            this.CreateRectangleButton.Click += new System.EventHandler(this.CreateRectangleButton_Click);

            this.DeleteRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRectangleButton.Image")));
            this.DeleteRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRectangleButton.Name = "DeleteRectangleButton";
            this.DeleteRectangleButton.Size = new System.Drawing.Size(55, 22);
            this.DeleteRectangleButton.Text = "Удалить";
            this.DeleteRectangleButton.Click += new System.EventHandler(this.DeleteRectangleButton_Click);
            
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            
            this.BorderColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderColorMenuItem.Name = "BorderColorMenuItem";
            this.BorderColorMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BorderColorMenuItem.Text = "Цвет границы...";
            this.BorderColorMenuItem.Click += new System.EventHandler(this.BorderColorMenuItem_Click);

            this.BorderWidthMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidthMenuItem.Name = "BorderWidthMenuItem";
            this.BorderWidthMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BorderWidthMenuItem.Text = "Толщина границы";

            this.BorderWidth1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidth1MenuItem.Name = "BorderWidth1MenuItem";
            this.BorderWidth1MenuItem.Size = new System.Drawing.Size(100, 22);
            this.BorderWidth1MenuItem.Text = "1 px";
            this.BorderWidth1MenuItem.Click += new System.EventHandler(this.BorderWidth1MenuItem_Click);

            this.BorderWidth2MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidth2MenuItem.Name = "BorderWidth2MenuItem";
            this.BorderWidth2MenuItem.Size = new System.Drawing.Size(100, 22);
            this.BorderWidth2MenuItem.Text = "2 px";
            this.BorderWidth2MenuItem.Click += new System.EventHandler(this.BorderWidth2MenuItem_Click);

            this.BorderWidth3MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidth3MenuItem.Name = "BorderWidth3MenuItem";
            this.BorderWidth3MenuItem.Size = new System.Drawing.Size(100, 22);
            this.BorderWidth3MenuItem.Text = "3 px";
            this.BorderWidth3MenuItem.Click += new System.EventHandler(this.BorderWidth3MenuItem_Click);

            this.BorderWidth5MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderWidth5MenuItem.Name = "BorderWidth5MenuItem";
            this.BorderWidth5MenuItem.Size = new System.Drawing.Size(100, 22);
            this.BorderWidth5MenuItem.Text = "5 px";
            this.BorderWidth5MenuItem.Click += new System.EventHandler(this.BorderWidth5MenuItem_Click);

            this.BorderWidthMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.BorderWidth1MenuItem,
                this.BorderWidth2MenuItem,
                this.BorderWidth3MenuItem,
                this.BorderWidth5MenuItem
            });

            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FillColorMenuItem,
            this.BorderColorMenuItem,
            this.BorderWidthMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripSplitButton1.Text = "Цвет";

            this.FillColorMenuItem.Name = "FillColorMenuItem";
            this.FillColorMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FillColorMenuItem.Text = "Цвет заливки...";
            this.FillColorMenuItem.Click += new System.EventHandler(this.FillColorMenuItem_Click);

            this.BorderColorDialog = new System.Windows.Forms.ColorDialog();
           
            this.AlignMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlignMenuItem.Name = "AlignMenuItem";
            this.AlignMenuItem.Size = new System.Drawing.Size(180, 22);
            this.AlignMenuItem.Text = "Выравнивание";

            this.AlignLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlignLeftMenuItem.Name = "AlignLeftMenuItem";
            this.AlignLeftMenuItem.Size = new System.Drawing.Size(200, 22);
            this.AlignLeftMenuItem.Text = "По левому краю";
            this.AlignLeftMenuItem.Click += new System.EventHandler(this.AlignLeftMenuItem_Click);

            this.AlignRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlignRightMenuItem.Name = "AlignRightMenuItem";
            this.AlignRightMenuItem.Size = new System.Drawing.Size(200, 22);
            this.AlignRightMenuItem.Text = "По правому краю";
            this.AlignRightMenuItem.Click += new System.EventHandler(this.AlignRightMenuItem_Click);

            this.AlignTopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlignTopMenuItem.Name = "AlignTopMenuItem";
            this.AlignTopMenuItem.Size = new System.Drawing.Size(200, 22);
            this.AlignTopMenuItem.Text = "По верхнему краю";
            this.AlignTopMenuItem.Click += new System.EventHandler(this.AlignTopMenuItem_Click);

            this.AlignBottomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlignBottomMenuItem.Name = "AlignBottomMenuItem";
            this.AlignBottomMenuItem.Size = new System.Drawing.Size(200, 22);
            this.AlignBottomMenuItem.Text = "По нижнему краю";
            this.AlignBottomMenuItem.Click += new System.EventHandler(this.AlignBottomMenuItem_Click);

            this.AlignCenterHorizontalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlignCenterHorizontalMenuItem.Name = "AlignCenterHorizontalMenuItem";
            this.AlignCenterHorizontalMenuItem.Size = new System.Drawing.Size(200, 22);
            this.AlignCenterHorizontalMenuItem.Text = "По центру (гориз.)";
            this.AlignCenterHorizontalMenuItem.Click += new System.EventHandler(this.AlignCenterHorizontalMenuItem_Click);

            this.AlignCenterVerticalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlignCenterVerticalMenuItem.Name = "AlignCenterVerticalMenuItem";
            this.AlignCenterVerticalMenuItem.Size = new System.Drawing.Size(200, 22);
            this.AlignCenterVerticalMenuItem.Text = "По центру (верт.)";
            this.AlignCenterVerticalMenuItem.Click += new System.EventHandler(this.AlignCenterVerticalMenuItem_Click);

            this.AlignSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.AlignSeparator.Name = "AlignSeparator";
            this.AlignSeparator.Size = new System.Drawing.Size(197, 6);

            this.DistributeHorizontallyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DistributeHorizontallyMenuItem.Name = "DistributeHorizontallyMenuItem";
            this.DistributeHorizontallyMenuItem.Size = new System.Drawing.Size(200, 22);
            this.DistributeHorizontallyMenuItem.Text = "Распределить (гориз.)";
            this.DistributeHorizontallyMenuItem.Click += new System.EventHandler(this.DistributeHorizontallyMenuItem_Click);

            this.DistributeVerticallyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DistributeVerticallyMenuItem.Name = "DistributeVerticallyMenuItem";
            this.DistributeVerticallyMenuItem.Size = new System.Drawing.Size(200, 22);
            this.DistributeVerticallyMenuItem.Text = "Распределить (верт.)";
            this.DistributeVerticallyMenuItem.Click += new System.EventHandler(this.DistributeVerticallyMenuItem_Click);

            this.AlignMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.AlignLeftMenuItem,
                this.AlignRightMenuItem,
                this.AlignTopMenuItem,
                this.AlignBottomMenuItem,
                this.AlignCenterHorizontalMenuItem,
                this.AlignCenterVerticalMenuItem,
                this.AlignSeparator,
                this.DistributeHorizontallyMenuItem,
                this.DistributeVerticallyMenuItem
            });

            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveForwardMenuItem,
            this.SetTextMenuItem,
            this.GroupMenuItem,
            this.UngroupMenuItem,
            this.AlignMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButton1.Text = "Порядок";
         
            this.MoveForwardMenuItem.Name = "MoveForwardMenuItem";
            this.MoveForwardMenuItem.Size = new System.Drawing.Size(110, 22);
            this.MoveForwardMenuItem.Text = "Ближе";
            this.MoveForwardMenuItem.Click += new System.EventHandler(this.MoveForwardMenuItem_Click);
       
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
          
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileCreateMenuItem,
            this.FileOpenMenuItem,
            this.FileSaveMenuItem,
            this.FileSaveAsMenuItem,
            this.toolStripMenuItem1,
            this.FileExportMenuItem,
            this.FileExportSvgMenuItem,
            this.FileExportJsonMenuItem,
            this.toolStripMenuItem2,
            this.FileExitMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
          
            this.FileCreateMenuItem.Name = "FileCreateMenuItem";
            this.FileCreateMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FileCreateMenuItem.Text = "Создать";
            this.FileCreateMenuItem.Click += new System.EventHandler(this.FileCreateMenuItem_Click);
          
            this.FileOpenMenuItem.Name = "FileOpenMenuItem";
            this.FileOpenMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FileOpenMenuItem.Text = "Открыть...";
            this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
         
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FileSaveMenuItem.Text = "Сохранить";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
        
            this.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
            this.FileSaveAsMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FileSaveAsMenuItem.Text = "Сохранить как...";
            this.FileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
   
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
    
            this.FileExportMenuItem.Name = "FileExportMenuItem";
            this.FileExportMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FileExportMenuItem.Text = "Экспорт...";
            this.FileExportMenuItem.Click += new System.EventHandler(this.FileExportMenuItem_Click);
            
            this.FileExportSvgMenuItem.Name = "FileExportSvgMenuItem";
            this.FileExportSvgMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FileExportSvgMenuItem.Text = "Экспорт в SVG...";
            this.FileExportSvgMenuItem.Click += new System.EventHandler(this.FileExportSvgMenuItem_Click);
            
            this.FileExportJsonMenuItem.Name = "FileExportJsonMenuItem";
            this.FileExportJsonMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FileExportJsonMenuItem.Text = "Экспорт в JSON...";
            this.FileExportJsonMenuItem.Click += new System.EventHandler(this.FileExportJsonMenuItem_Click);
       
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
      
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FileExitMenuItem.Text = "Выход";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
       
            this.FileOpenDialog.Filter = "Файлы рисунка|*.pict|Все файлы|*.*";
        
            this.FileSaveDialog.Filter = "Файлы рисунка|*.pict|Все файлы|*.*";
       
            this.FileExportDialog.Filter = "Файлы PNG|*.png";
     
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
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseDoubleClick);
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
        private System.Windows.Forms.ToolStripMenuItem SetTextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GroupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UngroupMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem FileExportSvgMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExportJsonMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.OpenFileDialog FileOpenDialog;
        private System.Windows.Forms.SaveFileDialog FileSaveDialog;
        private System.Windows.Forms.SaveFileDialog FileExportDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem FillColorMenuItem;
        private System.Windows.Forms.ColorDialog FillColorDialog;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem MoveForwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderColorMenuItem;
        private System.Windows.Forms.ColorDialog BorderColorDialog;
        private System.Windows.Forms.ToolStripMenuItem BorderWidthMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth1MenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth2MenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth3MenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderWidth5MenuItem;
        private System.Windows.Forms.ToolStripMenuItem AlignMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AlignLeftMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AlignRightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AlignTopMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AlignBottomMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AlignCenterHorizontalMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AlignCenterVerticalMenuItem;
        private System.Windows.Forms.ToolStripSeparator AlignSeparator;
        private System.Windows.Forms.ToolStripMenuItem DistributeHorizontallyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DistributeVerticallyMenuItem;
    }
}
