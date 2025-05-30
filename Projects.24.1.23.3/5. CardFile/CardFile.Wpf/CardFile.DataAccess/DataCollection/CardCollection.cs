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
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId = 5;

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                FirstName = "Андрей",
                MiddleName = "Геннадьевич",
                LastName = "Захаров",
                BirthDate = new DateTime(1985, 11, 7),
                Department = "Отдел разработки",
                Position = "Руководитель проекта",
                EmploymentDate = new DateTime(2010, 4, 18),
                DismissalDate = null,
                Salary = 250000m,
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                FirstName = "Нина",
                MiddleName = "Аркадьевна",
                LastName = "Шевченко",
                BirthDate = new DateTime(1990, 8, 21),
                Department = "Отдел тестирования",
                Position = "Старший тестировщик",
                EmploymentDate = new DateTime(2015, 2, 14),
                DismissalDate = new DateTime(2024, 11, 3),
                Salary = 200000m,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                FirstName = "Виктор",
                MiddleName = "Петрович",
                LastName = "Васильев",
                BirthDate = new DateTime(2001, 3, 11),
                Department = "Отдел разработки",
                Position = "Ведущий разработчик",
                EmploymentDate = new DateTime(2020, 9, 25),
                DismissalDate = null,
                Salary = 300000m,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                FirstName = "Ольга",
                MiddleName = "Владимировна",
                LastName = "Меднис",
                BirthDate = new DateTime(1981, 9, 2),
                Department = "Бухгалтерия",
                Position = "Главный бухгалтер",
                EmploymentDate = new DateTime(2010, 9, 25),
                DismissalDate = new DateTime(2023, 3, 18),
                Salary = 150000m,
            });

            InitializeMapper.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
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
            if (index >= 0)
            {
                _cards.RemoveAt(index);
                return true;
            }

            return false;
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
