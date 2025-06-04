using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [XmlIgnore]
        public DateTime DeliverDate { get; set; }

        [XmlAttribute("DeliverDate")]
        public string DeliverDateXml
        {
            get => DeliverDate.ToShortDateString();
            set => DeliverDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime ExpirationDate { get; set; }

        [XmlAttribute("ExpirationDate")]
        public string ExpirationDateXml
        {
            get => ExpirationDate.ToShortDateString();
            set => ExpirationDate = DateTime.Parse(value);
        }

        [XmlAttribute("Count")]
        public string Count { get; set; }

        [XmlAttribute("Rating")]
        public string Rating { get; set; }
    }
}
