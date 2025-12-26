using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System.Drawing.Drawing2D;

namespace GraphEditor.ViewServices;


internal abstract class Painter {
    public static void Paint(Graphics g, PictureViewModel? viewModel, bool isInteractive) {
        if (viewModel?.Rectangles == null || !viewModel.Rectangles.Any()) {
            return;
        }

        foreach (var rect in viewModel.Rectangles) {
            g.FillRectangle(rect.FillBrush, rect.Rectangle);
            g.DrawRectangle(rect.BorderPen, rect.Rectangle);

            if (!string.IsNullOrWhiteSpace(rect.Text)) {
                using var font = new Font(rect.FontFamily, rect.FontSize);
                using var textBrush = new SolidBrush(rect.TextColor);
                using var format = new StringFormat();
                format.Alignment = ToHorizontal(rect.TextAlign);
                format.LineAlignment = StringAlignment.Center;
                format.Trimming = StringTrimming.EllipsisCharacter;
                format.FormatFlags |= StringFormatFlags.LineLimit;
                g.DrawString(rect.Text, font, textBrush, rect.Rectangle, format);
            }

            if (!rect.IsSelected) continue;
            using var selectionPen = new Pen(Color.Black);
            selectionPen.DashStyle = DashStyle.Dot;
            g.DrawRectangle(selectionPen, rect.Rectangle);
        }

        if (!isInteractive) {
            return;
        }

        var activeBrush = Brushes.Black;
        var inactiveBrush = Brushes.White;
        var pen = Pens.Black;

        foreach (var marker in viewModel.Markers) {
            var brush = marker.IsActive ? activeBrush : inactiveBrush;
            g.FillRectangle(brush, marker.Rectangle);

            g.DrawRectangle(pen, marker.Rectangle);
        }
    }
    private static StringAlignment ToHorizontal(TextAlign align) => align switch {
        TextAlign.Left => StringAlignment.Near,
        TextAlign.Right => StringAlignment.Far,
        _ => StringAlignment.Center,
    };
}