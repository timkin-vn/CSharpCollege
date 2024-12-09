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

        public string LettClass { get; set; }

        public string ClassLed { get; set; }

        public string NumClass { get; set; }

        public string BadClass { get; set; } //класс корекции

        public int ChildrenCount { get; set; }

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
