using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                BayNumber = 1,
                ClientName = "Иван Иванов",
                ServiceType = "Замена масла",
                DropOffDate = new DateTime(2025, 6, 4, 9, 0, 0),
                PickupDate = null,
                ServiceCost = 150.00m,
                IsPaid = true,
                MechanicName = "Алексей"
            },
            new CardDto
            {
                Id = 2,
                BayNumber = 2,
                ClientName = "Сергей Сергеев",
                ServiceType = "Шиномонтаж",
                DropOffDate = new DateTime(2025, 6, 4, 10, 30, 0),
                PickupDate = new DateTime(2025, 6, 4, 11, 30, 0),
                ServiceCost = 200.00m,
                IsPaid = false,
                MechanicName = "Мария"
            },
            new CardDto
            {
                Id = 3,
                BayNumber = 3,
                ClientName = "Василий Васильев",
                ServiceType = "Диагностика двигателя",
                DropOffDate = new DateTime(2025, 6, 4, 12, 00, 0),
                PickupDate = new DateTime(2025, 6, 4, 13, 30, 0),
                ServiceCost = 350.00m,
                IsPaid = true,
                MechanicName = "Дмитрий"
            },
            new CardDto
            {
                Id = 4,
                BayNumber = 4,
                ClientName = "Николай Николаев",
                ServiceType = "Проверка тормозов",
                DropOffDate = new DateTime(2025, 6, 4, 14, 15, 0),
                PickupDate = null,
                ServiceCost = 150.00m,
                IsPaid = true,
                MechanicName = "Ольга"
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
