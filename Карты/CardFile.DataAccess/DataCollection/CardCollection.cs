using System;
using System.Collections.Generic;
using System.Linq;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                LicensePlate = "АМ219Р",
                OwnerName = "Сергей Бурунов",
                VehicleType = "Легковая",
                EntryDate = new DateTime(2025, 6, 1, 8, 0, 0),
                ExitDate = null,
                HourlyRate = 100m,
                IsPaid = true,
                ParkingSpot = 1
            },
            new CardDto
            {
                Id = 2,
                LicensePlate = "ВО010Р",
                OwnerName = "Илон Макс",
                VehicleType = "Middle",
                EntryDate = new DateTime(2025, 6, 1, 9, 15, 0),
                ExitDate = new DateTime(2025, 6, 1, 14, 0, 0),
                HourlyRate = 150m,
                IsPaid = false,
                ParkingSpot = 2
            },
            new CardDto
            {
                Id = 3,
                LicensePlate = "ВИ002Р",
                OwnerName = "Гена Букинг",
                VehicleType = "Грузовая",
                EntryDate = new DateTime(2025, 6, 1, 10, 30, 0),
                ExitDate = new DateTime(2025, 6, 1, 16, 0, 0),
                HourlyRate = 200m,
                IsPaid = true,
                ParkingSpot = 3
            },
            new CardDto
            {
                Id = 4,
                LicensePlate = "ОО100В",
                OwnerName = "Яна Иванова",
                VehicleType = "Легковая",
                EntryDate = new DateTime(2025, 6, 1, 11, 45, 0),
                ExitDate = null,
                HourlyRate = 100m,
                IsPaid = true,
                ParkingSpot = 4
            }
        };

        internal int CurrentId = 5;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto cardDto)
        {
            if (cardDto.Id == 0)
            {
                var newCard = cardDto.Clone();
                var id = CurrentId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == cardDto.Id);
            if (index >= 0)
            {
                _cards[index] = cardDto.Clone();
                return cardDto.Id;
            }

            return -1;
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

        internal void ReplaceAll(IEnumerable<CardDto> source)
        {
            _cards.Clear();
            _cards.AddRange(source);
            CurrentId = _cards.Any() ? _cards.Max(c => c.Id) + 1 : 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source, int currentId)
        {
            _cards.Clear();
            _cards.AddRange(source);
            CurrentId = currentId;
        }
    }
}