
using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Brand = "Toyota",
                Model = "Camry",
                Year = 2021,
                Color = "Белый",
                Mileage = 42000,
                Price = 2850000m,
                FuelType = "Бензин",
                EngineVolume = 2.5m,
                TransmissionType = "Автомат",
                HasAirConditioning = true,
                AddedDate = new DateTime(2024, 3, 15),
                SoldDate = null,
                Vin = "JTNB11HK0K3000001"
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Brand = "Kia",
                Model = "Rio",
                Year = 2019,
                Color = "Серый",
                Mileage = 78000,
                Price = 1350000m,
                FuelType = "Бензин",
                EngineVolume = 1.6m,
                TransmissionType = "Механика",
                HasAirConditioning = true,
                AddedDate = new DateTime(2023, 8, 20),
                SoldDate = new DateTime(2024, 1, 12),
                Vin = "Z94C241BBKR000002"
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Brand = "Volkswagen",
                Model = "Golf",
                Year = 2020,
                Color = "Синий",
                Mileage = 51000,
                Price = 2100000m,
                FuelType = "Бензин",
                EngineVolume = 1.4m,
                TransmissionType = "Робот",
                HasAirConditioning = true,
                AddedDate = new DateTime(2024, 5, 10),
                SoldDate = null,
                Vin = "WVWZZZAUZLW000003"
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Brand = "Lada",
                Model = "Vesta",
                Year = 2022,
                Color = "Красный",
                Mileage = 25000,
                Price = 1450000m,
                FuelType = "Бензин",
                EngineVolume = 1.6m,
                TransmissionType = "Механика",
                HasAirConditioning = false,
                AddedDate = new DateTime(2024, 6, 2),
                SoldDate = null,
                Vin = "XTAGFL110NY000004"
            });

            MapperInitialization.PreRegister();
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(card => card.Clone()).ToList();
        }

        public int Save(CardDto card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            if (card.Id == 0)
            {
                CardDto newCard = card.Clone();
                int id = NextId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            int index = _cards.FindIndex(c => c.Id == card.Id);

            if (index < 0)
                return -1;

            _cards[index] = card.Clone();
            return card.Id;
        }

        public bool Delete(int cardId)
        {
            int index = _cards.FindIndex(c => c.Id == cardId);

            if (index < 0)
                return false;

            _cards.RemoveAt(index);
            return true;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);

            NextId = _cards.Count == 0
                ? 1
                : _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(
            IEnumerable<CardDto> newCollection, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = Math.Max(nextId, _cards.Count == 0
                ? 1
                : _cards.Max(c => c.Id) + 1);
        }
    }
}
