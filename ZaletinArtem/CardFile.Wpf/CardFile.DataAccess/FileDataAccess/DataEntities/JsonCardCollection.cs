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
            Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<JsonCard>(c)));
            NextId = collection.NextId;
        }

        public void StoreToDataCollection(CardFileDataCollection collection)
        {
            collection.ReplaceAll(Cards.Select(c => Mapping.Mapper.Map<CardDto>(c)), NextId);
        }
    }
}