using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace GraphEditor.Business.Services
{
    internal class FileService
    {
        public void Save(string fileName, PictureModel model)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                using (var es = entry.Open())
                using (var writer = new StreamWriter(es))
                {
                    var xml = ToXml(model);
                    var serializer = new XmlSerializer(typeof(XmlPicture));
                    serializer.Serialize(writer, xml);
                }
            }
        }

        public PictureModel Open(string fileName)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    using (var es = entry.Open())
                    using (var reader = new StreamReader(es))
                    {
                        var serializer = new XmlSerializer(typeof(XmlPicture));
                        var xml = (XmlPicture)serializer.Deserialize(reader);
                        return FromXml(xml);
                    }
                }
            }
            catch
            {
                throw new Exception("Неверный формат файла");
            }
        }

        private XmlPicture ToXml(PictureModel model)
        {
            return new XmlPicture
            {
                Rectangles = model.Figures
                    .Select(f => new XmlRectangle
                    {
                        Left = f.Left,
                        Top = f.Top,
                        Width = f.Width,
                        Height = f.Height,
                        Type = f.Type,
                        CornerRadius = f.CornerRadius,
                        BorderColor = new XmlColor
                        {
                            Red = f.BorderColor.R,
                            Green = f.BorderColor.G,
                            Blue = f.BorderColor.B,
                        },
                        FillColor = new XmlColor
                        {
                            Red = f.FillColor.R,
                            Green = f.FillColor.G,
                            Blue = f.FillColor.B,
                        },
                    })
                .ToList(),
            };
        }

        private PictureModel FromXml(XmlPicture xml)
        {
            return new PictureModel
            {
                Figures = xml.Rectangles
                    .Select(r => new FigureModel
                    {
                        Left = r.Left,
                        Top = r.Top,
                        Width = r.Width,
                        Height = r.Height,
                        Type = r.Type,
                        CornerRadius = r.CornerRadius,
                        BorderColor = Color.FromArgb(r.BorderColor.Red, r.BorderColor.Green, r.BorderColor.Blue),
                        FillColor = Color.FromArgb(r.FillColor.Red, r.FillColor.Green, r.FillColor.Blue),
                    })
                    .ToList(),
                SelectedFigure = null,
            };
        }
    }
}