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
                Title="Таджикский психопат",
                DateRelease = new DateTime(2001, 4, 14),
                Director="Рашид Джамшутович",
                FilmReuge="Триллер",
                Price=300,
                Count_actor=10
               /* FirstName = "Андрей",
                MiddleName = "Геннадьевич",
                LastName = "Захаров",
                BirthDate = new DateTime(1985, 11, 7),
                Department = "Отдел разработки",
                Position = "Руководитель проекта",
                SubordinatesCount = 8,
                PaymentAmount = 250000m,*/
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "Достучаться до соседа",
                DateRelease = new DateTime(1997, 4, 14),
                Director = "Рашид Джамшутович",
                FilmReuge = "Драма",
                Price = 200,
                Count_actor = 50
                /* FirstName = "Андрей",
                 MiddleName = "Геннадьевич",
                 LastName = "Захаров",
                 BirthDate = new DateTime(1985, 11, 7),
                 Department = "Отдел разработки",
                 Position = "Руководитель проекта",
                 SubordinatesCount = 8,
                 PaymentAmount = 250000m,*/
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "Печальная буква хозяина",
                DateRelease = new DateTime(2005, 12, 17),
                Director = "Рашид Джамшутович",
                FilmReuge = "Триллер",
                Price = 100,
                Count_actor = 3
                /* FirstName = "Андрей",
                 MiddleName = "Геннадьевич",
                 LastName = "Захаров",
                 BirthDate = new DateTime(1985, 11, 7),
                 Department = "Отдел разработки",
                 Position = "Руководитель проекта",
                 SubordinatesCount = 8,
                 PaymentAmount = 250000m,*/
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Title = "Достучаться до соседа",
                DateRelease = new DateTime(1997, 4, 14),
                Director = "Рашид Джамшутович",
                FilmReuge = "Драма",
                Price = 200,
                Count_actor = 50
                /* FirstName = "Андрей",
                 MiddleName = "Геннадьевич",
                 LastName = "Захаров",
                 BirthDate = new DateTime(1985, 11, 7),
                 Department = "Отдел разработки",
                 Position = "Руководитель проекта",
                 SubordinatesCount = 8,
                 PaymentAmount = 250000m,*/
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
