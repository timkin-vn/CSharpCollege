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
                Title = "Макароны Спагетти Makfa",
                EXP = new DateTime(2026, 11, 7),
                Fabricator = "Makfa",
                Section = "Бакалея",
                Count = 8,
                Price = 79m,
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "Батон Сормовский в нарезке",
                EXP = new DateTime(2024, 12, 15),
                Fabricator = "Сормовский хлеб",
                Section = "Хлеб и выпечка",
                Count = 15,
                Price = 30m,
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "Молоко 2,5% пастеризованное",
                EXP = new DateTime(2024, 12, 19),
                Fabricator = "Княгинино",
                Section = "Молочная продукция",
                Count = 6,
                Price = 52m,
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Title = "Пирожное песочное глазированное",
                EXP = new DateTime(2024, 12, 29),
                Fabricator = "Сормовский хлеб",
                Section = "Хлеб и выпечка",
                Count = 5,
                Price = 37m,
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
