using System;
using System.Windows.Forms;

namespace GraphEditor
{
    partial class GradientForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelColor1;
        private Panel panelColor2;
        private Button btnColor1;
        private Button btnColor2;
        private Button btnOK;
        private Button btnCancel;
        private Label label1;
        private Label label2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelColor1 = new Panel();
            this.panelColor2 = new Panel();
            this.btnColor1 = new Button();
            this.btnColor2 = new Button();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.label1 = new Label();
            this.label2 = new Label();

            // label1
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.Text = "Цвет 1:";

            // panelColor1
            this.panelColor1.Location = new System.Drawing.Point(80, 20);
            this.panelColor1.Size = new System.Drawing.Size(50, 20);
            this.panelColor1.BorderStyle = BorderStyle.FixedSingle;

            // btnColor1
            this.btnColor1.Location = new System.Drawing.Point(140, 20);
            this.btnColor1.Size = new System.Drawing.Size(80, 23);
            this.btnColor1.Text = "Выбрать";
            this.btnColor1.Click += new EventHandler(this.btnColor1_Click);

            // label2
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.Text = "Цвет 2:";

            // panelColor2
            this.panelColor2.Location = new System.Drawing.Point(80, 50);
            this.panelColor2.Size = new System.Drawing.Size(50, 20);
            this.panelColor2.BorderStyle = BorderStyle.FixedSingle;

            // btnColor2
            this.btnColor2.Location = new System.Drawing.Point(140, 50);
            this.btnColor2.Size = new System.Drawing.Size(80, 23);
            this.btnColor2.Text = "Выбрать";
            this.btnColor2.Click += new EventHandler(this.btnColor2_Click);

            // btnOK
            this.btnOK.Location = new System.Drawing.Point(50, 90);
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.Text = "OK";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(135, 90);
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // GradientForm
            this.ClientSize = new System.Drawing.Size(240, 130);
            this.Text = "Настройка градиента";
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelColor1);
            this.Controls.Add(this.btnColor1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelColor2);
            this.Controls.Add(this.btnColor2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
        }
    }
}