using System.Text.Json;
using System.Text.Json.Serialization;
using GraphEditor.Business.Models;

namespace GraphEditor.Export;

public static class JsonExporter {
    private static readonly JsonSerializerOptions Options = new() {
        WriteIndented = true,
        Converters = { new ColorConverter() }
    };

    public static void Export(PictureModel picture, string filePath) {
        var dto = PictureDto.FromModel(picture);
        var json = JsonSerializer.Serialize(dto, Options);
        File.WriteAllText(filePath, json);
    }

    public class PictureDto {
        public required RectangleDto[] Rectangles { get; set; }
        public required GroupDto[] Groups { get; set; }

        public static PictureDto FromModel(PictureModel m) => new() {
            Rectangles = System.Linq.Enumerable.Select(m.Rectangles, RectangleDto.FromModel).ToArray(),
            Groups = System.Linq.Enumerable.Select(m.Groups, GroupDto.FromModel).ToArray()
        };
    }

    public class RectangleDto {
        public string Id { get; set; } = string.Empty;
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public System.Drawing.Color FillColor { get; set; }
        public System.Drawing.Color BorderColor { get; set; }
        public float BorderWidth { get; set; }
        public string? Text { get; set; }
        public System.Drawing.Color TextColor { get; set; }
        public string FontFamily { get; set; } = "Segoe UI";
        public float FontSize { get; set; }
        public TextAlign TextAlign { get; set; }

        public static RectangleDto FromModel(RectangleModel r) => new() {
            Id = r.Id.ToString(),
            Left = r.Left,
            Top = r.Top,
            Width = r.Width,
            Height = r.Height,
            FillColor = r.FillColor,
            BorderColor = r.BorderColor,
            BorderWidth = r.BorderWidth,
            Text = r.Text,
            TextColor = r.TextColor,
            FontFamily = r.FontFamily,
            FontSize = r.FontSize,
            TextAlign = r.TextAlign
        };
    }

    public class GroupDto {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string[] RectangleIds { get; set; } = [];

        public static GroupDto FromModel(GroupModel g) => new() {
            Id = g.Id.ToString(),
            Name = g.Name,
            RectangleIds = g.RectangleIds.ConvertAll(id => id.ToString()).ToArray()
        };
    }

    private sealed class ColorConverter : JsonConverter<System.Drawing.Color> {
        public override System.Drawing.Color Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options) {
            var s = reader.GetString();
            if (string.IsNullOrWhiteSpace(s)) return System.Drawing.Color.Black;
            return System.Drawing.ColorTranslator.FromHtml(s);
        }

        public override void Write(Utf8JsonWriter writer, System.Drawing.Color value, JsonSerializerOptions options) {
            writer.WriteStringValue(System.Drawing.ColorTranslator.ToHtml(value));
        }
    }
}