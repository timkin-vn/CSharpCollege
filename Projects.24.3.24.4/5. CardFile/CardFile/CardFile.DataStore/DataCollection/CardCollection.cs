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
                Brand = "Toyota",
                Model = "Camry",
                LicensePlate = "A123BC52",
                ManufactureDate = new DateTime(2018, 4, 12),
                Color = "Чёрный",
                Owner = "Иван Петров",
                RegistrationDate = new DateTime(2018, 5, 3),
                DeregistrationDate = null,
                Price = 2450000m,
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Brand = "BMW",
                Model = "320i",
                LicensePlate = "M777OP77",
                ManufactureDate = new DateTime(2020, 9, 18),
                Color = "Белый",
                Owner = "Алексей Смирнов",
                RegistrationDate = new DateTime(2020, 10, 1),
                DeregistrationDate = null,
                Price = 3200000m,
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Brand = "Lada",
                Model = "Vesta",
                LicensePlate = "K456TT152",
                ManufactureDate = new DateTime(2017, 2, 7),
                Color = "Серый",
                Owner = "Мария Орлова",
                RegistrationDate = new DateTime(2017, 2, 20),
                DeregistrationDate = new DateTime(2025, 12, 15),
                Price = 980000m,
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Brand = "Tesla",
                Model = "Model 3",
                LicensePlate = "E001EE99",
                ManufactureDate = new DateTime(2023, 6, 10),
                Color = "Красный",
                Owner = "Дмитрий Волков",
                RegistrationDate = new DateTime(2023, 7, 2),
                DeregistrationDate = null,
                Price = 4700000m,
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
            NextId = _cards.Count == 0 ? 1 : _cards.Max(c => c.Id) + 1;
        }

        public void ReplaceAll(IEnumerable<CardDto> cards, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = nextId;
        }
    }
}
