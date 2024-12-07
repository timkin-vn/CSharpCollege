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

        public decimal PaymentAmount { get; set; }

        public int ChildrenCount { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }

        public string Position { get; set; }

        public string Notes { get; set; }

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
            //return new CardDto
            //{
            //    Id = Id,
            //    FirstName = FirstName,
            //    MiddleName = MiddleName,
            //    LastName = LastName,
            //    BirthDate = BirthDate,
            //    PaymentAmount = PaymentAmount,
            //    ChildrenCount = ChildrenCount,
            //    Email = Email;
            //    PhoneNumber = PhoneNumber;
            //    Adress = Adress;
            //    Position = Position;
            //    Notes = Notes;
            //};
        }

        public void Update(CardDto newCard)
        {
            Mapping.Mapper.Map(newCard, this);
            //FirstName = newCard.FirstName;
            //MiddleName = newCard.MiddleName;
            //LastName = newCard.LastName;
            //BirthDate = newCard.BirthDate;
            //PaymentAmount = newCard.PaymentAmount;
            //ChildrenCount = newCard.ChildrenCount;
            //Email = newCard.Email;
            //PhoneNumber = newCard.PhoneNumber;
            //Adress = newCard.Adress;
            //Position = newCard.Position;
            //Notes = newCard.Notes;
        }
    }
}
