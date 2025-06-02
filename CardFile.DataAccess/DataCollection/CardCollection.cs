using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                Name = "Т-34",
                Manufacturer = "СССР",
                Designer = "КБ Морозова",
                ProductionYear = 1940,
                Type = "Средний танк",
                MaxSpeed = 53,
                Gun = "76-мм Ф-34",
                Weight = 26.5
            },
            new CardDto
            {
                Id = 2,
                Name = "Ил-2",
                Manufacturer = "СССР",
                Designer = "Сергей Ильюшин",
                ProductionYear = 1941,
                Type = "Штурмовик",
                MaxSpeed = 414,
                Gun = "23-мм пушки ВЯ",
                Weight = 6.3
            },
            new CardDto
            {
                Id = 3,
                Name = "Panther",
                Manufacturer = "Германия",
                Designer = "MAN AG",
                ProductionYear = 1943,
                Type = "Танк",
                MaxSpeed = 55,
                Gun = "75-мм KwK 42",
                Weight = 44.8
            }
        };

        internal int CurrentId = 4;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto cardDto)
        {
            if (cardDto.Id == 0)
            {
                var newCard = cardDto.Clone();
                var id = CurrentId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == cardDto.Id);
            if (index >= 0)
            {
                _cards[index] = cardDto.Clone();
                return cardDto.Id;
            }

            return -1;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index < 0)
            {
                return false;
            }

            _cards.RemoveAt(index);
            return true;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source)
        {
            _cards.Clear();
            _cards.AddRange(source);
            CurrentId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source, int currentId)
        {
            _cards.Clear();
            _cards.AddRange(source);
            CurrentId = currentId;
        }
    }
}