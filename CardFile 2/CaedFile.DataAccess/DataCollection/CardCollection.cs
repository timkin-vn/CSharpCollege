using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
                FirstName = "Вадим",
                MiddleName = "Вадимович",
                LastName = "Вадим",
                BirthDate = new DateTime(1941, 04, 04),
                HeightAmount = 215,
                Weight = 80,
                City = "Нижний Новгород",
                Street = "Бурнаковка",
                House = 4
            },
            new CardDto
            {
                Id = 2,
                FirstName = "Маргарита",
                MiddleName = "Вадимовна",
                LastName = "Фикова",
                BirthDate = new DateTime(1946, 03, 11),
                HeightAmount = 145,
                Weight = 40,
                City = "Нижний Новгород",
                Street = "Бурнаковка",
                House = 4
            },
            new CardDto
            {
                Id = 3,
                FirstName = "Ярослав",
                MiddleName = "Инвокеров",
                LastName = "Артемов",
                BirthDate = new DateTime(2020, 1, 25),
                HeightAmount = 100,
                Weight  = 200,
                City = "Дно",
                Street = "Советская",
                House = 27
            },
            new CardDto
            {
                Id = 4,
                FirstName = "Саф",
                MiddleName = "Пичугин",
                LastName = "Германович",
                BirthDate = new DateTime(2007, 11, 11),
                HeightAmount = 190,
                Weight  = 75,
                City = "Санкт-Петербург",
                Street = "Чайковского",
                House = 15
            },
        };

        internal int CurrentId = 5;

        public CardCollection()
        {
            MapperInitialize.Initialize();
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone());
        }

        public CardDto Get(int id)
        {
            return _cards.FirstOrDefault(c => c.Id == id)?.Clone();
        }

        public bool Update(CardDto card)
        {
            if (card.Id == 0)
            {
                return false;
            }

            var existingCard = _cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard == null)
            {
                return false;
            }

            existingCard.Update(card);
            return true;
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                newCard.Id = CurrentId++;
                _cards.Add(newCard);
                return newCard.Id;
            }

            var existingCard = _cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard == null)
            {
                return -1;
            }

            existingCard.Update(card);
            return card.Id;
        }

        public bool Delete(int id)
        {
            var existingCard = _cards.FirstOrDefault(c => c.Id == id);

            if (existingCard == null)
            {
                return false;
            }

            _cards.Remove(existingCard);
            return true;
        }

        internal void ReplaceCollection(IEnumerable<CardDto> collection)
        {
            _cards.Clear();
            _cards.AddRange(collection);
            CurrentId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceCollection(IEnumerable<CardDto> collection, int currentId)
        {
            _cards.Clear();
            _cards.AddRange(collection);
            CurrentId = currentId;
        }
    }
}
