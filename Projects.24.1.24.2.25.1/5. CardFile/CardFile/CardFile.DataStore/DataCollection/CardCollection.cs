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

        internal int NextId { get; set; } = 7;

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                Name = "Pudge",
                LoreName = "The Butcher",
                PrimaryAttribute = "Сила",
                AttackType = "Ближний бой",
                Role = "Инициатор",
                ReleaseDate = new DateTime(2013, 7, 9),
                ReworkDate = null,
                WinRate = 50.9m,
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Name = "Invoker",
                LoreName = "Kael",
                PrimaryAttribute = "Универсальный",
                AttackType = "Дальний бой",
                Role = "Нюкер",
                ReleaseDate = new DateTime(2013, 7, 9),
                ReworkDate = new DateTime(2023, 3, 7),
                WinRate = 49.4m,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Name = "Juggernaut",
                LoreName = "Yurnero",
                PrimaryAttribute = "Ловкость",
                AttackType = "Ближний бой",
                Role = "Керри",
                ReleaseDate = new DateTime(2013, 7, 9),
                ReworkDate = null,
                WinRate = 51.8m,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "Crystal Maiden",
                LoreName = "Rylai",
                PrimaryAttribute = "Интеллект",
                AttackType = "Дальний бой",
                Role = "Поддержка",
                ReleaseDate = new DateTime(2013, 7, 9),
                ReworkDate = new DateTime(2018, 8, 1),
                WinRate = 52.3m,
            });
            _cards.Add(new CardDto
            {
                Id = 5,
                Name = "Phantom Assassin",
                LoreName = "Mortred",
                PrimaryAttribute = "Ловкость",
                AttackType = "Ближний бой",
                Role = "Керри",
                ReleaseDate = new DateTime(2013, 7, 9),
                ReworkDate = null,
                WinRate = 50.2m,
            });
            _cards.Add(new CardDto
            {
                Id = 6,
                Name = "Axe",
                LoreName = "Mogul Khan",
                PrimaryAttribute = "Сила",
                AttackType = "Ближний бой",
                Role = "Инициатор",
                ReleaseDate = new DateTime(2013, 7, 9),
                ReworkDate = new DateTime(2019, 1, 1),
                WinRate = 51.1m,
            });

            MapperInitialization.PreRegister();
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

        internal void ReplaceAll(IEnumerable<CardDto> newCollection)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = nextId;
        }
    }
}
