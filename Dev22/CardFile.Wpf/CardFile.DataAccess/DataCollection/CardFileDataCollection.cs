using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.DataCollection
{
    public class CardFileDataCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        private int _nextId = 5;

        public CardFileDataCollection() 
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
                SubordinatesCount = 8,
                PaymentAmount = 250000m,
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
                SubordinatesCount = 4,
                PaymentAmount = 200000m,
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
                SubordinatesCount = 2,
                PaymentAmount = 300000m,
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
                SubordinatesCount = 3,
                PaymentAmount = 150000m,
            });
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone()).ToList();
        }

        public CardDto Get(int id)
        {
            return _cards.FirstOrDefault(c => c.Id == id)?.Clone();
        }

        public bool Update(CardDto dto)
        {
            var existingRecord = _cards.FirstOrDefault(c => c.Id == dto.Id);
            if (existingRecord == null)
            {
                return false;
            }

            existingRecord.Update(dto);
            return true;
        }

        public int Insert(CardDto dto)
        {
            if (dto.Id > 0)
            {
                return -1;
            }

            var newRecord = dto.Clone();
            newRecord.Id = _nextId++;
            _cards.Add(newRecord);
            return newRecord.Id;
        }

        public int Save(CardDto dto)
        {
            if (dto.Id == 0)
            {
                return Insert(dto);
            }

            if (Update(dto))
            {
                return dto.Id;
            }

            return -1;
        }

        public bool Delete(int id)
        {
            var existingRecord = _cards.FirstOrDefault(c => c.Id == id);
            if (existingRecord == null)
            {
                return false;
            }

            _cards.Remove(existingRecord);
            return true;
        }
    }
}
