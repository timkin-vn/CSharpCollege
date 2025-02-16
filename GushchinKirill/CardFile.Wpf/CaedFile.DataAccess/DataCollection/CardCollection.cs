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
                Name = "Зоопарк Лимпопо",
                Description = "Зоопарк,развлечения ",
                Street = "ул. Ярошенко",
                House = "7Б",
                City = "Нижний Новгород",
                MailIndex = 603035,
                Rating = 4.8,   
                CounterReviews = 11780,
                Status = "Открыт"
            },
            new CardDto
            {
                Id = 2,
                Name = "Музей Усадьба Рукавишниковых",
                Description = "Художественный музей в отреставрированной усадьбе на берегу Волги, в котором проводятся экскурсии.",
                Street = "Верхневолжская набережная",
                House = "7",
                City = "Нижний Новгород",
                MailIndex = 603005,
                Rating = 4.7,
                CounterReviews = 2098,
                Status = "Открыт"
            },
            new CardDto
            {
                Id = 3,
                Name = "Нижегородский кремль",
                Description = "Кирпичная средневековая крепость с 12 башнями – сейчас здесь располагаются музеи и проходят концерты.",
                Street = "пл. Минина и Пожарского",
                House = "",
                City = "Нижний Новгород",
                MailIndex = 603005,
                Rating = 4.6,
                CounterReviews = 27356,
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
