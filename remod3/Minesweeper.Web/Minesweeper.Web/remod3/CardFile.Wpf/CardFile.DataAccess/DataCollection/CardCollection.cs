using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.Infrastructure;

namespace CardFile.DataAccess.DataCollection;
public class CardCollection {
    private List<CardDto> _cards = new List<CardDto> {
        new CardDto {
            Id = 1,
            Title = "Мастера меча онлайн",
            OriginalTitle = "Sword Art Online",
            Genre = "Драма • Приключения • Романтика • Сейнен • Фэнтези",
            ReleaseDate = new DateTime(year:2012, month:1, day:1),
            Studio = "Отдел разработки",
            Director = "Руководитель проектов",
            EndDate = null,
            Rating = 7.2,
        }
    };

    internal int CurrentId = 2;

    public CardCollection() {
        MapperRegistrator.Register();
    }

    public IEnumerable<CardDto> GetAll() {
        return _cards.Select(c => c.Clone()).ToList();
    }

    public int Save(CardDto cardDto) {
        if (cardDto.Id == 0) {
            var newCard = cardDto.Clone();
            var id = CurrentId++;
            newCard.Id = id;
            _cards.Add(newCard);
            return id;
        }

        var index = _cards.FindIndex(c => c.Id == cardDto.Id);
        if (index >= 0) {
            _cards[index] = cardDto.Clone();
            return cardDto.Id;
        }

        return -1;
    }

    public bool Delete(int cardId) {
        var index = _cards.FindIndex(c => c.Id == cardId);
        if (index < 0) {
            return false;
        }

        _cards.RemoveAt(index);
        return true;
    }

    internal void ReplaceAll(IEnumerable<CardDto> source) {
        _cards.Clear();
        _cards.AddRange(source);
        CurrentId = _cards.Max(c => c.Id) + 1;
    }

    internal void ReplaceAll(IEnumerable<CardDto> source, int currentId) {
        _cards.Clear();
        _cards.AddRange(source);
        CurrentId = currentId;
    }
}