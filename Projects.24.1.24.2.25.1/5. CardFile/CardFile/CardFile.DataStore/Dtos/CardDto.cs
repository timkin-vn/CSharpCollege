namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } // Название
        public string MiddleName { get; set; } // Доп. информация (серия) - ДОБАВИТЬ ЭТО
        public string LastName { get; set; }  // Автор
        public string Department { get; set; } // Жанр
        public string Position { get; set; }   // Издательство
        public System.DateTime BirthDate { get; set; }
        public System.DateTime EmploymentDate { get; set; }
        public System.DateTime? DismissalDate { get; set; }
        public decimal Salary { get; set; } // Цена

        public CardDto Clone()
        {
            return CardFile.Common.Infrastructure.Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
