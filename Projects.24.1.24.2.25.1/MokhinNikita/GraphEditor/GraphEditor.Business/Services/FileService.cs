using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;

namespace GraphEditor.Business.Services
{
    public class FileService
    {
        public void Save(string filename, PictureModel picture)
        {
            using(var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                using(var archive = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(filename) + ".xml");
                    using(var es = entry.Open())
                    {
                        using(var writer =  new StreamWriter(es))
                        {
                            var xml = ToXml(picture);
                            var serializer = new XmlSerializer(typeof(XmlPicture));
                            serializer.Serialize(writer, xml);
                        }
                    }
                }
            }
        }
        public PictureModel Open(string filename)
        {
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
                    {
                        var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(filename) + ".xml");
                        using (var es = entry.Open())
                        {
                            using (var reader = new StreamReader(es))
                            {
                                var serializer = new XmlSerializer(typeof (XmlPicture));
                                var xml = (XmlPicture) serializer.Deserialize(reader);
                                return FromXml(xml);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(filename, e);
            }

        }
        private XmlPicture ToXml(PictureModel picture)
        {
            return new XmlPicture
            {
                Rectangles = (from r in picture.Rectangles
                              select new XmlRectangle
                              {
                                  Left = r.Left,
                                  Top = r.Top,
                                  Width = r.Width, Height = r.Height,
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
                                  BorderWidth = r.BorderWidth,

                              }).ToList(),
            };
        }
        private PictureModel FromXml(XmlPicture picture)
        {
            return new PictureModel
            {
                Rectangles = ( from r in picture.Rectangles select new RectangleModel
                {
                    Left = r.Left,
                    Top = r.Top,
                    Width = r.Width,
                    Height = r.Height,
                    BorderColor = System.Drawing.Color.FromArgb(r.BorderColor.Red, r.BorderColor.Green, r.BorderColor.Blue),
                    FillColor = System.Drawing.Color.FromArgb(r.FillColor.Red, r.FillColor.Green, r.FillColor.Blue),
                    BorderWidth = r.BorderWidth,
                }).ToList()
            };
        }
    }
}
