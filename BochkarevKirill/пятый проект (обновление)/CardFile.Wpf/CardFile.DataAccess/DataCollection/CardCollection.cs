using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
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
        public int CurrentId => _currentId;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            //var result = new List<CardDto>();

            //foreach (var card in _cards)
            //{
            //    result.Add(card.Clone());
            //}

            //return result;

            // LINQ = Language integrated query
            return _repairOrders.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto cardDto)
        {
            if (cardDto.Id == 0)
            {
                var newCard = cardDto.Clone();
                var id = _currentId++;
                newCard.Id = id;
                _repairOrders.Add(newCard);
                return id;
            }

            var index = _repairOrders.FindIndex(c => c.Id == cardDto.Id);
            if (index >= 0)
            {
                _repairOrders[index] = cardDto.Clone();
                return cardDto.Id;
            }

            return -1;
        }

        public bool Delete(int cardId)
        {
            var index = _repairOrders.FindIndex(c => c.Id == cardId);
            if (index < 0)
            {
                return false;
            }

            _repairOrders.RemoveAt(index);
            return true;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source)
        {
            _repairOrders.Clear();
            _repairOrders.AddRange(source);
            _currentId = _repairOrders.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source, int currentId)
        {
            _repairOrders.Clear();
            _repairOrders.AddRange(source);
            _currentId = currentId;
        }
    }
}