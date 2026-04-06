using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId { get; set; } = 5;

        public CardCollection()
        {
            // Тестовые данные для медиа-картотеки
            _cards.Add(new CardDto
            {
                Id = 1,
                Title = "Ведьмак 3: Дикая Охота",
                Genre = "RPG",
                GlobalRating = 9.8,
                MyScore = 10,
                ReleaseDate = new DateTime(2015, 5, 19),
                Description = "Легендарное приключение Геральта из Ривии."
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "Интерстеллар",
                Genre = "Научная фантастика",
                GlobalRating = 8.7,
                MyScore = 9,
                ReleaseDate = new DateTime(2014, 11, 6),
                Description = "Фильм Кристофера Нолана о путешествиях сквозь пространство и время."
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "The Last of Us Part II",
                Genre = "Action-adventure",
                GlobalRating = 9.3,
                MyScore = 8,
                ReleaseDate = new DateTime(2020, 6, 19),
                Description = "Драматическая история мести в постапокалиптическом мире."
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Title = "Начало",
                Genre = "Триллер",
                GlobalRating = 8.8,
                MyScore = 10,
                ReleaseDate = new DateTime(2010, 7, 8),
                Description = "Проникновение в чужие сны."
            });

            NextId = 5;

            MapperInitialization.PreRegister();
            }
            public IEnumerable<CardDto> GetAll()
            {
                // Возвращаем клоны объектов, чтобы защитить основную коллекцию
                return _cards.Select(c => c.Clone()).ToList();
            }

            public int Save(CardDto card)
            {
                if (card.Id == 0) // Если Id равен 0, значит это новая карточка
                {
                    var newCard = card.Clone();
                    var id = NextId++;
                    newCard.Id = id;
                    _cards.Add(newCard);
                    return id;
                }

                // Если Id не 0, ищем существующую запись для обновления
                var index = _cards.FindIndex(c => c.Id == card.Id);
                if (index < 0) return -1;

                _cards[index] = card.Clone();
                return card.Id;
            }

            public bool Delete(int cardId)
            {
                var index = _cards.FindIndex(c => c.Id == cardId);
                if (index < 0) return false;

                _cards.RemoveAt(index);
                return true;
            }
            internal void ReplaceAll(IEnumerable<CardDto> newCollection, int nextId)
            {
                _cards.Clear();
                _cards.AddRange(newCollection);
                NextId = nextId;
            }
    }
}
