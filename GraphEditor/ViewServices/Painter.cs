using GraphEditor.ViewModels;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace GraphEditor.ViewServices {
    internal class Painter {
        public void Paint(Graphics g, PictureViewModel? viewModel, bool isInteractive) {
            if (viewModel?.Rectangles == null || !viewModel.Rectangles.Any()) {
                return;
            }

            foreach (var rect in viewModel.Rectangles) {
                g.FillRectangle(rect.FillBrush, rect.Rectangle);
                g.DrawRectangle(rect.BorderPen, rect.Rectangle);

                if (!string.IsNullOrWhiteSpace(rect.Text)) {
                    using var font = new Font(rect.FontFamily, rect.FontSize);
                    using var textBrush = new SolidBrush(rect.TextColor);
                    using var format = new StringFormat { Alignment = ToHorizontal(rect.TextAlign), LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
                    format.FormatFlags |= StringFormatFlags.LineLimit;
                    g.DrawString(rect.Text, font, textBrush, rect.Rectangle, format);
                }

                if (rect.IsSelected) {
                    using var selectionPen = new Pen(Color.Black) { DashStyle = DashStyle.Dot };
                    g.DrawRectangle(selectionPen, rect.Rectangle);
                }
            }

            if (!isInteractive) return; {
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;
                var pen = Pens.Black;

                foreach (var marker in viewModel.Markers) {
                    var brush = marker.IsActive ? activeBrush : inactiveBrush;
                    g.FillRectangle(brush, marker.Rectangle);

                    g.DrawRectangle(pen, marker.Rectangle);
                }
            }
        }
        private static StringAlignment ToHorizontal(TextAlign align) => align switch {
            TextAlign.Left => StringAlignment.Near,
            TextAlign.Right => StringAlignment.Far,
            _ => StringAlignment.Center,
        };
    }
}