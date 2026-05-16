using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.DataStore.DataCollection
{
    public class StudentCollection
    {
        private List<StudentDto> _students = new List<StudentDto>();
        private int _nextId = 1;

        // КОНСТРУКТОР - добавляем тестовых студентов
        public StudentCollection()
        {
            // Студент 1: Иванов
            var student1 = new StudentDto
            {
                Id = _nextId++,
                LastName = "Иванов",
                FirstName = "Иван",
                MiddleName = "Иванович",
                BirthDate = new DateTime(2000, 5, 15),
                Faculty = "Информационных технологий",
                Course = 3,
                Group = "ИТ-31",
                RecordBookNumber = "123456",
                AverageGrade = 4.5,
                AdmissionDate = new DateTime(2021, 9, 1),
                DismissalDate = null
            };
            _students.Add(student1);

            // Студент 2: Петрова
            var student2 = new StudentDto
            {
                Id = _nextId++,
                LastName = "Петрова",
                FirstName = "Екатерина",
                MiddleName = "Алексеевна",
                BirthDate = new DateTime(2001, 8, 23),
                Faculty = "Экономический",
                Course = 4,
                Group = "ЭК-42",
                RecordBookNumber = "234567",
                AverageGrade = 4.8,
                AdmissionDate = new DateTime(2020, 9, 1),
                DismissalDate = null
            };
            _students.Add(student2);

            // Студент 3: Сидоров (отчисленный)
            var student3 = new StudentDto
            {
                Id = _nextId++,
                LastName = "Сидоров",
                FirstName = "Алексей",
                MiddleName = "Дмитриевич",
                BirthDate = new DateTime(1999, 12, 10),
                Faculty = "Физико-математический",
                Course = 2,
                Group = "ФМ-22",
                RecordBookNumber = "345678",
                AverageGrade = 3.2,
                AdmissionDate = new DateTime(2022, 9, 1),
                DismissalDate = new DateTime(2023, 6, 30)
            };
            _students.Add(student3);
        }

        public IEnumerable<StudentDto> GetAll()
        {
            return _students.ToList();
        }

        public int Save(StudentDto student)
        {
            if (student.Id == 0)
            {
                student.Id = _nextId++;
                _students.Add(student);
                return student.Id;
            }
            else
            {
                var index = _students.FindIndex(s => s.Id == student.Id);
                if (index >= 0)
                {
                    _students[index] = student;
                }
                return student.Id;
            }
        }

        public bool Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return student != null && _students.Remove(student);
        }

        public void ReplaceAll(IEnumerable<StudentDto> students)
        {
            _students.Clear();
            _students.AddRange(students);
            if (students.Any())
            {
                _nextId = students.Max(s => s.Id) + 1;
            }
        }
    }
}