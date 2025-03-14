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

        public string Title { get; set; }

        public DateTime EXP { get; set; }

        public string Fabricator { get; set; }

        public string Section { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

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
