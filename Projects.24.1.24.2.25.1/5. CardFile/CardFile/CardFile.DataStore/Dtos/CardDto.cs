using CardFile.Common.Infrastructure;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public int FoundationYear { get; set; }

        public decimal Revenue { get; set; }

        public int Employees { get; set; }

        public string Description { get; set; }

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}