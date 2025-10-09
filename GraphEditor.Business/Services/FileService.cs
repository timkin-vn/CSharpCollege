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
                        var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                        using (var es = entry!.Open()) {
                            using (var reader = new StreamReader(es)) {
                                var serializer = new XmlSerializer(typeof(XmlPicture));
                                var xml = (XmlPicture)serializer.Deserialize(reader)!;
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
            };
        }

        private PictureModel FromXml(XmlPicture xml) {
            return new PictureModel {
                Rectangles = xml.Rectangles
                    .Select(r => new RectangleModel {
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
                    }).ToList(),
                SelectedRectangle = null,
            };
        }
    }
}
