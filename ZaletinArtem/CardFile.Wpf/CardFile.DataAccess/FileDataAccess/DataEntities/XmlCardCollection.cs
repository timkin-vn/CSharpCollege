using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.DataEntities
{
    [Serializable]
    [XmlRoot("BookCollection")]
    public class XmlCardCollection
    {
        [XmlAttribute("NextId")]
        public int NextId { get; set; }

        [XmlArray("Books")]
        [XmlArrayItem("Book")]
        public List<XmlCard> Cards { get; set; } = new List<XmlCard>();

        public void FillFromDataCollection(CardFileDataCollection collection)
        {
            Cards.Clear();
            Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<XmlCard>(c)));
            NextId = collection.NextId;
        }

        public void StoreToDataCollection(CardFileDataCollection collection)
        {
            collection.ReplaceAll(Cards.Select(c => Mapping.Mapper.Map<CardDto>(c)), NextId);
        }
    }
}
