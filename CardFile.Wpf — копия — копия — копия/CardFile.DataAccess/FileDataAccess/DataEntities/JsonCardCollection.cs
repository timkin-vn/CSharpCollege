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
    internal class JsonCardCollection
    {
        public int NextId { get; set; }

        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();

        public void FillFromDataCollection(CardFileDataCollection collection)
        {
            Cards.Clear();
            Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<JsonCard>(c)
            //new JsonCard
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
