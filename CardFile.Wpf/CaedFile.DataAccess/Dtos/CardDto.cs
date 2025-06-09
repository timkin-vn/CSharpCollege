using CardFile.Common.Infrastructure;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public int BonusPoints { get; set; }
        public string CardType { get; set; }

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }

        public void Update(CardDto newCard)
        {
            Mapping.Mapper.Map(newCard, this);
        }
    }
}