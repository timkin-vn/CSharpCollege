using GraphEditor.Business.Models;
using GraphEditor.Business.Models.File;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Services
{
    internal class FileService
    {
        public XmlPictureModel ToXmlModel(PictureModel model)
        {
            return new XmlPictureModel
            {
                Rectangles = model.Rectangles.Select(r => new XmlRectangleModel
                {
                    X = r.X,
                    Y = r.Y,
                    Width = r.Width,
                    Height = r.Height,
                    FillColor = new XmlColorModel
                    {
                        Red = r.FillColor.R,
                        Green = r.FillColor.G,
                        Blue = r.FillColor.B,
                    },
                    DrawColor = new XmlColorModel
                    {
                        Red = r.DrawColor.R,
                        Green = r.DrawColor.G,
                        Blue = r.DrawColor.B,
                    },
                }).ToList(),
            };
        }

        public PictureModel FromXmlModel(XmlPictureModel xmlModel)
        {
            return new PictureModel
            {
                RectangleList = xmlModel.Rectangles.Select(r => new RectangleModel
                {
                    X = r.X,
                    Y = r.Y,
                    Width = r.Width,
                    Height = r.Height,
                    FillColor = Color.FromArgb(r.FillColor.Red, r.FillColor.Green, r.FillColor.Blue),
                    DrawColor = Color.FromArgb(r.DrawColor.Red, r.DrawColor.Green, r.DrawColor.Blue),
                }).ToList(),
            };
        }

        public void SaveFile(string fileName, PictureModel picture)
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
                            var xmlPicture = ToXmlModel(picture);
                            var serializer = new XmlSerializer(typeof(XmlPictureModel));
                            serializer.Serialize(writer, xmlPicture);
                        }
                    }
                }
            }
        }
        public PictureModel OpenFile(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    try
                    {
                        var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                        using (var es = entry.Open())
                        {
                            using (var reader = new StreamReader(es))
                            {
                                var serializer = new XmlSerializer(typeof(XmlPictureModel));
                                var xmlPicture = (XmlPictureModel)serializer.Deserialize(reader);
                                return FromXmlModel(xmlPicture);
                            }
                        }
                    }
                    catch
                    {
                        throw new Exception("Неверный формат файла");
                    }

                }
            }
        }
    }
}
