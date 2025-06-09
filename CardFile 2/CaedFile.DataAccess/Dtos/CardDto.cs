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

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal HeightAmount { get; set; }

        public int Weight { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }

        public CardDto Clone()
        {
            return (CardDto)this.MemberwiseClone();
        }

        public void Update(CardDto updatedCard)
        {
            FirstName = updatedCard.FirstName;
            MiddleName = updatedCard.MiddleName;
            LastName = updatedCard.LastName;
            BirthDate = updatedCard.BirthDate;
            HeightAmount = updatedCard.HeightAmount;
            Weight = updatedCard.Weight;
            City = updatedCard.City;
            Street = updatedCard.Street;
            House = updatedCard.House;
        }
    }
}
