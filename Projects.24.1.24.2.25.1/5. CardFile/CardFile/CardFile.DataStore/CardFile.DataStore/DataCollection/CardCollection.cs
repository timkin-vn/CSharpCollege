using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId { get; set; } = 5;

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                ProductName = "Беспроводная мышь",
                Category = "Компьютерная техника",
                Brand = "Logitech",
                DateAdded = new DateTime(2025, 2, 10),
                Supplier = "ООО ТехноСнаб",
                StorageLocation = "Склад A-01",
                LastRestockDate = new DateTime(2025, 8, 12),
                ExpirationDate = null,
                Price = 2499m,
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                ProductName = "Кофе молотый 250 г",
                Category = "Продукты питания",
                Brand = "Жардин",
                DateAdded = new DateTime(2025, 4, 5),
                Supplier = "ОптПоставка",
                StorageLocation = "Склад B-04",
                LastRestockDate = new DateTime(2025, 9, 1),
                ExpirationDate = new DateTime(2026, 12, 31),
                Price = 489m,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                ProductName = "Настольная лампа",
                Category = "Товары для дома",
                Brand = "Эра",
                DateAdded = new DateTime(2025, 1, 20),
                Supplier = "ДомМаркет",
                StorageLocation = "Зал C-02",
                LastRestockDate = new DateTime(2025, 7, 18),
                ExpirationDate = null,
                Price = 1790m,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                ProductName = "Шампунь 400 мл",
                Category = "Уходовая косметика",
                Brand = "Nivea",
                DateAdded = new DateTime(2025, 3, 15),
                Supplier = "КосметикТрейд",
                StorageLocation = "Склад B-02",
                LastRestockDate = new DateTime(2025, 8, 28),
                ExpirationDate = new DateTime(2027, 3, 15),
                Price = 359m,
            });

            MapperInitialization.PreRegister();
        }

        public IEnumerable<CardDto> GetAll()
        {
            //return _cards;

            //var result = new List<CardDto>();

            //foreach (var card in _cards)
            //{
            //    result.Add(card.Clone());
            //}

            //return result;

            return _cards.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                var id = NextId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == card.Id);
            if (index < 0)
            {
                return -1;
            }

            _cards[index] = card.Clone();
            return card.Id;
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

        internal void ReplaceAll(IEnumerable<CardDto> newCollection)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = nextId;
        }
    }
}
