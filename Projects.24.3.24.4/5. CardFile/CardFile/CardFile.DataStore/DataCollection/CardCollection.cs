using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId { get; set; } = 3;

        public CardCollection()
        {

            _cards.Add(new CardDto
            {
                Id = 1,
                Brand = "Toyota",
                ModelName = "Camry",
                Year = 2021,
                VinCode = "ABC123456789",
                Price = 3500000m,
                EngineType = "Бензин",
                EngineVolume = 2.5,
                Mileage = 15000,
                Color = "Черный",
                LastServiceDate = DateTime.Now.AddMonths(-3)
            });


            _cards.Add(new CardDto
            {
                Id = 2,
                Brand = "BMW",
                ModelName = "X5",
                Year = 2022,
                VinCode = "XYZ987654321",
                Price = 8200000m,
                EngineType = "Дизель",
                EngineVolume = 3.0,
                Mileage = 5000,
                Color = "Белый",
                LastServiceDate = null
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
            var card = _cards.FirstOrDefault(c => c.Id == cardId);
            if (card != null)
            {
                _cards.Remove(card);
                return true;
            }
            return false;
        }
    }
}