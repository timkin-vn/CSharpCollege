using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Services
{
    internal class FileService
    {
        public void SaveToFile(string fileName, PictureModel model)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    using (var es = entry.Open())
                    {
                        using (var writer = new StreamWriter(es))
                        {
                            var xml = ToXml(model);
                            var serializer = new XmlSerializer(typeof(XmlPicture));
                            serializer.Serialize(writer, xml);
                        }
                    }
                }
            }
        }

        public PictureModel OpenFile(string fileName)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
                    {
                        var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                        using (var es = entry.Open())
                        {
                            using (var reader = new StreamReader(es))
                            {
                                var serializer = new XmlSerializer(typeof(XmlPicture));
                                var xml = (XmlPicture)serializer.Deserialize(reader);
                                return FromXml(xml);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Неверный формат файла", ex);
            }
        }

        private XmlPicture ToXml(PictureModel model)
        {
            return new XmlPicture
            {
                Rectangles = model.Rectangles
                    .Select(r => new XmlRectangle
                    {
                        Left = r.Left,
                        Top = r.Top,
                        Width = r.Width,
                        Height = r.Height,
                        BorderColor = new XmlColor 
                        { 
                            Red = r.BorderColor.R,
                            Green = r.BorderColor.G,
                            Blue = r.BorderColor.B,
                        },
                        FillColor = new XmlColor
                        {
                            Red = r.FillColor.R,
                            Green = r.FillColor.G,
                            Blue = r.FillColor.B,
                        },
                    }).ToList(),
                
                Circles = model.Circles
                    .Select(c => new XmlCircle
            {
                        Left = c.Left,
                        Top = c.Top,
                        Diameter = c.Diameter,
                        BorderColor = new XmlColor
                        {
                            Red = c.BorderColor.R,
                            Green = c.BorderColor.G,
                            Blue = c.BorderColor.B,
                        },
                        FillColor = new XmlColor
                {
                            Red = c.FillColor.R,
                            Green = c.FillColor.G,
                            Blue = c.FillColor.B,
                        },
                    }).ToList(),
            };
        }

        private PictureModel FromXml(XmlPicture xml)
        {
            return new PictureModel
            {
                Rectangles = xml.Rectangles
                    .Select(r => new RectangleModel
                    {
                        Left = r.Left,
                        Top = r.Top,
                        Width = r.Width,
                        Height = r.Height,
                        BorderColor = Color.FromArgb(r.BorderColor.Red, r.BorderColor.Green, r.BorderColor.Blue),
                        FillColor = Color.FromArgb(r.FillColor.Red, r.FillColor.Green, r.FillColor.Blue),
                    }).ToList(),
                Circles = (xml.Circles ?? new List<XmlCircle>())
                    .Select(c => new CircleModel
                    {
                        Left = c.Left,
                        Top = c.Top,
                        Diameter = c.Diameter,
                        BorderColor = Color.FromArgb(c.BorderColor.Red, c.BorderColor.Green, c.BorderColor.Blue),
                        FillColor = Color.FromArgb(c.FillColor.Red, c.FillColor.Green, c.FillColor.Blue),
                    }).ToList(),

                     SelectedRectangle = null,
                     SelectedCircle = null,
            };
        }
    }
}
