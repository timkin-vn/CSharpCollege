using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
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
                        if (entry == null)
                            throw new Exception("XML файл не найден в архиве");

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
            var xmlPicture = new XmlPicture
            {
                Shapes = new List<XmlShape>()
            };

            foreach (var shape in model.Shapes)
            {
                var xmlShape = new XmlShape
                {
                    Left = shape.Left,
                    Top = shape.Top,
                    Width = shape.Width,
                    Height = shape.Height,
                    BorderThickness = shape.BorderThickness,
                    ShapeType = shape.ShapeType,
                    TrianglePointsUp = shape.TrianglePointsUp,
                    BorderColor = new XmlColor
                    {
                        Red = shape.BorderColor.R,
                        Green = shape.BorderColor.G,
                        Blue = shape.BorderColor.B,
                    },
                    FillColor = new XmlColor
                    {
                        Red = shape.FillColor.R,
                        Green = shape.FillColor.G,
                        Blue = shape.FillColor.B,
                    },
                };
                xmlPicture.Shapes.Add(xmlShape);
            }

            return xmlPicture;
        }

        private PictureModel FromXml(XmlPicture xml)
        {
            var model = new PictureModel();

            foreach (var xmlShape in xml.Shapes)
            {
                var shape = new ShapeModel
                {
                    Left = xmlShape.Left,
                    Top = xmlShape.Top,
                    Width = xmlShape.Width,
                    Height = xmlShape.Height,
                    BorderThickness = xmlShape.BorderThickness,
                    ShapeType = xmlShape.ShapeType,
                    TrianglePointsUp = xmlShape.TrianglePointsUp,
                    BorderColor = Color.FromArgb(xmlShape.BorderColor.Red, xmlShape.BorderColor.Green, xmlShape.BorderColor.Blue),
                    FillColor = Color.FromArgb(xmlShape.FillColor.Red, xmlShape.FillColor.Green, xmlShape.FillColor.Blue),
                };
                model.Shapes.Add(shape);
            }

            model.SelectedShape = null;
            return model;
        }
    }
}