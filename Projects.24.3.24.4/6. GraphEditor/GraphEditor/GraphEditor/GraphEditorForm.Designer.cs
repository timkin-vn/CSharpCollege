namespace GraphEditor
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

        private void InitializeComponent()
        {
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.fileHeader = new System.Windows.Forms.Label();
            this.newButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.shapesHeader = new System.Windows.Forms.Label();
            this.triangleButton = new System.Windows.Forms.Button();
            this.rectangleButton = new System.Windows.Forms.Button();
            this.ellipseButton = new System.Windows.Forms.Button();
            this.diamondButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.actionsHeader = new System.Windows.Forms.Label();
            this.colorButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.sizeHeader = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.NumericUpDown();
            this.heightLabel = new System.Windows.Forms.Label();
            this.heightBox = new System.Windows.Forms.NumericUpDown();
            this.statusLabel = new System.Windows.Forms.Label();
            this.canvasPanel = new GraphEditor.DoubleBufferedPanel();
            this.sidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
            this.SuspendLayout();

            this.sidebar.AutoScroll = true;
            this.sidebar.Controls.Add(this.fileHeader);
            this.sidebar.Controls.Add(this.newButton);
            this.sidebar.Controls.Add(this.openButton);
            this.sidebar.Controls.Add(this.saveButton);
            this.sidebar.Controls.Add(this.shapesHeader);
            this.sidebar.Controls.Add(this.triangleButton);
            this.sidebar.Controls.Add(this.rectangleButton);
            this.sidebar.Controls.Add(this.ellipseButton);
            this.sidebar.Controls.Add(this.diamondButton);
            this.sidebar.Controls.Add(this.selectButton);
            this.sidebar.Controls.Add(this.actionsHeader);
            this.sidebar.Controls.Add(this.colorButton);
            this.sidebar.Controls.Add(this.deleteButton);
            this.sidebar.Controls.Add(this.sizeHeader);
            this.sidebar.Controls.Add(this.widthLabel);
            this.sidebar.Controls.Add(this.widthBox);
            this.sidebar.Controls.Add(this.heightLabel);
            this.sidebar.Controls.Add(this.heightBox);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(180, 620);
            this.sidebar.TabIndex = 0;
            this.sidebar.WrapContents = false;

            this.fileHeader.AutoSize = true;
            this.fileHeader.Name = "fileHeader";
            this.fileHeader.Text = "Файл";

            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(150, 28);
            this.newButton.TabIndex = 1;
            this.newButton.Text = "Новый";
            this.newButton.Click += new System.EventHandler(this.newButton_Click);

            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(150, 28);
            this.openButton.TabIndex = 2;
            this.openButton.Text = "Открыть";
            this.openButton.Click += new System.EventHandler(this.openButton_Click);

            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 28);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Сохранить";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);

            this.shapesHeader.AutoSize = true;
            this.shapesHeader.Name = "shapesHeader";
            this.shapesHeader.Text = "Фигуры";

            this.triangleButton.Name = "triangleButton";
            this.triangleButton.Size = new System.Drawing.Size(150, 28);
            this.triangleButton.TabIndex = 4;
            this.triangleButton.Text = "Треугольник";
            this.triangleButton.Click += new System.EventHandler(this.triangleButton_Click);

            this.rectangleButton.Name = "rectangleButton";
            this.rectangleButton.Size = new System.Drawing.Size(150, 28);
            this.rectangleButton.TabIndex = 5;
            this.rectangleButton.Text = "Прямоугольник";
            this.rectangleButton.Click += new System.EventHandler(this.rectangleButton_Click);

            this.ellipseButton.Name = "ellipseButton";
            this.ellipseButton.Size = new System.Drawing.Size(150, 28);
            this.ellipseButton.TabIndex = 6;
            this.ellipseButton.Text = "Эллипс";
            this.ellipseButton.Click += new System.EventHandler(this.ellipseButton_Click);

            this.diamondButton.Name = "diamondButton";
            this.diamondButton.Size = new System.Drawing.Size(150, 28);
            this.diamondButton.TabIndex = 7;
            this.diamondButton.Text = "Ромб";
            this.diamondButton.Click += new System.EventHandler(this.diamondButton_Click);

            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(150, 28);
            this.selectButton.TabIndex = 8;
            this.selectButton.Text = "Выбор / правка";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);

            this.actionsHeader.AutoSize = true;
            this.actionsHeader.Name = "actionsHeader";
            this.actionsHeader.Text = "Действия";

            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(150, 28);
            this.colorButton.TabIndex = 9;
            this.colorButton.Text = "Цвет заливки";
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);

            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(150, 28);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);

            this.sizeHeader.AutoSize = true;
            this.sizeHeader.Name = "sizeHeader";
            this.sizeHeader.Text = "Размер";

            this.widthLabel.AutoSize = true;
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Text = "Ширина";

            this.widthBox.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.widthBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            this.widthBox.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(150, 23);
            this.widthBox.TabIndex = 11;
            this.widthBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            this.widthBox.ValueChanged += new System.EventHandler(this.sizeBox_ValueChanged);

            this.heightLabel.AutoSize = true;
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Text = "Высота";

            this.heightBox.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.heightBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            this.heightBox.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(150, 23);
            this.heightBox.TabIndex = 12;
            this.heightBox.Value = new decimal(new int[] { 100, 0, 0, 0 });
            this.heightBox.ValueChanged += new System.EventHandler(this.sizeBox_ValueChanged);

            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusLabel.Location = new System.Drawing.Point(180, 600);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(720, 20);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Выберите фигуру и рисуйте мышью (можно поверх других). Режим Выбор: тянуть — двигать, Ctrl+ЛКМ — менять размер.";

            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.Location = new System.Drawing.Point(180, 0);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(720, 600);
            this.canvasPanel.TabIndex = 2;
            this.canvasPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.canvasPanel_Paint);
            this.canvasPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseDown);
            this.canvasPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseMove);
            this.canvasPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseUp);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.canvasPanel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.sidebar);
            this.Name = "GraphEditorForm";
            this.Text = "Графический редактор";
            this.sidebar.ResumeLayout(false);
            this.sidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Label fileHeader;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label shapesHeader;
        private System.Windows.Forms.Button triangleButton;
        private System.Windows.Forms.Button rectangleButton;
        private System.Windows.Forms.Button ellipseButton;
        private System.Windows.Forms.Button diamondButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Label actionsHeader;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label sizeHeader;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.NumericUpDown widthBox;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.NumericUpDown heightBox;
        private System.Windows.Forms.Label statusLabel;
        private GraphEditor.DoubleBufferedPanel canvasPanel;
    }
}
