namespace GraphEditor
{
    partial class GraphEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphEditorForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.createRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.FillColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BorderColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.MoveForwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveBackwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToForegroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.WeightBorderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FigureTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.FillColorDialog = new System.Windows.Forms.ColorDialog();
            this.BorderColorDialog = new System.Windows.Forms.ColorDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAsКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.QuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileExportDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.OpacityTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.ConfirmOpacityBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createRectangleButton,
            this.deleteButton,
            this.toolStripSeparator1,
            this.toolStripSplitButton1,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.WeightBorderToolStripButton,
            this.toolStripSeparator3,
            this.FigureTypeComboBox,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.OpacityTextBox1,
            this.ConfirmOpacityBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // createRectangleButton
            // 
            this.createRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.createRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("createRectangleButton.Image")));
            this.createRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createRectangleButton.Name = "createRectangleButton";
            this.createRectangleButton.Size = new System.Drawing.Size(54, 22);
            this.createRectangleButton.Text = "Создать";
            this.createRectangleButton.Click += new System.EventHandler(this.createRectangleButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(55, 22);
            this.deleteButton.Text = "Удалить";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
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
            this.FillColorToolStripMenuItem,
            this.BorderColorToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripSplitButton1.Text = "Цвет";
            // 
            // FillColorToolStripMenuItem
            // 
            this.FillColorToolStripMenuItem.Name = "FillColorToolStripMenuItem";
            this.FillColorToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.FillColorToolStripMenuItem.Text = "Цвет заливки";
            this.FillColorToolStripMenuItem.Click += new System.EventHandler(this.FillColorToolStripMenuItem_Click);
            // 
            // BorderColorToolStripMenuItem
            // 
            this.BorderColorToolStripMenuItem.Name = "BorderColorToolStripMenuItem";
            this.BorderColorToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.BorderColorToolStripMenuItem.Text = "Цвет контура";
            this.BorderColorToolStripMenuItem.Click += new System.EventHandler(this.BorderColorToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveForwardToolStripMenuItem,
            this.MoveBackwardToolStripMenuItem,
            this.MoveToForegroundToolStripMenuItem,
            this.MoveToBackgroundToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButton1.Text = "Порядок";
            // 
            // MoveForwardToolStripMenuItem
            // 
            this.MoveForwardToolStripMenuItem.Name = "MoveForwardToolStripMenuItem";
            this.MoveForwardToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.MoveForwardToolStripMenuItem.Text = "Вперед";
            this.MoveForwardToolStripMenuItem.Click += new System.EventHandler(this.MoveForwardToolStripMenuItem_Click);
            // 
            // MoveBackwardToolStripMenuItem
            // 
            this.MoveBackwardToolStripMenuItem.Name = "MoveBackwardToolStripMenuItem";
            this.MoveBackwardToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.MoveBackwardToolStripMenuItem.Text = "Назад";
            this.MoveBackwardToolStripMenuItem.Click += new System.EventHandler(this.MoveBackwardToolStripMenuItem_Click);
            // 
            // MoveToForegroundToolStripMenuItem
            // 
            this.MoveToForegroundToolStripMenuItem.Name = "MoveToForegroundToolStripMenuItem";
            this.MoveToForegroundToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.MoveToForegroundToolStripMenuItem.Text = "На передний план";
            this.MoveToForegroundToolStripMenuItem.Click += new System.EventHandler(this.MoveToForegroundToolStripMenuItem_Click);
            // 
            // MoveToBackgroundToolStripMenuItem
            // 
            this.MoveToBackgroundToolStripMenuItem.Name = "MoveToBackgroundToolStripMenuItem";
            this.MoveToBackgroundToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.MoveToBackgroundToolStripMenuItem.Text = "На задний план";
            this.MoveToBackgroundToolStripMenuItem.Click += new System.EventHandler(this.MoveToBackgroundToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // WeightBorderToolStripButton
            // 
            this.WeightBorderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.WeightBorderToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("WeightBorderToolStripButton.Image")));
            this.WeightBorderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WeightBorderToolStripButton.Name = "WeightBorderToolStripButton";
            this.WeightBorderToolStripButton.Size = new System.Drawing.Size(110, 22);
            this.WeightBorderToolStripButton.Text = "Толщина контура";
            this.WeightBorderToolStripButton.Click += new System.EventHandler(this.WeightBorderToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // FigureTypeComboBox
            // 
            this.FigureTypeComboBox.Items.AddRange(new object[] {
            "Прямоугольник",
            "Эллипс",
            "Пятиугольник",
            "Ромб",
            "Равнобедренный Треугольник"});
            this.FigureTypeComboBox.Name = "FigureTypeComboBox";
            this.FigureTypeComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileCreateToolStripMenuItem,
            this.FileOpenToolStripMenuItem,
            this.FileSaveToolStripMenuItem,
            this.FileSaveAsКакToolStripMenuItem,
            this.toolStripMenuItem1,
            this.FileExportToolStripMenuItem,
            this.toolStripMenuItem2,
            this.QuitToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // FileCreateToolStripMenuItem
            // 
            this.FileCreateToolStripMenuItem.Name = "FileCreateToolStripMenuItem";
            this.FileCreateToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.FileCreateToolStripMenuItem.Text = "Создать";
            this.FileCreateToolStripMenuItem.Click += new System.EventHandler(this.FileCreateToolStripMenuItem_Click);
            // 
            // FileOpenToolStripMenuItem
            // 
            this.FileOpenToolStripMenuItem.Name = "FileOpenToolStripMenuItem";
            this.FileOpenToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.FileOpenToolStripMenuItem.Text = "Открыть";
            this.FileOpenToolStripMenuItem.Click += new System.EventHandler(this.FileOpenToolStripMenuItem_Click);
            // 
            // FileSaveToolStripMenuItem
            // 
            this.FileSaveToolStripMenuItem.Name = "FileSaveToolStripMenuItem";
            this.FileSaveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.FileSaveToolStripMenuItem.Text = "Сохранить";
            this.FileSaveToolStripMenuItem.Click += new System.EventHandler(this.FileSaveToolStripMenuItem_Click);
            // 
            // FileSaveAsКакToolStripMenuItem
            // 
            this.FileSaveAsКакToolStripMenuItem.Name = "FileSaveAsКакToolStripMenuItem";
            this.FileSaveAsКакToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.FileSaveAsКакToolStripMenuItem.Text = "Сохранить как";
            this.FileSaveAsКакToolStripMenuItem.Click += new System.EventHandler(this.FileSaveAsКакToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 6);
            // 
            // FileExportToolStripMenuItem
            // 
            this.FileExportToolStripMenuItem.Name = "FileExportToolStripMenuItem";
            this.FileExportToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.FileExportToolStripMenuItem.Text = "Экспорт";
            this.FileExportToolStripMenuItem.Click += new System.EventHandler(this.FileExportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(150, 6);
            // 
            // QuitToolStripMenuItem
            // 
            this.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem";
            this.QuitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.QuitToolStripMenuItem.Text = "Выход";
            this.QuitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.AboutToolStripMenuItem.Text = "О программе...";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // FileSaveDialog
            // 
            this.FileSaveDialog.Filter = "Файлы проекта рисунка|*.piczip";
            // 
            // FileOpenDialog
            // 
            this.FileOpenDialog.Filter = "Файлы проекта рисунка|*.piczip";
            // 
            // FileExportDialog
            // 
            this.FileExportDialog.Filter = "Файл PNG|*.png";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel1.Text = "Прозрачность";
            // 
            // OpacityTextBox1
            // 
            this.OpacityTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.OpacityTextBox1.MaxLength = 3;
            this.OpacityTextBox1.Name = "OpacityTextBox1";
            this.OpacityTextBox1.Size = new System.Drawing.Size(25, 25);
            this.OpacityTextBox1.Text = "255";
            // 
            // ConfirmOpacityBtn
            // 
            this.ConfirmOpacityBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ConfirmOpacityBtn.Image = ((System.Drawing.Image)(resources.GetObject("ConfirmOpacityBtn.Image")));
            this.ConfirmOpacityBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConfirmOpacityBtn.Name = "ConfirmOpacityBtn";
            this.ConfirmOpacityBtn.Size = new System.Drawing.Size(27, 22);
            this.ConfirmOpacityBtn.Text = "ОК";
            this.ConfirmOpacityBtn.Click += new System.EventHandler(this.ConfirmOpacityBtn_Click);
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
        private System.Windows.Forms.ToolStripButton createRectangleButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem FillColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BorderColorToolStripMenuItem;
        private System.Windows.Forms.ColorDialog FillColorDialog;
        private System.Windows.Forms.ColorDialog BorderColorDialog;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem MoveForwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveBackwardToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileCreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveAsКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem FileExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem QuitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog FileSaveDialog;
        private System.Windows.Forms.OpenFileDialog FileOpenDialog;
        private System.Windows.Forms.SaveFileDialog FileExportDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton WeightBorderToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem MoveToForegroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToBackgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox FigureTypeComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox OpacityTextBox1;
        private System.Windows.Forms.ToolStripButton ConfirmOpacityBtn;
    }
}

