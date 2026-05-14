using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System.Drawing.Drawing2D;

namespace GraphEditor.ViewServices;

internal static class Painter {
    public static void Paint(Graphics g, PictureViewModel? viewModel, bool isInteractive) {
        if (viewModel?.Rectangles == null || !viewModel.Rectangles.Any()) {
            return;
        }

        foreach (var rect in viewModel.Rectangles) {
            using var fillBrush = new SolidBrush(rect.FillColor);
            using var borderPen = new Pen(rect.BorderColor, rect.BorderWidth);

            g.FillRectangle(fillBrush, rect.Rectangle);
            g.DrawRectangle(borderPen, rect.Rectangle);

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
        }

        using var selectionPen = new Pen(Color.Black, 1f);
        selectionPen.DashStyle = DashStyle.Dash;
        foreach (var rect in viewModel.Rectangles.Where(r => r.IsSelected)) {
            g.DrawRectangle(selectionPen, Rectangle.Inflate(rect.Rectangle, 2, 2));
        }

        if (!isInteractive) {
            return;
        }

        foreach (var marker in viewModel.AllMarkers) {
            using var brush = new SolidBrush(marker.IsActive ? Color.Black : Color.White);
            g.FillRectangle(brush, marker.Rectangle);
            g.DrawRectangle(Pens.Black, marker.Rectangle);
        }
    }

    private static StringAlignment ToHorizontal(TextAlign align) => align switch {
        TextAlign.Left => StringAlignment.Near,
        TextAlign.Right => StringAlignment.Far,
        _ => StringAlignment.Center
    };
}
