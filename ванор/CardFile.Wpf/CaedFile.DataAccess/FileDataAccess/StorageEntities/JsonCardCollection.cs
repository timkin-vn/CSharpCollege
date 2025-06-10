using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    public class JsonCardCollection
    {
        public int CurrentId { get; set; }

        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();

        public void FillFromCollection(CardCollection collection)
        {
            Cards.Clear();
            Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<JsonCard>(c)
            //new JsonCard
            //{
            //    Id = c.Id,
            //    FirstName = c.FirstName,
            //    MiddleName = c.MiddleName,
            //    LastName = c.LastName,
            //    BirthDate = c.BirthDate,
            //    PaymentAmount = c.PaymentAmount,
            //    Circulation = c.Circulation,
            //}
            ));

            CurrentId = collection.CurrentId;
        }

        public void SaveToCollection(CardCollection collection)
        {
            collection.ReplaceCollection(Cards.Select(c => Mapping.Mapper.Map<CardDto>(c)
            //new Dtos.CardDto
            //{
            //    Id = c.Id,
            //    FirstName = c.FirstName,
            //    MiddleName = c.MiddleName,
            //    LastName = c.LastName,
            //    BirthDate = c.BirthDate,
            //    PaymentAmount = c.PaymentAmount,
            //    Circulation = c.Circulation,
            //}
            ), CurrentId);
        }
    }
}
