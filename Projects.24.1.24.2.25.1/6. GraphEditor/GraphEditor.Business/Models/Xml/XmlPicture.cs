using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using GraphEditor.Business.Models;

namespace GraphEditor.Business.Models.Xml
{
    [XmlRoot("Picture")]
    public class XmlPicture
    {
        [XmlArray("Rectangles")]
        [XmlArrayItem("Rectangle")]
        public List<XmlRectangle> Rectangles { get; set; } = new List<XmlRectangle>();

        public static XmlPicture ToXml(PictureModel model)
        {
            return new XmlPicture
            {
                Rectangles = model.Rectangles.Select(r => new XmlRectangle
                {
                    Left = r.Left,
                    Top = r.Top,
                    Width = r.Width,
                    Height = r.Height,
                    ShapeType = r.ShapeType,
                    FillColor = XmlColor.ToXml(r.FillColor),
                    BorderColor = XmlColor.ToXml(r.BorderColor),
                }).ToList()
            };
        }

        public static PictureModel FromXml(XmlPicture xml)
        {
            var model = new PictureModel();

            if (xml?.Rectangles == null)
            {
                return model;
            }

            foreach (var rectangle in xml.Rectangles)
            {
                model.Rectangles.Add(new RectangleModel
                {
                    Left = rectangle.Left,
                    Top = rectangle.Top,
                    Width = rectangle.Width,
                    Height = rectangle.Height,
                    ShapeType = rectangle.ShapeType,
                    FillColor = XmlColor.FromXml(rectangle.FillColor, PictureServiceDefaultColors.DefaultFillColor),
                    BorderColor = XmlColor.FromXml(rectangle.BorderColor, PictureServiceDefaultColors.DefaultBorderColor),
                    EditMode = EditMode.None,
                });
            }

            return model;
        }
    }
}
