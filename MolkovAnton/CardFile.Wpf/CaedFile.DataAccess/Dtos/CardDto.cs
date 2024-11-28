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

        public string LicenseNumber { get; set; }

        public string LinceseName { get; set; }

        public string IssuedLicense { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                BirthDate = BirthDate,
                PaymentAmount = PaymentAmount,
                ChildrenCount = ChildrenCount,
                LicenseNumber = LicenseNumber,
                IssuedLicense = IssuedLicense,
                LinceseName = LinceseName,
            };
        }

        public void Update(CardDto newCard)
        {
            FirstName = newCard.FirstName;
            MiddleName = newCard.MiddleName;
            LastName = newCard.LastName;
            BirthDate = newCard.BirthDate;
            PaymentAmount = newCard.PaymentAmount;
            ChildrenCount = newCard.ChildrenCount;
            LicenseNumber = newCard.LicenseNumber;
            IssuedLicense = newCard.IssuedLicense;
            LinceseName = newCard.LinceseName;
        }
    }
}
