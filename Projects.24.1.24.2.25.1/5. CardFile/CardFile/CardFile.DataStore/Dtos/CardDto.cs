using CardFile.Common.Infrastructure;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object

        public int Id { get; set; }

        public string Hero { get; set; }

        public string Slot1 { get; set; }

        public string Slot2 { get; set; }

        public string Slot3 { get; set; }

        public string Slot4 { get; set; }

        public string Slot5 { get; set; }

        public string Slot6 { get; set; }

        public string Neutral { get; set; }

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
            //return new CardDto
            //{
            //    Id = Id,
            //    FirstName = FirstName,
            //    MiddleName = MiddleName,
            //    LastName = LastName,
            //    BirthDate = BirthDate,
            //    Department = Department,
            //    Position = Position,
            //    EmploymentDate = EmploymentDate,
            //    DismissalDate = DismissalDate,
            //    Salary = Salary,
            //};
        }
    }
}
