﻿using CardFile.Common.Infrastructure;
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

        public void FillFromCollection(CardCollection collection)
        {
            Cards.Clear();
            Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<XmlCard>(c)
            //new XmlCard
            //{
            //    Id = c.Id,
            //    NameMedication = c.NameMedication,
            //    TypeMedication = c.TypeMedication,
            //    DateManufacture = c.DateManufacture,
            //    DateExpiration = c.DateExpiration,
            //    CountMedication = c.CountMedication,
            //    PriceOneMedication = c.PriceOneMedication,
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
            //    NameMedication = c.NameMedication,
            //    TypeMedication = c.TypeMedication,
            //    DateManufacture = c.DateManufacture,
            //    DateExpiration = c.DateExpiration,
            //    CountMedication = c.CountMedication,
            //    PriceOneMedication = c.PriceOneMedication,
            //}
            ), CurrentId);
        }
    }
}
