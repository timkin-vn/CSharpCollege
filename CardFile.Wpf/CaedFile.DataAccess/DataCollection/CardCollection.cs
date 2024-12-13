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
                Bank_name = "Сбер",
                ATM_count = 500,
                Street = "Новохохловская",
                House = "12",
                City = "Москва",
                Card_number = "2345 5433 6545 6542",
                Money_count = 4567856.78M,
                Money_limit = 90000
            },
            new CardDto
            {
                Id = 2,
                Bank_name = "Alfa",
                ATM_count = 400,
                Street = "Арбат",
                House = "92",
                City = "Москва",
                Card_number = "2222 2222 2222 2222",
                Money_count = 9867856.78M,
                Money_limit = 120000
            },
            new CardDto
            {
                Id = 3,
                Bank_name = "Tinkoff",
                ATM_count = 600,
                Street = "Усачёва",
                House = "54",
                City = "Москва",
                Card_number = "3333 3333 3333 2344",
                Money_count = 7856.78M,
                Money_limit = 70000
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
