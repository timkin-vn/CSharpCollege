using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }

        // Было: FirstName -> Стало: Title (Название)
        public string FirstName { get; set; }

        // Было: LastName -> Стало: Author (Автор)
        public string LastName { get; set; }

        // Было: Department -> Стало: Genre (Жанр)
        public string Department { get; set; }

        // Было: Position -> Стало: Publisher (Издательство)
        public string Position { get; set; }

        // Было: Salary -> Стало: Price (Цена)
        public decimal Salary { get; set; }

        // Оставляем дату для "Даты выхода", чтобы не менять логику в других файлах
        public DateTime BirthDate { get; set; }

        // Эти поля можно оставить пустыми или скрыть в интерфейсе
        public DateTime EmploymentDate { get; set; }
        public DateTime? DismissalDate { get; set; }
    }
}
