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
                FirstName = "Иван",
                MiddleName = "Петрович",
                LastName = "Смирнов",
                BirthDate = new DateTime(1980, 5, 12),
                Gender = "Мужской",
                Address = "ул. Ленина, д.1",
                Diagnosis = "ОРВИ",
                LastVisitDate = new DateTime(2025, 5, 25),
                PhoneNumber = "+7 (900) 111-22-33"
            },
            new CardDto
            {
                Id = 2,
                FirstName = "Елена",
                MiddleName = "Алексеевна",
                LastName = "Кузнецова",
                BirthDate = new DateTime(1993, 2, 20),
                Gender = "Женский",
                Address = "ул. Советская, д.15",
                Diagnosis = "Мигрень",
                LastVisitDate = null,
                PhoneNumber = "+7 (900) 222-33-44"
            },
            new CardDto
            {
                Id = 3,
                FirstName = "Артём",
                MiddleName = "Игоревич",
                LastName = "Волков",
                BirthDate = new DateTime(2001, 11, 3),
                Gender = "Мужской",
                Address = "пр-т Мира, д.78",
                Diagnosis = "Гастрит",
                LastVisitDate = new DateTime(2025, 6, 1),
                PhoneNumber = "+7 (900) 333-44-55"
            },
        };

        internal int CurrentId = 4;

        public CardCollection()
        {
            MapperRegistrator.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
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