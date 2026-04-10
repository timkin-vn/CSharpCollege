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
            MapperInitialization.PreRegister();

            _cards.Add(new CardDto
            {
                Id = 1,
                ClientFirstName = "Андрей",
                ClientLastName = "Захаров",
                ProductName = "Ноутбук Lenovo IdeaPad",
                OrderDate = new DateTime(2026, 3, 11),
                Address = "г. Нижний Новгород, ул. Горького, д. 14",
                DeliveryMethod = "Курьер",
                ShippingDate = new DateTime(2026, 3, 12),
                ReceivedDate = new DateTime(2026, 3, 14),
                TotalAmount = 74990m,
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                ClientFirstName = "Нина",
                ClientLastName = "Шевченко",
                ProductName = "Смартфон Samsung Galaxy A55",
                OrderDate = new DateTime(2026, 3, 18),
                Address = "г. Казань, ул. Баумана, д. 8",
                DeliveryMethod = "Пункт выдачи",
                ShippingDate = new DateTime(2026, 3, 19),
                ReceivedDate = new DateTime(2026, 3, 22),
                TotalAmount = 38990m,
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                ClientFirstName = "Виктор",
                ClientLastName = "Васильев",
                ProductName = "Игровая мышь Logitech G102",
                OrderDate = new DateTime(2026, 4, 2),
                Address = "г. Самара, ул. Ленина, д. 25",
                DeliveryMethod = "Почта",
                ShippingDate = new DateTime(2026, 4, 3),
                ReceivedDate = null,
                TotalAmount = 2490m,
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                ClientFirstName = "Ольга",
                ClientLastName = "Меднис",
                ProductName = "Наушники Sony WH-CH720N",
                OrderDate = new DateTime(2026, 4, 5),
                Address = "г. Москва, ул. Тверская, д. 19",
                DeliveryMethod = "Курьер",
                ShippingDate = new DateTime(2026, 4, 6),
                ReceivedDate = null,
                TotalAmount = 9990m,
            });
        }

        public IEnumerable<CardDto> GetAll()
        {
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

        public void ReplaceAll(IEnumerable<CardDto> cards)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = _cards.Max(c => c.Id) + 1;
        }

        public void ReplaceAll(IEnumerable<CardDto> cards, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = nextId;
        }
    }
}
