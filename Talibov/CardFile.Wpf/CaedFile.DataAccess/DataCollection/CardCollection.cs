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
                Name = "Альфа банк",
                Description = "Банковское отделение",
                Street = "Улица Бринского",
                House = "97",
                City = "Нижний Новгород",
                MailIndex = 603157,
                Rating = 5,
                CounterReviews = 139,
                Status = "Закрыт по техническим причинам"
            },
            new CardDto
            {
                Id = 2,
                Name = "Кроконт",
                Description = "Магнит",
                Street = "Улица Есенина",
                House = "2",
                City = "Нижний Новгород",
                MailIndex = 603037,
                Rating = 4.4,
                CounterReviews = 177,
                Status = "Работает"
            },
            new CardDto
            {
                Id = 3,
                Name = "Аптека.ру",
                Description = "Аптека",
                Street = "улица Бринского",
                House = "2",
                City = "Нижний Новгород",
                MailIndex = 123456,
                Rating = 4.2,
                CounterReviews = 67,
                Status = "Работает"
            }
        };

        internal int CurrentId = 4;

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
