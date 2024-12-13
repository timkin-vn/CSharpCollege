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
        public string Bank_name { get; set; }
        public int ATM_count { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string City { get; set; }
        public decimal Money_count { get; set; }
        public int Money_limit { get; set; }
        public string Card_number { get; set; }



        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
            
        }

        public void Update(CardDto newCard)
        {
            Mapping.Mapper.Map(newCard, this);
        }
    }
}
