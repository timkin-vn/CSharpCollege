using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;
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

        internal int NextId = 5;

        public CardFileDataCollection() 
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                FirstName = "Андрей",
                MiddleName = "Геннадьевич",
                LastName = "Захаров",
                BirthDate = new DateTime(1985, 11, 7),
                DebtAmount = 10000,
                Position = "нет",
                SubordinatesCount = 800,
                PaymentAmount = 25000m,
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                FirstName = "Нина",
                MiddleName = "Аркадьевна",
                LastName = "Шевченко",
                BirthDate = new DateTime(1990, 8, 21),
                DebtAmount = 600000,
                Position = "да",
                SubordinatesCount = 500000,
                PaymentAmount = 20000m,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                FirstName = "Виктор",
                MiddleName = "Петрович",
                LastName = "Васильев",
                BirthDate = new DateTime(2001, 3, 11),
                DebtAmount = 4000,
                Position = "нет",
                SubordinatesCount = 2000,
                PaymentAmount = 30000m,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                FirstName = "Ольга",
                MiddleName = "Владимировна",
                LastName = "Меднис",
                BirthDate = new DateTime(1981, 9, 2),
                DebtAmount = 2300,
                Position = "нет",
                SubordinatesCount = 300,
                PaymentAmount = 15000m,
            });

            MapperRegistrator.Register();
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
            newRecord.Id = NextId++;
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

        internal void ReplaceAll(IEnumerable<CardDto> collection)
        {
            _cards.Clear();
            _cards.AddRange(collection.Select(c => c.Clone()));
            NextId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> collection, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(collection.Select(c => c.Clone()));
            NextId = nextId;
        }
    }
}
