using System.Globalization;
using System.Text;
using GraphEditor.Business.Models;

namespace GraphEditor.Export;

public static class SvgExporter {
    public static void Export(PictureModel picture, string filePath, int canvasWidth, int canvasHeight, Color background) {
        var sb = new StringBuilder();
        sb.AppendLine($"""<?xml version="1.0" encoding="UTF-8"?>""");
        sb.AppendLine($"""<svg xmlns="http://www.w3.org/2000/svg" width="{canvasWidth}" height="{canvasHeight}" viewBox="0 0 {canvasWidth} {canvasHeight}">""");

        sb.AppendLine($"""  <rect x="0" y="0" width="{canvasWidth}" height="{canvasHeight}" fill="{ToSvgColor(background)}"/>""");

        foreach (var r in picture.Rectangles) {
            sb.AppendLine(RectToSvg(r));
        }

        sb.AppendLine("</svg>");
        File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }

    private static string RectToSvg(RectangleModel r) {
        var fill = ToSvgColor(r.FillColor);
        var stroke = ToSvgColor(r.BorderColor);
        var strokeWidth = r.BorderWidth.ToString(CultureInfo.InvariantCulture);

        var rect = $"""  <rect x="{r.Left}" y="{r.Top}" width="{r.Width}" height="{r.Height}" fill="{fill}" stroke="{stroke}" stroke-width="{strokeWidth}" />""";

        if (string.IsNullOrWhiteSpace(r.Text))
            return rect;

        var (textAnchor, dx) = r.TextAlign switch {
            TextAlign.Left => ("start", 4),
            TextAlign.Right => ("end", -4),
            _ => ("middle", 0)
        };

        var cx = r.TextAlign switch {
            TextAlign.Left => r.Left,
            TextAlign.Right => r.Right,
            _ => r.Left + r.Width / 2
        };

        var cy = r.Top + r.Height / 2;

        var txt = $"""
                     <text x="{cx + dx}" y="{cy}" fill="{ToSvgColor(r.TextColor)}" font-family="{EscapeXml(r.FontFamily)}" font-size="{r.FontSize.ToString(CultureInfo.InvariantCulture)}" dominant-baseline="middle" text-anchor="{textAnchor}">
                       {EscapeXml(r.Text)}
                     </text>
                   """;

        return rect + Environment.NewLine + txt;
    }

    private static string ToSvgColor(Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

    private static string EscapeXml(string? s) {
        return string.IsNullOrEmpty(s) ? string.Empty : s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
    }
}
