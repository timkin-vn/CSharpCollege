using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId { get; set; } = 6;

        public CardCollection()
        {
            _cards.Add(new CardDto
            {
                Id = 1,
                Name = "Лукойл",
                Industry = "Нефть и газ",
                FoundationYear = 1991,
                Revenue = 8600m,
                Employees = 100000,
                Description = "Одна из крупнейших российских нефтяных и газовых компаний, вторая по объёму выручки в России. Основная деятельность — разведка и добыча нефти и газа, производство нефтепродуктов.",
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Name = "Газпром",
                Industry = "Нефть и газ",
                FoundationYear = 1989,
                Revenue = 10800m,
                Employees = 490000,
                Description = "Крупнейшая газовая компания в мире, занимает около 12% мировой добычи газа. Владелец крупнейшей в мире газотранспортной системы.",
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Name = "Роснефть",
                Industry = "Нефть и газ",
                FoundationYear = 1993,
                Revenue = 9200m,
                Employees = 330000,
                Description = "Крупнейшая нефтяная компания России, входит в топ-5 мировых публичных нефтегазовых корпораций по объёму добычи. Контролируется государством.",
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Name = "Сбербанк",
                Industry = "Банковский сектор",
                FoundationYear = 1841,
                Revenue = 3800m,
                Employees = 280000,
                Description = "Крупнейший российский и восточноевропейский банк, основной кредитор национальной экономики. Предоставляет широкий спектр банковских и финансовых услуг.",
            });
            _cards.Add(new CardDto
            {
                Id = 5,
                Name = "Яндекс",
                Industry = "Информационные технологии",
                FoundationYear = 1997,
                Revenue = 950m,
                Employees = 25000,
                Description = "Одна из крупнейших IT-компаний России, оператор одноимённой поисковой системы. Развивает сервисы: поиск, карты, такси, доставку, маркетплейс, облачные технологии.",
            });

            MapperInitialization.PreRegister();
        }

        public IEnumerable<CardDto> GetAll()
        {
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
            if (index < 0)
            {
                return false;
            }

            _cards.RemoveAt(index);
            return true;
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