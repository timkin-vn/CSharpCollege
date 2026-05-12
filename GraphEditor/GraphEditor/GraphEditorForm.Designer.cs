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
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.FillColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.BorderColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.MoveForwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveBackwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveToFrontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToBackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.GridToggleButton = new System.Windows.Forms.ToolStripButton();
            this.SnapToggleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ShadowToggleButton = new System.Windows.Forms.ToolStripButton();
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
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GridMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SnapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.FileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileExportDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.OpacityLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OpacityTrackBar = new System.Windows.Forms.TrackBar();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateRectangleButton,
            this.DeleteRectangleButton,
            this.toolStripSeparator1,
            this.toolStripSplitButton1,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.GridToggleButton,
            this.SnapToggleButton,
            this.toolStripSeparator3,
            this.ShadowToggleButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(900, 25);
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
            this.CreateRectangleButton.ToolTipText = "Создать новый прямоугольник";
            this.CreateRectangleButton.Click += new System.EventHandler(this.CreateRectangleButton_Click);
            // 
            // DeleteRectangleButton
            // 
            this.DeleteRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteRectangleButton.Enabled = false;
            this.DeleteRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRectangleButton.Image")));
            this.DeleteRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRectangleButton.Name = "DeleteRectangleButton";
            this.DeleteRectangleButton.Size = new System.Drawing.Size(55, 22);
            this.DeleteRectangleButton.Text = "Удалить";
            this.DeleteRectangleButton.ToolTipText = "Удалить выделенный прямоугольник (Delete)";
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
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripSplitButton1.Text = "Цвет";
            this.toolStripSplitButton1.ToolTipText = "Цвет заливки выделенного прямоугольника";
            // 
            // FillColorMenuItem
            // 
            this.FillColorMenuItem.Name = "FillColorMenuItem";
            this.FillColorMenuItem.Size = new System.Drawing.Size(147, 22);
            this.FillColorMenuItem.Text = "Цвет заливки";
            this.FillColorMenuItem.Click += new System.EventHandler(this.FillColorMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BorderColorMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(64, 22);
            this.toolStripDropDownButton2.Text = "Граница";
            this.toolStripDropDownButton2.ToolTipText = "Цвет границы выделенного прямоугольника";
            // 
            // BorderColorMenuItem
            // 
            this.BorderColorMenuItem.Name = "BorderColorMenuItem";
            this.BorderColorMenuItem.Size = new System.Drawing.Size(147, 22);
            this.BorderColorMenuItem.Text = "Цвет границы";
            this.BorderColorMenuItem.Click += new System.EventHandler(this.BorderColorMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveForwardMenuItem,
            this.MoveBackwardMenuItem,
            this.toolStripSeparator4,
            this.MoveToFrontMenuItem,
            this.MoveToBackMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButton1.Text = "Порядок";
            this.toolStripDropDownButton1.ToolTipText = "Управление Z-порядком фигур";
            // 
            // MoveForwardMenuItem
            // 
            this.MoveForwardMenuItem.Name = "MoveForwardMenuItem";
            this.MoveForwardMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.MoveForwardMenuItem.Size = new System.Drawing.Size(226, 22);
            this.MoveForwardMenuItem.Text = "Вперед";
            this.MoveForwardMenuItem.Click += new System.EventHandler(this.MoveForwardMenuItem_Click);
            // 
            // MoveBackwardMenuItem
            // 
            this.MoveBackwardMenuItem.Name = "MoveBackwardMenuItem";
            this.MoveBackwardMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.MoveBackwardMenuItem.Size = new System.Drawing.Size(226, 22);
            this.MoveBackwardMenuItem.Text = "Назад";
            this.MoveBackwardMenuItem.Click += new System.EventHandler(this.MoveBackwardMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(223, 6);
            // 
            // MoveToFrontMenuItem
            // 
            this.MoveToFrontMenuItem.Name = "MoveToFrontMenuItem";
            this.MoveToFrontMenuItem.Size = new System.Drawing.Size(226, 22);
            this.MoveToFrontMenuItem.Text = "На передний план";
            this.MoveToFrontMenuItem.Click += new System.EventHandler(this.MoveToFrontMenuItem_Click);
            // 
            // MoveToBackMenuItem
            // 
            this.MoveToBackMenuItem.Name = "MoveToBackMenuItem";
            this.MoveToBackMenuItem.Size = new System.Drawing.Size(226, 22);
            this.MoveToBackMenuItem.Text = "На задний план";
            this.MoveToBackMenuItem.Click += new System.EventHandler(this.MoveToBackMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // GridToggleButton
            // 
            this.GridToggleButton.CheckOnClick = true;
            this.GridToggleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.GridToggleButton.Image = ((System.Drawing.Image)(resources.GetObject("GridToggleButton.Image")));
            this.GridToggleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GridToggleButton.Name = "GridToggleButton";
            this.GridToggleButton.Size = new System.Drawing.Size(44, 22);
            this.GridToggleButton.Text = "Сетка";
            this.GridToggleButton.ToolTipText = "Показать/скрыть сетку (Ctrl+G)";
            this.GridToggleButton.Click += new System.EventHandler(this.GridToggleButton_Click);
            // 
            // SnapToggleButton
            // 
            this.SnapToggleButton.CheckOnClick = true;
            this.SnapToggleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SnapToggleButton.Image = ((System.Drawing.Image)(resources.GetObject("SnapToggleButton.Image")));
            this.SnapToggleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SnapToggleButton.Name = "SnapToggleButton";
            this.SnapToggleButton.Size = new System.Drawing.Size(73, 22);
            this.SnapToggleButton.Text = "Привязка";
            this.SnapToggleButton.ToolTipText = "Включить/выключить привязку к сетке (Ctrl+B)";
            this.SnapToggleButton.Click += new System.EventHandler(this.SnapToggleButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ShadowToggleButton
            // 
            this.ShadowToggleButton.CheckOnClick = true;
            this.ShadowToggleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ShadowToggleButton.Enabled = false;
            this.ShadowToggleButton.Image = ((System.Drawing.Image)(resources.GetObject("ShadowToggleButton.Image")));
            this.ShadowToggleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShadowToggleButton.Name = "ShadowToggleButton";
            this.ShadowToggleButton.Size = new System.Drawing.Size(46, 22);
            this.ShadowToggleButton.Text = "Тень";
            this.ShadowToggleButton.ToolTipText = "Включить/выключить тень для выделенной фигуры";
            this.ShadowToggleButton.Click += new System.EventHandler(this.ShadowToggleButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.видToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 24);
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
            this.FileCreateMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileCreateMenuItem.Size = new System.Drawing.Size(222, 22);
            this.FileCreateMenuItem.Text = "Создать";
            this.FileCreateMenuItem.Click += new System.EventHandler(this.FileCreateMenuItem_Click);
            // 
            // FileOpenMenuItem
            // 
            this.FileOpenMenuItem.Name = "FileOpenMenuItem";
            this.FileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileOpenMenuItem.Size = new System.Drawing.Size(222, 22);
            this.FileOpenMenuItem.Text = "Открыть...";
            this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSaveMenuItem.Size = new System.Drawing.Size(222, 22);
            this.FileSaveMenuItem.Text = "Сохранить";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileSaveAsMenuItem
            // 
            this.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
            this.FileSaveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.S)));
            this.FileSaveAsMenuItem.Size = new System.Drawing.Size(222, 22);
            this.FileSaveAsMenuItem.Text = "Сохранить как...";
            this.FileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(219, 6);
            // 
            // FileExportMenuItem
            // 
            this.FileExportMenuItem.Name = "FileExportMenuItem";
            this.FileExportMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.FileExportMenuItem.Size = new System.Drawing.Size(222, 22);
            this.FileExportMenuItem.Text = "Экспорт...";
            this.FileExportMenuItem.Click += new System.EventHandler(this.FileExportMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(219, 6);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.FileExitMenuItem.Size = new System.Drawing.Size(222, 22);
            this.FileExitMenuItem.Text = "Выход";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GridMenuItem,
            this.SnapMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // GridMenuItem
            // 
            this.GridMenuItem.CheckOnClick = true;
            this.GridMenuItem.Name = "GridMenuItem";
            this.GridMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.GridMenuItem.Size = new System.Drawing.Size(246, 22);
            this.GridMenuItem.Text = "Показать сетку";
            this.GridMenuItem.Click += new System.EventHandler(this.GridToggleButton_Click);
            // 
            // SnapMenuItem
            // 
            this.SnapMenuItem.CheckOnClick = true;
            this.SnapMenuItem.Name = "SnapMenuItem";
            this.SnapMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.SnapMenuItem.Size = new System.Drawing.Size(246, 22);
            this.SnapMenuItem.Text = "Привязка к сетке";
            this.SnapMenuItem.Click += new System.EventHandler(this.SnapToggleButton_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(149, 22);
            this.AboutMenuItem.Text = "О программе";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpacityLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 506);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 24);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // OpacityLabel
            // 
            this.OpacityLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.OpacityLabel.Name = "OpacityLabel";
            this.OpacityLabel.Size = new System.Drawing.Size(113, 19);
            this.OpacityLabel.Text = "Прозрачность: 255";
            // 
            // OpacityTrackBar
            // 
            this.OpacityTrackBar.AutoSize = false;
            this.OpacityTrackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OpacityTrackBar.Enabled = false;
            this.OpacityTrackBar.LargeChange = 32;
            this.OpacityTrackBar.Location = new System.Drawing.Point(0, 475);
            this.OpacityTrackBar.Maximum = 255;
            this.OpacityTrackBar.Minimum = 30;
            this.OpacityTrackBar.Name = "OpacityTrackBar";
            this.OpacityTrackBar.Size = new System.Drawing.Size(900, 31);
            this.OpacityTrackBar.SmallChange = 5;
            this.OpacityTrackBar.TabIndex = 3;
            this.OpacityTrackBar.TickFrequency = 16;
            this.OpacityTrackBar.Value = 255;
            this.OpacityTrackBar.Scroll += new System.EventHandler(this.OpacityTrackBar_Scroll);
            // 
            // GraphEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 530);
            this.Controls.Add(this.OpacityTrackBar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "GraphEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Графический редактор";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphEditorForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GraphEditorForm_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CreateRectangleButton;
        private System.Windows.Forms.ToolStripButton DeleteRectangleButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem FillColorMenuItem;
        private System.Windows.Forms.ColorDialog FillColorDialog;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem MoveForwardMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveBackwardMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MoveToFrontMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToBackMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem BorderColorMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton GridToggleButton;
        private System.Windows.Forms.ToolStripButton SnapToggleButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton ShadowToggleButton;
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
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GridMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SnapMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.OpenFileDialog FileOpenDialog;
        private System.Windows.Forms.SaveFileDialog FileSaveDialog;
        private System.Windows.Forms.SaveFileDialog FileExportDialog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel OpacityLabel;
        private System.Windows.Forms.TrackBar OpacityTrackBar;
    }
}