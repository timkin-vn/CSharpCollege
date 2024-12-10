using CardFile.DataAccess.Dtos;
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
                LastName = "Лапин",
                BirthDate = new DateTime(1985, 12, 11),
                PaymentAmount = 120000m,
                ChildrenCount = 2,
                City = "Нижний Новгород",
                Street = "Рождественская",
                House = "17",
            },
            new CardDto
            {
                Id = 2,
                FirstName = "Людмила",
                MiddleName = "Дмитриевна",
                LastName = "Фролова",
                BirthDate = new DateTime(1995, 7, 18),
                PaymentAmount = 110000m,
                ChildrenCount = 1,
                City = "Нижний Новгород", 
                Street = "Рождественская", 
                House = "17",
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
                City = "Нижний Новгород",
                Street = "Рождественская",
                House = "17",
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
                City = "Нижний Новгород",
                Street = "Рождественская",
                House = "17",
            },
        };

        private int _currentId = 5;

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
                newCard.Id = _currentId++;
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
    }
}
