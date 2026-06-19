namespace TriangleEditor
{
    partial class TriangleEditorForm
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
            this.toolbarPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.newButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.colorButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.hintLabel = new System.Windows.Forms.Label();
            this.canvasPanel = new TriangleEditor.DoubleBufferedPanel();
            this.toolbarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbarPanel
            // 
            this.toolbarPanel.Controls.Add(this.newButton);
            this.toolbarPanel.Controls.Add(this.openButton);
            this.toolbarPanel.Controls.Add(this.saveButton);
            this.toolbarPanel.Controls.Add(this.exportButton);
            this.toolbarPanel.Controls.Add(this.colorButton);
            this.toolbarPanel.Controls.Add(this.deleteButton);
            this.toolbarPanel.Controls.Add(this.hintLabel);
            this.toolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolbarPanel.Location = new System.Drawing.Point(0, 0);
            this.toolbarPanel.Name = "toolbarPanel";
            this.toolbarPanel.Padding = new System.Windows.Forms.Padding(6);
            this.toolbarPanel.Size = new System.Drawing.Size(900, 44);
            this.toolbarPanel.TabIndex = 0;
            // 
            // newButton
            // 
            this.newButton.AutoSize = true;
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 27);
            this.newButton.TabIndex = 0;
            this.newButton.Text = "Новый";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // openButton
            // 
            this.openButton.AutoSize = true;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 27);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "Открыть";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = true;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 27);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.AutoSize = true;
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 27);
            this.exportButton.TabIndex = 3;
            this.exportButton.Text = "Экспорт PNG";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // colorButton
            // 
            this.colorButton.AutoSize = true;
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(75, 27);
            this.colorButton.TabIndex = 4;
            this.colorButton.Text = "Цвет заливки";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.AutoSize = true;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 27);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // hintLabel
            // 
            this.hintLabel.AutoSize = true;
            this.hintLabel.Margin = new System.Windows.Forms.Padding(12, 8, 3, 0);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(300, 15);
            this.hintLabel.TabIndex = 6;
            this.hintLabel.Text = "ЛКМ по пустому — рисовать, по фигуре — двигать, Ctrl+ЛКМ — ресайз";
            // 
            // canvasPanel
            // 
            this.canvasPanel.BackColor = System.Drawing.Color.White;
            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.Location = new System.Drawing.Point(0, 44);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(900, 556);
            this.canvasPanel.TabIndex = 1;
            this.canvasPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.canvasPanel_Paint);
            this.canvasPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseDown);
            this.canvasPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseMove);
            this.canvasPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseUp);
            // 
            // TriangleEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.canvasPanel);
            this.Controls.Add(this.toolbarPanel);
            this.Name = "TriangleEditorForm";
            this.Text = "Редактор треугольников";
            this.toolbarPanel.ResumeLayout(false);
            this.toolbarPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.FlowLayoutPanel toolbarPanel;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label hintLabel;
        private TriangleEditor.DoubleBufferedPanel canvasPanel;
    }
}
