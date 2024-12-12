
using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string NameMedication { get; set; }
        public string TypeMedication { get; set; }
        public DateTime DateManufacture { get; set; }

        public DateTime DateExpiration { get; set; }

        public int CountMedication { get; set; }

        public decimal PriceOneMedication { get; set; }

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
            //return new CardDto
            //{
            //   Id = Id,
            //    NameMedication = NameMedication,
            //    TypeMedication = TypeMedication,
            //    DateManufacture = DateManufacture,
            //    DateExpiration = DateExpiration,
            //    CountMedication = CountMedication,
            //    PriceOneMedication = PriceOneMedication,
            //};
        }

        public void Update(CardDto newCard)
        {
            Mapping.Mapper.Map(newCard, this);
           
            //    NameMedication = NameMedication,
            //    TypeMedication = TypeMedication,
            //    DateManufacture = DateManufacture,
            //    DateExpiration = DateExpiration,
            //    CountMedication = CountMedication,
            //    PriceOneMedication = PriceOneMedication,
        }
    }
}
