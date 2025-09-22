using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
            catch (System.Exception ex)
            {
                throw new System.Exception("Неверный формат файла", ex);
            }
        }

        private XmlPicture ToXml(PictureModel model)
        {
            var xml = new XmlPicture
            {
                Figures = new List<XmlFigure>()
            };

            foreach (var figure in model.Figures)
            {
                var xmlFigure = new XmlFigure();

                if (figure is RectangleModel)
                {
                    xmlFigure.Type = "Rectangle";
                }
                else if (figure is CircleModel)
                {
                    xmlFigure.Type = "Circle";
                }
                else if (figure is TriangleModel)
                {
                    xmlFigure.Type = "Triangle";
                }
                else
                {
                    xmlFigure.Type = "Rectangle";
                }

                xmlFigure.Left = figure.Left;
                xmlFigure.Top = figure.Top;
                xmlFigure.Width = figure.Width;
                xmlFigure.Height = figure.Height;
                xmlFigure.BorderColor = new XmlColor
                {
                    Red = figure.BorderColor.R,
                    Green = figure.BorderColor.G,
                    Blue = figure.BorderColor.B,
                };
                xmlFigure.FillColor = new XmlColor
                {
                    Red = figure.FillColor.R,
                    Green = figure.FillColor.G,
                    Blue = figure.FillColor.B,
                };

                xml.Figures.Add(xmlFigure);
            }

            return xml;
        }

        private PictureModel FromXml(XmlPicture xml)
        {
            var model = new PictureModel();

            if (xml.Figures != null)
            {
                foreach (var xmlFigure in xml.Figures)
                {
                    FigureModel figure = null;

                    if (xmlFigure.Type == "Rectangle")
                    {
                        figure = new RectangleModel();
                    }
                    else if (xmlFigure.Type == "Circle")
                    {
                        figure = new CircleModel();
                    }
                    else if (xmlFigure.Type == "Triangle")
                    {
                        figure = new TriangleModel();
                    }
                    else
                    {
                        figure = new RectangleModel();
                    }

                    figure.Left = xmlFigure.Left;
                    figure.Top = xmlFigure.Top;
                    figure.Width = xmlFigure.Width;
                    figure.Height = xmlFigure.Height;
                    figure.BorderColor = Color.FromArgb(xmlFigure.BorderColor.Red, xmlFigure.BorderColor.Green, xmlFigure.BorderColor.Blue);
                    figure.FillColor = Color.FromArgb(xmlFigure.FillColor.Red, xmlFigure.FillColor.Green, xmlFigure.FillColor.Blue);

                    model.Figures.Add(figure);
                }
            }

            return model;
        }
    }
}