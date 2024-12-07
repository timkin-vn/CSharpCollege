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

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public int SubordinatesCount { get; set; }

        public decimal PaymentAmount { get; set; }

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
            //return new CardDto
            //{
            //    Id = Id,
            //    FirstName = FirstName,
            //    LastName = LastName,
            //    MiddleName = MiddleName,
            //    BirthDate = BirthDate,
            //    Department = Department,
            //    Position = Position,
            //    SubordinatesCount = SubordinatesCount,
            //    PaymentAmount = PaymentAmount,
            //};
        }

        public void Update(CardDto from)
        {
            Mapping.Mapper.Map(from, this);
            //FirstName = from.FirstName;
            //LastName = from.LastName;
            //MiddleName = from.MiddleName;
            //BirthDate = from.BirthDate;
            //Department = from.Department;
            //Position = from.Position;
            //SubordinatesCount = from.SubordinatesCount;
            //PaymentAmount = from.PaymentAmount;
        }
    }
}
