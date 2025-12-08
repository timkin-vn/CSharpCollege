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
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId = 5;

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                Title = "Тень Ветра",
                Author = "Карлос Руис Сафон",
                Year = 2001,
                Genre = "Готический детектив",
                Description = "Мальчик находит таинственную книгу, которая меняет всю его жизнь."
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "451° по Фаренгейту",
                Author = "Рэй Брэдбери",
                Year = 1953,
                Genre = "Антиутопия",
                Description = "Роман описывает американское общество близкого будущего, в котором книги находятся под запретом; «пожарные»[1], к числу которых принадлежит и главный герой Гай Монтэг, сжигают любые найденные книги. В ходе романа Монтэг разочаровывается в идеалах общества, частью которого он является, становится изгоем и присоединяется к небольшой подпольной группе маргиналов, сторонники которой заучивают тексты книг, чтобы спасти их для потомков. В книге содержится немало цитат из произведений англоязычных авторов прошлого (таких, как Уильям Шекспир, Джонатан Свифт и другие), а также несколько цитат из Библии."
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "Мастер и Маргарита",
                Author = "Михаил Булгаков",
                Year = 1967,
                Genre = "Мистика",
                Description = "Дьявол появляется в Москве и меняет судьбы многих людей."
            });

            InitializeMapper.Register();
        }

        public IEnumerable<CardDto> GetAll()
        {
            //var result = new List<CardDto>();

            //foreach (var card in _cards)
            //{
            //    result.Add(card.Clone());
            //}

            //return result;
            return _cards.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                var id = NextId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == card.Id);
            if (index < 0)
            {
                return -1;
            }

            _cards[index] = card.Clone();
            return card.Id;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index >= 0)
            {
                _cards.RemoveAt(index);
                return true;
            }

            return false;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = nextId;
        }
    }
}
