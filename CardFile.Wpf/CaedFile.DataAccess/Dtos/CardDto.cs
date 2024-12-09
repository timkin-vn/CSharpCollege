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

        public string Tutor { get; set; }

        public string CourseNumber { get; set; }

        public string SpecialProgram { get; set; } //Специальная программа(типо усиленная группа)

        public int NumberStudents { get; set; }

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
