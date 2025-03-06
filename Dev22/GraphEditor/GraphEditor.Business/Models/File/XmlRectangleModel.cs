using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphEditor.Business.Models.File
{
    [Serializable]
    public class XmlRectangleModel
    {
        [XmlAttribute("X")]
        public int X { get; set; }

        [XmlAttribute("Y")]
        public int Y { get; set; }

        [XmlAttribute("Width")]
        public int Width { get; set; }

        [XmlAttribute("Height")]
        public int Height { get; set; }

        [XmlElement("FillColor")]
        public XmlColorModel FillColor { get; set; }

        [XmlElement("DrawColor")]
        public XmlColorModel DrawColor { get; set; }

        [XmlIgnore] // Игнорируем это свойство при сериализации
        public Image Image { get; set; }

        // Сериализация изображения в массив байтов
        [XmlElement("ImageData")]
        public byte[] ImageData
        {
            get
            {
                if (Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); 
                        return ms.ToArray();
                    }
                }
                return null;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    Image = null;
                }
            }
        }
    }
}