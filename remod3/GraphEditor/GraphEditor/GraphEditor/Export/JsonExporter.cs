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
        var dto = PictureDto.FromModel();
        var json = JsonSerializer.Serialize(dto, Options);
        File.WriteAllText(filePath, json);
    }

    private class PictureDto {
        public static PictureDto FromModel() => new();
    }

    private sealed class ColorConverter : JsonConverter<Color> {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var s = reader.GetString();
            return string.IsNullOrWhiteSpace(s) ? Color.Black : ColorTranslator.FromHtml(s);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) {
            writer.WriteStringValue(ColorTranslator.ToHtml(value));
        }
    }
}
