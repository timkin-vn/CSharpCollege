using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.DataCollection
{
    public class CardCollection
    {
        private List<CardDto> _cards = new List<CardDto>
        {
            new CardDto
            {
                Id = 1,
                Category = "Процессор",
                Manufacturer = "Intel",
                Series = "Core i5-12600kf",
                ReleaseDate = new DateTime(2021, 10, 27),
                Price = 15360m,
                StockQuantity = 23,
                WarrantyMonths = 12,
                DiscontinuedDate = null,
                ProducingCountry = "Вьетнам",
            },
            new CardDto
            {
                Id = 2,
                Category = "Видеокарта",
                Manufacturer = "Nvidia",
                Series = "GeForce RTX 5090",
                ReleaseDate = new DateTime(2025, 01, 30),
                Price = 250000m,
                StockQuantity = 58,
                WarrantyMonths = 12,
                DiscontinuedDate = null,
                ProducingCountry = "Китай",
            },
            new CardDto
            {
                Id = 3,
                Category = "Блок питания",
                Manufacturer = "Cougar",
                Series = "GEX850",
                ReleaseDate = new DateTime(2020, 05, 14),
                Price = 7390m,
                StockQuantity = 8,
                WarrantyMonths = 12,
                DiscontinuedDate = new DateTime(2023, 11, 20),
                ProducingCountry = "Китай",
            },
        };

        internal int CurrentId = 4;

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
            CurrentId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> source, int currentId)
        {
            _cards.Clear();
            _cards.AddRange(source);
            CurrentId = currentId;
        }
    }
}
