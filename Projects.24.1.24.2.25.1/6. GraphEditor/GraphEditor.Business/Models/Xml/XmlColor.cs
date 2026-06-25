using System.Drawing;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.Xml
{
    public class XmlColor
    {
        [XmlAttribute]
        public int Red { get; set; }

        [XmlAttribute]
        public int Green { get; set; }

        [XmlAttribute]
        public int Blue { get; set; }

        public static XmlColor ToXml(Color color)
        {
            return new XmlColor
            {
                Red = color.R,
                Green = color.G,
                Blue = color.B,
            };
        }

        public static Color FromXml(XmlColor xml, Color defaultColor)
        {
            if (xml == null)
            {
                return defaultColor;
            }

            return Color.FromArgb(xml.Red, xml.Green, xml.Blue);
        }
    }
}
