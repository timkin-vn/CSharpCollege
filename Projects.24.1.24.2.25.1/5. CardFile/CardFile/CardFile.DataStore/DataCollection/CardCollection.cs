using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.DataCollection
{
    // Хранилище карточек товаров
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();
        internal int NextId { get; set; } = 5;

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                ProductName = "Телефон",
                ProductModel = "Galaxy Ultra",
                ProductColor = "Черный",
                ManufactureDate = new DateTime(2023, 1, 15),
                Category = "Электроника",
                Manufacturer = "Samsung",
                ReceiptDate = new DateTime(2023, 2, 10),
                WriteOffDate = null,
                Price = 69999m,
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                ProductName = "Ноутбук",
                ProductModel = "MacBook",
                ProductColor = "Бетон",
                ManufactureDate = new DateTime(2023, 3, 20),
                Category = "Компьютеры",
                Manufacturer = "Apple",
                ReceiptDate = new DateTime(2023, 4, 5),
                WriteOffDate = new DateTime(2024, 1, 15),
                Price = 119999m,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                ProductName = "Планшет",
                ProductModel = "iPad",
                ProductColor = "Панелька",
                ManufactureDate = new DateTime(2023, 5, 10),
                Category = "Электроника",
                Manufacturer = "Apple",
                ReceiptDate = new DateTime(2023, 6, 1),
                WriteOffDate = null,
                Price = 89999m,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                ProductName = "Наушники",
                ProductModel = "Шумоподавляющие",
                ProductColor = "Жёлтый",
                ManufactureDate = new DateTime(2023, 8, 1),
                Category = "Аксессуары",
                Manufacturer = "Pony",
                ReceiptDate = new DateTime(2023, 9, 10),
                WriteOffDate = null,
                Price = 249m,
            });

            MapperInitialization.PreRegister();
        }

        public void ReplaceAll(IEnumerable<CardDto> newCards)
        {
            _cards.Clear();
            _cards.AddRange(newCards.Select(c => c.Clone()));
            if (_cards.Any())
                NextId = _cards.Max(c => c.Id) + 1;
            else
                NextId = 1;
        }

        public void ReplaceAll(IEnumerable<CardDto> newCards, int nextId)
        {
            NextId = nextId;
            ReplaceAll(newCards);
        }

        public IEnumerable<CardDto> GetAll() => _cards.Select(c => c.Clone()).ToList();

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
            if (index < 0) return -1;

            _cards[index] = card.Clone();
            return card.Id;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index < 0) return false;
            _cards.RemoveAt(index);
            return true;
        }
    }
}