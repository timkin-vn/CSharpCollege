using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Calculator {
    public class RoundedButton : Button {
        private int _cornerRadius = 15;

        public RoundedButton() {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.FromArgb(50, 50, 50);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs pevent) {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int diameter = _cornerRadius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Width - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Width - diameter, rect.Height - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(BackColor)) {
                pevent.Graphics.FillPath(brush, path);
            }

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(pevent.Graphics, Text, Font, rect, ForeColor, flags);

            if (Focused) {
                using (Pen pen = new Pen(Color.FromArgb(100, 100, 100), 2)) {
                    pevent.Graphics.DrawPath(pen, path);
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
            BackColor = Color.FromArgb(
                Math.Min(BackColor.R + 30, 255),
                Math.Min(BackColor.G + 30, 255),
                Math.Min(BackColor.B + 30, 255));
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            ResetBackColor();
            Invalidate();
        }

        private void ResetBackColor() {
            if (Text == "=")
                BackColor = Color.FromArgb(0, 120, 215);
            else if (Text == "C")
                BackColor = Color.FromArgb(200, 50, 50);
            else if ("+-*/".Contains(Text))
                BackColor = Color.FromArgb(70, 70, 70);
            else if ("√x²%MSMR+/-".Contains(Text) || this == (Parent as CalculatorForm.CalculatorForm)?.SineButton)
                BackColor = Color.FromArgb(0, 150, 136);
            else if (this == (Parent as CalculatorForm.CalculatorForm)?.TrigMenuButton)
                BackColor = Color.FromArgb(100, 100, 100);
            else
                BackColor = Color.FromArgb(50, 50, 50);
        }
    }
}