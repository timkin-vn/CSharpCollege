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
            _cards.Add(new CardDto
            {
                Id = 1,
                FirstName = "Александр",
                MiddleName = "s1mple", //ник 
                LastName = "Костылев",
                BirthDate = new DateTime(1997, 10, 2),
                Department = "Natus Vincere", //команда 
                Position = "awper",              
                EmploymentDate = new DateTime(2016, 8, 4),//начало карьеры
                DismissalDate = null,            
                Salary = 1.37m, //рейтинг хлтьв
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                FirstName = "Николай",
                MiddleName = "device",
                LastName = "Ридтц",
                BirthDate = new DateTime(1995, 9, 8),
                Department = "Astralis",
                Position = "awper",
                EmploymentDate = new DateTime(2013, 5, 1),
                DismissalDate = new DateTime(2024, 3, 15),  
                Salary = 1.25m,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                FirstName = "Илья",
                MiddleName = "m0NESY",
                LastName = "Осипов",
                BirthDate = new DateTime(2005, 5, 1),
                Department = "G2 Esports",
                Position = "Awper",
                EmploymentDate = new DateTime(2022, 1, 3),
                DismissalDate = null,
                Salary = 1.41m,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                FirstName = "Денис",
                MiddleName = "electroNic",
                LastName = "Шарипов",
                BirthDate = new DateTime(1998, 9, 2),
                Department = "Virtus.pro",
                Position = "Igl",                
                EmploymentDate = new DateTime(2015, 10, 15),
                DismissalDate = null,
                Salary = 1.12m,
            });
            _cards.Add(new CardDto
            {
                Id = 5,
                FirstName = "Дмитрий",
                MiddleName = "sh1ro",
                LastName = "Соколов",
                BirthDate = new DateTime(2001, 6, 15),
                Department = "Cloud9",
                Position = "AWPer",
                EmploymentDate = new DateTime(2019, 8, 20),
                DismissalDate = null,
                Salary = 1.32m,
            });

            MapperInitialization.PreRegister();
        }

        public IEnumerable<CardDto> GetAll()
        {
            //return _cards;

            //var result = new List<CardDto>();

            //foreach (var card in _cards)
            //{
            //    result.Add(card.Clone());
            //}

            //return result;

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
