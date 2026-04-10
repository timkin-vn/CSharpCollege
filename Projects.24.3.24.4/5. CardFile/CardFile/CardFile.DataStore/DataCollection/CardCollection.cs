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
                Artist = "Daft Punk",
                AlbumTitle = "Discovery",
                Genre = "French house",
                ReleaseDate = new DateTime(2001, 3, 12),
                Label = "Virgin Records",
                Format = "CD",
                PurchaseDate = new DateTime(2023, 6, 18),
                LastListenDate = new DateTime(2026, 4, 2),
                Price = 1599m,
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Artist = "Radiohead",
                AlbumTitle = "OK Computer",
                Genre = "Alternative rock",
                ReleaseDate = new DateTime(1997, 5, 21),
                Label = "Parlophone",
                Format = "Vinyl",
                PurchaseDate = new DateTime(2024, 10, 5),
                LastListenDate = null,
                Price = 3490m,
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Artist = "Nirvana",
                AlbumTitle = "Nevermind",
                Genre = "Grunge",
                ReleaseDate = new DateTime(1991, 9, 24),
                Label = "DGC Records",
                Format = "Cassette",
                PurchaseDate = new DateTime(2022, 11, 13),
                LastListenDate = new DateTime(2026, 3, 28),
                Price = 990m,
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Artist = "Linkin Park",
                AlbumTitle = "Meteora",
                Genre = "Nu metal",
                ReleaseDate = new DateTime(2003, 3, 25),
                Label = "Warner Records",
                Format = "Digital",
                PurchaseDate = new DateTime(2025, 1, 14),
                LastListenDate = new DateTime(2026, 4, 8),
                Price = 499m,
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
