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
                FirstName = "Иван",
                MiddleName = "Давыдов",
                LastName = "Захаров",
                BirthDate = new DateTime(2002, 1, 15),
                PaymentAmount = 625000m,
                Sud = 5,
                ArticleNumber = "228",
                ArticleName = "Котики",
                IssuedArticle = "Кореев Я.Г"
            },
            new CardDto
            {
                Id = 2,
                FirstName = "Хошино",
                MiddleName = "Таканаши",
                LastName = "Фролова",
                BirthDate = new DateTime(1995, 7, 13),
                PaymentAmount = 230000m,
                Sud = 0,
                ArticleNumber = "178",
                ArticleName = "Уклонение от налогов",
                IssuedArticle = "Пуджеро А.А"
            },
            new CardDto
            {
                Id = 3,
                FirstName = "Геннадий",
                MiddleName = "Андреевич",
                LastName = "Васильев",
                BirthDate = new DateTime(1990, 2, 1),
                PaymentAmount = 10000m,
                Sud = 3,
                ArticleNumber = "281.1",
                ArticleName = "Сепаратизм",
                IssuedArticle = "Чен Б.Я"
            },
            new CardDto
            {
                Id = 4,
                FirstName = "Ирина",
                MiddleName = "Васильевна",
                LastName = "Кореева",
                BirthDate = new DateTime(1999, 5, 8),
                PaymentAmount = 500000m,
                Sud = 1,
                ArticleNumber = "186",
                ArticleName = "Подделка денег",
                IssuedArticle = "Фурион И.С"
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
