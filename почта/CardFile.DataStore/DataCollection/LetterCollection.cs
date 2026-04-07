using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataStore.DataCollection
{
    public class LetterCollection
    {
        private readonly List<LetterDto> _letters = new List<LetterDto>();
        internal int NextId { get; set; } = 4;

        public LetterCollection()
        {
            _letters.Add(new LetterDto { Id = 1, Sender = "admin@company.com", Receiver = "user@company.com", Subject = "Добро пожаловать", Body = "Ваш аккаунт успешно создан.", Date = new DateTime(2025, 1, 15), IsRead = true });
            _letters.Add(new LetterDto { Id = 2, Sender = "hr@company.com", Receiver = "user@company.com", Subject = "График отпусков", Body = "Просьба ознакомиться с новым графиком.", Date = new DateTime(2025, 2, 10), IsRead = false });
            _letters.Add(new LetterDto { Id = 3, Sender = "support@company.com", Receiver = "user@company.com", Subject = "Обновление системы", Body = "Плановые работы пройдут в выходные.", Date = new DateTime(2025, 3, 5), IsRead = true });
            MapperInitialization.PreRegister();
        }

        public IEnumerable<LetterDto> GetAll() => _letters.Select(c => c.Clone()).ToList();
        public int Save(LetterDto letter)
        {
            if (letter.Id == 0)
            {
                var newLetter = letter.Clone();
                var id = NextId++;
                newLetter.Id = id;
                _letters.Add(newLetter);
                return id;
            }
            var index = _letters.FindIndex(c => c.Id == letter.Id);
            if (index < 0) return -1;
            _letters[index] = letter.Clone();
            return letter.Id;
        }
        public bool Delete(int letterId)
        {
            var index = _letters.FindIndex(c => c.Id == letterId);
            if (index < 0) return false;
            _letters.RemoveAt(index);
            return true;
        }
        internal void ReplaceAll(IEnumerable<LetterDto> newCollection) => ReplaceAll(newCollection, 0);
        internal void ReplaceAll(IEnumerable<LetterDto> newCollection, int nextId)
        {
            _letters.Clear();
            _letters.AddRange(newCollection);
            NextId = nextId == 0 ? (_letters.Any() ? _letters.Max(c => c.Id) + 1 : 1) : nextId;
        }
    }
}