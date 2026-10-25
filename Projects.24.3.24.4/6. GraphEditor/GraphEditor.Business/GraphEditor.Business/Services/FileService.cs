using System.Collections.Generic;
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
    public class FileService
    {
        public void Save(string fileName, PictureModel model)
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

        public PictureModel Open(string fileName)
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
            var xmlPicture = new XmlPicture { Figures = new List<XmlFigure>() };

            foreach (var f in model.Figures)
            {
                XmlFigure xf = null;
                if (f is RectangleModel) xf = new XmlRectangle();
                else if (f is CircleModel) xf = new XmlCircle();
                else if (f is TriangleModel) xf = new XmlTriangle();

                if (xf != null)
                {
                    xf.Left = f.Left;
                    xf.Top = f.Top;
                    xf.Width = f.Width;
                    xf.Height = f.Height;
                    xf.BorderColor = new XmlColor { Red = f.BorderColor.R, Green = f.BorderColor.G, Blue = f.BorderColor.B };
                    xf.FillColor = new XmlColor { Red = f.FillColor.R, Green = f.FillColor.G, Blue = f.FillColor.B };
                    xmlPicture.Figures.Add(xf);
                }
            }
            return xmlPicture;
        }

        private PictureModel FromXml(XmlPicture xml)
        {
            var model = new PictureModel();
            foreach (var xf in xml.Figures)
            {
                FigureModel f = null;
                if (xf is XmlRectangle) f = new RectangleModel();
                else if (xf is XmlCircle) f = new CircleModel();
                else if (xf is XmlTriangle) f = new TriangleModel();

                if (f != null)
                {
                    f.Left = xf.Left;
                    f.Top = xf.Top;
                    f.Width = xf.Width;
                    f.Height = xf.Height;
                    f.BorderColor = Color.FromArgb(xf.BorderColor.Red, xf.BorderColor.Green, xf.BorderColor.Blue);
                    f.FillColor = Color.FromArgb(xf.FillColor.Red, xf.FillColor.Green, xf.FillColor.Blue);
                    model.Figures.Add(f);
                }
            }
            return model;
        }
    }
}