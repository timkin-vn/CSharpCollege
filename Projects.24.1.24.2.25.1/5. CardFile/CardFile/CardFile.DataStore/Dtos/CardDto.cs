using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LoreName { get; set; }

        public string PrimaryAttribute { get; set; }

        public string AttackType { get; set; }

        public string Role { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime? ReworkDate { get; set; }

        public decimal WinRate { get; set; }

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
