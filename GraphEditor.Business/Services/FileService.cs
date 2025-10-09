using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace GraphEditor.Business.Services {
    internal class FileService {
        public void SaveToFile(string fileName, PictureModel model) {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Create)) {
                    var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    using (var es = entry.Open()) {
                        using (var writer = new StreamWriter(es)) {
                            var xml = ToXml(model);
                            var serializer = new XmlSerializer(typeof(XmlPicture));
                            serializer.Serialize(writer, xml);
                        }
                    }
                }
            }
        }

        public PictureModel OpenFile(string fileName) {
            try {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
                    using (var archive = new ZipArchive(fs, ZipArchiveMode.Read)) {
                        var entryName = Path.GetFileNameWithoutExtension(fileName) + ".xml";
                        var entry = archive.GetEntry(entryName);
                        if (entry == null) {
                            throw new InvalidDataException($"Отсутствует запись {entryName}.");
                        }

                        using (var es = entry.Open()) {
                            using (var reader = new StreamReader(es)) {
                                var serializer = new XmlSerializer(typeof(XmlPicture));
                                if (serializer.Deserialize(reader) is not XmlPicture xml) {
                                    throw new InvalidDataException("Не удалось десериализовать файл.");
                                }
                                return FromXml(xml);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw new Exception("Неверный формат файла", ex);
            }
        }

        private XmlPicture ToXml(PictureModel model) {
            return new XmlPicture {
                Rectangles = model.Rectangles
                    .Select(r => new XmlRectangle {
                        Id = r.Id,
                        Left = r.Left,
                        Top = r.Top,
                        Width = r.Width,
                        Height = r.Height,
                        BorderColor = new XmlColor {
                            Red = r.BorderColor.R,
                            Green = r.BorderColor.G,
                            Blue = r.BorderColor.B,
                        },
                        FillColor = new XmlColor {
                            Red = r.FillColor.R,
                            Green = r.FillColor.G,
                            Blue = r.FillColor.B,
                        },
                        BorderWidth = r.BorderWidth,
                        Text = r.Text,
                        TextColor = new XmlColor {
                            Red = r.TextColor.R,
                            Green = r.TextColor.G,
                            Blue = r.TextColor.B,
                        },
                        FontFamily = r.FontFamily,
                        FontSize = r.FontSize,
                        TextAlign = r.TextAlign.ToString(),
                    }).ToList(),
                Groups = model.Groups
                    .Select(g => new XmlGroup {
                        Id = g.Id,
                        Name = g.Name,
                        RectangleIds = g.RectangleIds.ToList(),
                    })
                    .ToList(),
            };
        }

        private PictureModel FromXml(XmlPicture xml) {
            static Color ToColor(XmlColor? color, Color fallback) => color == null
                ? fallback
                : Color.FromArgb(color.Red, color.Green, color.Blue);
            
            var rectangles = xml.Rectangles
                    .Select(r => new RectangleModel {
                        Id = r.Id == Guid.Empty ? Guid.NewGuid() : r.Id,
                        Left = r.Left,
                        Top = r.Top,
                        Width = r.Width,
                        Height = r.Height,
                        BorderColor = Color.FromArgb(r.BorderColor.Red, r.BorderColor.Green, r.BorderColor.Blue),
                        FillColor = Color.FromArgb(r.FillColor.Red, r.FillColor.Green, r.FillColor.Blue),

                        BorderWidth = r.BorderWidth <= 0 ? 1.5f : r.BorderWidth,
                        Text = r.Text,
                        TextColor = r.TextColor != null ?
                            Color.FromArgb(r.TextColor.Red, r.TextColor.Green, r.TextColor.Blue)
                            : Color.Black,
                        FontFamily = string.IsNullOrWhiteSpace(r.FontFamily) ? "Segoe UI" : r.FontFamily,
                        FontSize = r.FontSize <= 0 ? 10f : r.FontSize,
                        TextAlign = Enum.TryParse<TextAlign>(r.TextAlign, out var align) ? align : TextAlign.Center,
                    })
                    .ToList();

            var model = new PictureModel {
                Rectangles = rectangles,
                Groups = xml.Groups
                    .Select(g => {
                        var group = new GroupModel { Id = g.Id == Guid.Empty ? Guid.NewGuid() : g.Id, Name = g.Name };
                        foreach (var id in g.RectangleIds) {
                            if (rectangles.Any(r => r.Id == id)) {
                                group.Add(id);
                            }
                        }
                        return group;
                    })
                    .Where(g => !g.IsEmpty)
                    .ToList(),
                SelectedRectangle = null,
            };
            return model;
        }
    }
}
