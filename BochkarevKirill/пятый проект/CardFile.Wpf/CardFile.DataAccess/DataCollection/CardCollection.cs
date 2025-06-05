using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _repairOrders = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                FirstName = "Иван",
                MiddleName = "Иванович",
                LastName = "Иванов",
                ArrivalDate = new DateTime(2023, 5, 10),
                RepairReason = "Замена двигателя", 
                CompletionDate = new DateTime(2023, 5, 20),
                RepairCost = 50000m 
            },
            new CardDto
            {
                Id = 2,
                FirstName = "Петр",
                MiddleName = "Петрович",
                LastName = "Петров",
                ArrivalDate = new DateTime(2023, 6, 1),
                RepairReason = "Покраска кузова",
                CompletionDate = null, 
                RepairCost = 30000m
            }
        };

        private int _currentId = 3;

        public int CurrentId
        {
            get => _currentId;
            internal set => _currentId = value;
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _repairOrders.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto repairOrder)
        {
            if (repairOrder.Id == 0)
            {
                var newOrder = repairOrder.Clone();
                newOrder.Id = _currentId++;
                _repairOrders.Add(newOrder);
                return newOrder.Id;
            }

            var index = _repairOrders.FindIndex(c => c.Id == repairOrder.Id);
            if (index >= 0)
            {
                _repairOrders[index] = repairOrder.Clone();
                return repairOrder.Id;
            }

            return -1;
        }

        public bool Delete(int orderId)
        {
            var index = _repairOrders.FindIndex(c => c.Id == orderId);
            if (index < 0) return false;

            _repairOrders.RemoveAt(index);
            return true;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source)
        {
            _repairOrders = source.Select(c => c.Clone()).ToList();
            _currentId = _repairOrders.Count > 0 ? _repairOrders.Max(c => c.Id) + 1 : 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source, int currentId)
        {
            _repairOrders = source.Select(c => c.Clone()).ToList();
            _currentId = currentId;
        }
    }
}