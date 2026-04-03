namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public System.DateTime BirthDate { get; set; }
        public System.DateTime EmploymentDate { get; set; }
        public System.DateTime? DismissalDate { get; set; }
        public decimal Salary { get; set; }

        public CardDto Clone()
        {
            return CardFile.Common.Infrastructure.Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
