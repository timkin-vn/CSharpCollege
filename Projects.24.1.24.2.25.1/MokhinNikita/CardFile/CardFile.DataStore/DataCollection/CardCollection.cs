using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();
        internal int NextId { get; set; } = 3;
        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                FirstName = "Никита",
                MiddleName = "Денисович",
                LastName = "Мохин",
                BirthDate = new DateTime(2007, 10, 26),
                Department = "Разработчик",
                Position = "Участник",
                EmploymentDate = new DateTime(2020, 4, 18),
                DismissalDate = null,
                Salary = 10000m
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                FirstName = "Марина",
                MiddleName = "Александровна",
                LastName = "Васильева",
                BirthDate = new DateTime(1989, 11, 25),
                Department = "Разработчик",
                Position = "Ментор",
                EmploymentDate = new DateTime(2016, 4, 18),
                DismissalDate = null,
                Salary = 205000m
            });
            MapperInitialization.Preregister();
        }
        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone()).ToList();
        }
        public int Save(CardDto card)
        {
            if(card.Id == 0)
            {
                var newCard = card.Clone();
                var id = NextId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }
            var index = _cards.FindIndex(c => c.Id == card.Id);
            if(index == -1)
            {
                return -1;
            }
            _cards[index] = card.Clone();
            return card.Id;
        }
        public bool Delete(int CardId)
        {
            var index = _cards.FindIndex(_ => _.Id == CardId);
            if (index == -1)
            {
                return false;
            }
            _cards.RemoveAt(index);
            return true;
        }
        internal void ReplaceAll(IEnumerable<CardDto> cards)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = _cards.Max(c => c.Id) + 1;
        }
        internal void ReplaceAll(IEnumerable<CardDto> cards, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = nextId;
        }
    }
}
