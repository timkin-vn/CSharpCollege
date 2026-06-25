using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
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
            return new XmlPicture
            {
                Rectangles = model.Rectangles
                    .Select(r => new XmlRectangle
                    {
                        Left = r.Left,
                        Top = r.Top,
                        Width = r.Width,
                        Height = r.Height,
                        BorderColor = new XmlColor { Red = r.BorderColor.R, Green = r.BorderColor.G, Blue = r.BorderColor.B },
                        FillColor = new XmlColor { Red = r.FillColor.R, Green = r.FillColor.G, Blue = r.FillColor.B },
                    })
                    .ToList(),
                Triangles = model.Triangles
                    .Select(t => new XmlTriangle
                    {
                        X1 = t.X1,
                        Y1 = t.Y1,
                        X2 = t.X2,
                        Y2 = t.Y2,
                        X3 = t.X3,
                        Y3 = t.Y3,
                        BorderColor = new XmlColor { Red = t.BorderColor.R, Green = t.BorderColor.G, Blue = t.BorderColor.B },
                        FillColor = new XmlColor { Red = t.FillColor.R, Green = t.FillColor.G, Blue = t.FillColor.B },
                    })
                    .ToList(),
                Circles = model.Circles
                    .Select(c => new XmlCircle
                    {
                        CenterX = c.CenterX,
                        CenterY = c.CenterY,
                        Width = c.Width,
                        Height = c.Height,
                        BorderColor = new XmlColor { Red = c.BorderColor.R, Green = c.BorderColor.G, Blue = c.BorderColor.B },
                        FillColor = new XmlColor { Red = c.FillColor.R, Green = c.FillColor.G, Blue = c.FillColor.B },
                    })
                    .ToList(),
                HilbertCurves = model.HilbertCurves
                    .Select(h => new XmlHilbertCurve
                    {
                        X = h.X,
                        Y = h.Y,
                        Size = h.Size,
                        Order = h.Order,
                        BorderColor = new XmlColor { Red = h.BorderColor.R, Green = h.BorderColor.G, Blue = h.BorderColor.B },
                        FillColor = new XmlColor { Red = h.FillColor.R, Green = h.FillColor.G, Blue = h.FillColor.B },
                    })
                    .ToList(),
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
                    })
                    .ToList(),
                Triangles = xml.Triangles
                    .Select(t => new TriangleModel
                    {
                        X1 = t.X1,
                        Y1 = t.Y1,
                        X2 = t.X2,
                        Y2 = t.Y2,
                        X3 = t.X3,
                        Y3 = t.Y3,
                        BorderColor = Color.FromArgb(t.BorderColor.Red, t.BorderColor.Green, t.BorderColor.Blue),
                        FillColor = Color.FromArgb(t.FillColor.Red, t.FillColor.Green, t.FillColor.Blue),
                    })
                    .ToList(),
                Circles = xml.Circles
                    .Select(c => new CircleModel
                    {
                        CenterX = c.CenterX,
                        CenterY = c.CenterY,
                        Width = c.Width,
                        Height = c.Height,
                        BorderColor = Color.FromArgb(c.BorderColor.Red, c.BorderColor.Green, c.BorderColor.Blue),
                        FillColor = Color.FromArgb(c.FillColor.Red, c.FillColor.Green, c.FillColor.Blue),
                    })
                    .ToList(),
                HilbertCurves = xml.HilbertCurves
                    .Select(h => new HilbertCurveModel
                    {
                        X = h.X,
                        Y = h.Y,
                        Size = h.Size,
                        Order = h.Order,
                        BorderColor = Color.FromArgb(h.BorderColor.Red, h.BorderColor.Green, h.BorderColor.Blue),
                        FillColor = Color.FromArgb(h.FillColor.Red, h.FillColor.Green, h.FillColor.Blue),
                    })
                    .ToList(),
                SelectedRectangle = null,
                SelectedTriangle = null,
                SelectedCircle = null,
                SelectedHilbertCurve = null,
            };
        }
    }
}