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
                FirstName = "Андрей",
                MiddleName = "Александрович",
                LastName = "Захаров",
                BirthDate = new DateTime(1984, 11, 15),
                Department = "Отдел разработки",
                Position = "Руководитель проектов",
                EmploymentDate = new DateTime(2012, 3, 1),
                DismissalDate = null,
                Salary = 125000m,
            },
            new CardDto
            {
                Id = 2,
                FirstName = "Людмила",
                MiddleName = "Викторовна",
                LastName = "Фролова",
                BirthDate = new DateTime(1995, 7, 18),
                Department = "Отдел анализа",
                Position = "Аналитик",
                EmploymentDate = new DateTime(2021, 3, 1),
                DismissalDate = new DateTime(2025, 4, 15),
                Salary = 100000m,
            },
            new CardDto
            {
                Id = 3,
                FirstName = "Геннадий",
                MiddleName = "Андреевич",
                LastName = "Василенко",
                BirthDate = new DateTime(1979, 3, 21),
                Department = "Отдел разработки",
                Position = "Старший разработчик",
                EmploymentDate = new DateTime(2018, 6, 1),
                DismissalDate = new DateTime(2024, 7, 1),
                Salary = 150000m,
            },
            new CardDto
            {
                Id = 4,
                FirstName = "Ирина",
                MiddleName = "Васильевна",
                LastName = "Мельникова",
                BirthDate = new DateTime(1989, 5, 8),
                Department = "Бухгалтерия",
                Position = "Главный бухгалтер",
                EmploymentDate = new DateTime(2016, 10, 5),
                DismissalDate = null,
                Salary = 150000m,
            },
        };

        internal int CurrentId = 5;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            //var result = new List<CardDto>();

            //foreach (var card in _cards)
            //{
            //    result.Add(card.Clone());
            //}

            //return result;

            // LINQ = Language integrated query
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
