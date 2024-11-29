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

        private int _nextId = 4;

        public CardFileDataCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                Name = "Сбер",
                Description = "Банковское отделение",
                Street = "Березовская улица",
                House = "95",
                City = "Нижний Новгород",
                MailIndex = 603157,
                Rating = 4.5,
                CounterReviews = 139,
                Status = "Закрыт по причине ремонта"
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Name = "Птичье молоко",
                Description = "Кафе, ресторан, банкетный зал",
                Street = "Юбилейный бульвар",
                House = "30Б",
                City = "Нижний Новгород",
                MailIndex = 603037,
                Rating = 4.3,
                CounterReviews = 177,
                Status = "Работает"
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Name = "Максавит",
                Description = "Аптека",
                Street = "улица Культуры",
                House = "14",
                City = "Нижний Новгород",
                MailIndex = 123456,
                Rating = 4.7,
                CounterReviews = 53,
                Status = "Работает"
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
