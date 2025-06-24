using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    [XmlRoot("CardCollection")]
    public class XmlCardCollection
    {
        [XmlAttribute("NextId")]
        public int CurrentId { get; set; }

        [XmlArray("Cards")]
        [XmlArrayItem("Card")]
        public List<XmlCard> Cards { get; set; } = new List<XmlCard>();

        public void FillFromCollection(CardProductsCollection collection)
        {
            Cards.Clear();
            Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<XmlCard>(c)
            //new XmlCard
            //{
            //    Id = c.Id,
            //    FirstName = c.FirstName,
            //    MiddleName = c.MiddleName,
            //    LastName = c.LastName,
            //    BirthDate = c.BirthDate,
            //    PaymentAmount = c.PaymentAmount,
            //    ChildrenCount = c.ChildrenCount,
            //}
            ));

            CurrentId = collection.CurrentId;
        }

        public void SaveToCollection(CardProductsCollection collection)
        {
            collection.ReplaceCollection(Cards.Select(c => Mapping.Mapper.Map<CardProductsDto>(c)
            //new Dtos.CardDto
            //{
            //    Id = c.Id,
            //    FirstName = c.FirstName,
            //    MiddleName = c.MiddleName,
            //    LastName = c.LastName,
            //    BirthDate = c.BirthDate,
            //    PaymentAmount = c.PaymentAmount,
            //    ChildrenCount = c.ChildrenCount,
            //}
            ), CurrentId);
        }
    }
}
