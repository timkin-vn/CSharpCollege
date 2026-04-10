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
            MapperInitialization.PreRegister();

            _cards.Add(new CardDto
            {
                Id = 1,
                Title = "Wild Hunt",
                Series = "The Witcher 3",
                Platform = "PC",
                Genre = "Action RPG",
                Developer = "CD Projekt Red",
                ReleaseDate = new DateTime(2015, 5, 19),
                PurchaseDate = new DateTime(2022, 6, 5),
                CompletionDate = new DateTime(2022, 8, 17),
                Price = 899m,
                PersonalRating = 10,
                IsDigital = true
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "Persona 5 Royal",
                Series = "Persona",
                Platform = "PlayStation 5",
                Genre = "JRPG",
                Developer = "Atlus",
                ReleaseDate = new DateTime(2022, 10, 21),
                PurchaseDate = new DateTime(2023, 1, 13),
                CompletionDate = null,
                Price = 3490m,
                PersonalRating = 9,
                IsDigital = false
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "Baldur's Gate 3",
                Series = "Baldur's Gate",
                Platform = "PC",
                Genre = "CRPG",
                Developer = "Larian Studios",
                ReleaseDate = new DateTime(2023, 8, 3),
                PurchaseDate = new DateTime(2023, 8, 4),
                CompletionDate = null,
                Price = 1999m,
                PersonalRating = 10,
                IsDigital = true
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Title = "Forza Horizon 5",
                Series = "Forza Horizon",
                Platform = "Xbox Series X",
                Genre = "Racing",
                Developer = "Playground Games",
                ReleaseDate = new DateTime(2021, 11, 9),
                PurchaseDate = new DateTime(2024, 2, 10),
                CompletionDate = new DateTime(2024, 3, 2),
                Price = 4290m,
                PersonalRating = 8,
                IsDigital = false
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
            return false;
        }
    }
}
