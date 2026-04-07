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
            _cards.Add(new CardDto
            {
                Id = 1,
                Nickname = "Ame",
                RealName = "Wang Chunyu",
                Country = "Китай",
                BirthDate = new DateTime(1997, 4, 7),
                Team = "Xtreme Gaming",
                Role = "Керри",
                TotalEarnings = 4500000m,
                Achievements = "Финалист TI8, TI9, TI10; чемпион мейджоров"
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Nickname = "Yatoro",
                RealName = "Ilya Muliarchuk",
                Country = "Украина",
                BirthDate = new DateTime(2003, 3, 12),
                Team = "Team Spirit",
                Role = "Керри",
                TotalEarnings = 6090000m,
                Achievements = "Двукратный чемпион The International (TI10, TI12), победитель Riyadh Masters 2025"
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Nickname = "Satanic",
                RealName = "Alan Gallyamov",
                Country = "Россия",
                BirthDate = new DateTime(2007, 10, 13),
                Team = "PARIVISION",
                Role = "Керри",
                TotalEarnings = 423000m,
                Achievements = "Победитель ESL One Raleigh 2025, DreamLeague Season 26"
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Nickname = "Nightfall",
                RealName = "Egor Grigorenko",
                Country = "Россия",
                BirthDate = new DateTime(2002, 5, 16),
                Team = "Aurora Gaming",
                Role = "Керри",
                TotalEarnings = 1400000m,
                Achievements = "Победитель множества онлайн-турниров, топ-рейтинг Liquipedia"
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
            if (index < 0) return -1;

            _cards[index] = card.Clone();
            return card.Id;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index < 0) return false;
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