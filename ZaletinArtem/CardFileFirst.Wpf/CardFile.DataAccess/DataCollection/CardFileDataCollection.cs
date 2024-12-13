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
                Id = 2,
                Title = "Преступление и наказание",
                Author = "Фёдр Достоевский",
                PublicationDate = new DateTime(1866, 1, 1),
                Genre = "Роман",
                PageCount = 671,
                Price = 450m,
            });
            _cards.Add(new CardDto
            {
                Id = 1,
                Title = "Война и мир",
                Author = "Лев Толстой",
                PublicationDate = new DateTime(1869, 1, 1),
                Genre = "Роман",
                PageCount = 1225,
                Price = 500,
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