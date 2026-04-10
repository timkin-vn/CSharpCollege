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
                Title = "Half-Life 2",
                Studio = "Valve",
                Genre = "Шутер",
                ReleaseDate = new DateTime(2004, 11, 16),
                Platform = "PC",
                Publisher = "Valve",
                PurchaseDate = new DateTime(2021, 12, 25),
                CompletionDate = new DateTime(2022, 1, 4),
                Price = 499m,
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "Hades",
                Studio = "Supergiant Games",
                Genre = "Roguelike",
                ReleaseDate = new DateTime(2020, 9, 17),
                Platform = "PC",
                Publisher = "Supergiant Games",
                PurchaseDate = new DateTime(2024, 7, 19),
                CompletionDate = null,
                Price = 1100m,
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "The Witcher 3",
                Studio = "CD Projekt Red",
                Genre = "RPG",
                ReleaseDate = new DateTime(2015, 5, 19),
                Platform = "PlayStation 5",
                Publisher = "CD Projekt",
                PurchaseDate = new DateTime(2023, 3, 8),
                CompletionDate = new DateTime(2023, 6, 30),
                Price = 1499m,
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Title = "ULTRAKILL",
                Studio = "Arsi \"Hakita\" Patala",
                Genre = "Action",
                ReleaseDate = new DateTime(2020, 9, 3),
                Platform = "PC",
                Publisher = "New Blood Interactive",
                PurchaseDate = new DateTime(2025, 1, 11),
                CompletionDate = null,
                Price = 899m,
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
