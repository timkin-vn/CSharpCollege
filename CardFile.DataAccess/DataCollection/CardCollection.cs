using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId = 8; // Обновлено после добавления 7 элементов

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                Name = "Carbon Racing Bike",
                Type = "Велосипед",
                Manufacturer = "Specialized",
                Country = "США",
                Price = 250000,
                StockQuantity = 5,
                ShopNumber = 3,
                SportType = "Велоспорт",

            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Name = "Pro Tennis Racket",
                Type = "Ракетка",
                Manufacturer = "Wilson",
                Country = "Швейцария",
                Price = 18000,
                StockQuantity = 15,
                ShopNumber = 7,
                SportType = "Теннис"
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Name = "Olympic Barbell Set",
                Type = "Тренажер",
                Manufacturer = "Rogue",
                Country = "Канада",
                Price = 89000,
                StockQuantity = 8,
                ShopNumber = 2,
                SportType = "Тяжелая атлетика"
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "Trail Running Shoes",
                Type = "Обувь",
                Manufacturer = "Salomon",
                Country = "Франция",
                Price = 12000,
                StockQuantity = 22,
                ShopNumber = 5,
                SportType = "Бег"
            });
            _cards.Add(new CardDto
            {
                Id = 5,
                Name = "Competition Swimwear",
                Type = "Экипировка",
                Manufacturer = "Speedo",
                Country = "Великобритания",
                Price = 8000,
                StockQuantity = 30,
                ShopNumber = 4,
                SportType = "Плавание"
            });
            _cards.Add(new CardDto
            {
                Id = 6,
                Name = "Mountaineering Backpack",
                Type = "Снаряжение",
                Manufacturer = "The North Face",
                Country = "США",
                Price = 15000,
                StockQuantity = 12,
                ShopNumber = 1,
                SportType = "Альпинизм"
            });
            _cards.Add(new CardDto
            {
                Id = 7,
                Name = "Professional Dartboard",
                Type = "Инвентарь",
                Manufacturer = "Winmau",
                Country = "Великобритания",
                Price = 6500,
                StockQuantity = 18,
                ShopNumber = 6,
                SportType = "Дартс"
            });

            InitializeMapper.Register();
        }

        // Остальные методы без изменений
        public IEnumerable<CardDto> GetAll() =>
            _cards.Select(c => c.Clone()).ToList();

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                newCard.Id = NextId++;
                _cards.Add(newCard);
                return newCard.Id;
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