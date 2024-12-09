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
    [XmlRoot("CardCollection")]
    public class XmlCardCollection
    {
        [XmlAttribute("NextId")]
        public int NextId { get; set; }

        [XmlArray("Cards")]
        [XmlArrayItem("Card")]
        public List<XmlCard> Cards { get; set; } = new List<XmlCard>();

        public void FillFromDataCollection(CardFileDataCollection collection)
        {
            Cards.Clear();
            Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<XmlCard>(c)
            //new XmlCard
            //{
            //    Id = c.Id,
            //    FirstName = c.FirstName,
            //    LastName = c.LastName,
            //    MiddleName = c.MiddleName,
            //    BirthDate = c.BirthDate,
            //    Department = c.Department,
            //    Position = c.Position,
            //    PaymentAmount = c.PaymentAmount,
            //    SubordinatesCount = c.SubordinatesCount,
            //}
            ));

            NextId = collection.NextId;
        }

        public void StoreToDataCollection(CardFileDataCollection collection)
        {
            collection.ReplaceAll(Cards.Select(c => Mapping.Mapper.Map<CardDto>(c)
            //new CardDto
            //{
            //    Id = c.Id,
            //    FirstName = c.FirstName,
            //    LastName = c.LastName,
            //    MiddleName = c.MiddleName,
            //    BirthDate = c.BirthDate,
            //    Department = c.Department,
            //    Position = c.Position,
            //    PaymentAmount = c.PaymentAmount,
            //    SubordinatesCount = c.SubordinatesCount,
            //}
            ), NextId);
        }
    }
}
