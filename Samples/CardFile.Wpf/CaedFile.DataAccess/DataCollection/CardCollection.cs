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
                FirstName = "Андрей",
                MiddleName = "Александрович",
                LastName = "Захаров",
                BirthDate = new DateTime(1984, 11, 15),
                PaymentAmount = 125000m,
                ChildrenCount = 2,
            },
            new CardDto
            {
                Id = 2,
                FirstName = "Людмила",
                MiddleName = "Викторовна",
                LastName = "Фролова",
                BirthDate = new DateTime(1995, 7, 18),
                PaymentAmount = 100000m,
                ChildrenCount = 1,
            },
            new CardDto
            {
                Id = 3,
                FirstName = "Геннадий",
                MiddleName = "Андреевич",
                LastName = "Василенко",
                BirthDate = new DateTime(1979, 3, 21),
                PaymentAmount = 150000m,
                ChildrenCount = 3,
            },
            new CardDto
            {
                Id = 4,
                FirstName = "Ирина",
                MiddleName = "Васильевна",
                LastName = "Мельникова",
                BirthDate = new DateTime(1989, 5, 8),
                PaymentAmount = 150000m,
                ChildrenCount = 3,
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
