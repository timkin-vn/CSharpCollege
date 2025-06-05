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
                DishName = "Салат Чука",
                Category = "Салаты",
                Description = "Водоросли Чука с ореховым соусом",
                PortionSize = 200,
                Price = 350,
                IsAvaliableNow = true,
                SeasonDish = false,
                IsVegan = true,
                IsSpicy = false,
            },
            new CardDto
            {
                Id = 2,
                DishName = "Калифорния",
                Category = "Суши",
                Description = "Снежный краб, икра тобико",
                PortionSize = 400,
                Price = 650,
                IsAvaliableNow = true,
                SeasonDish = false,
                IsVegan = false,
                IsSpicy = false,
            },
            new CardDto
            {
                Id = 3,
                DishName = "Том Ям",
                Category = "Супы",
                Description = "Сливки, грибы, морепродукты",
                PortionSize = 300,
                Price = 450,
                IsAvaliableNow = true,
                SeasonDish = true,
                IsVegan = false,
                IsSpicy = true,
            },
            new CardDto
            {
                Id = 4,
                DishName = "Молочный улун",
                Category = "Чай",
                Description = "Изысканный чай с молочным привкусом",
                PortionSize = 600,
                Price = 350,
            },
        };

        internal int CurrentId = 5;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            //var result = new List<CardDto>();

            //foreach (var card in _cards)
            //{
            //    result.Add(card.Clone());
            //}

            //return result;

            // LINQ = Language integrated query
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
