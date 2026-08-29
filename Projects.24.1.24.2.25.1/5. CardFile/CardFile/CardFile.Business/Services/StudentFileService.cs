using CardFile.Business.Models;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace CardFile.Business.Services
{
    public class StudentFileService
    {
        private StudentCollection _collection = new StudentCollection();
        private FileDataService _fileDataService = new FileDataService();

        public IEnumerable<Student> GetAll()
        {
            return _collection.GetAll().Select(dto => new Student
            {
                Id = dto.Id,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                BirthDate = dto.BirthDate,
                Faculty = dto.Faculty,
                Course = dto.Course,
                Group = dto.Group,
                RecordBookNumber = dto.RecordBookNumber,
                AverageGrade = dto.AverageGrade,
                AdmissionDate = dto.AdmissionDate,
                DismissalDate = dto.DismissalDate
            }).ToList();
        }

        public int Save(Student student)
        {
            var dto = new StudentDto
            {
                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                BirthDate = student.BirthDate,
                Faculty = student.Faculty,
                Course = student.Course,
                Group = student.Group,
                RecordBookNumber = student.RecordBookNumber,
                AverageGrade = student.AverageGrade,
                AdmissionDate = student.AdmissionDate,
                DismissalDate = student.DismissalDate
            };
            return _collection.Save(dto);
        }

        public bool Delete(int id) => _collection.Delete(id);

        public void SaveToFile(string fileName)
        {
            _fileDataService.SaveToFile(fileName, _collection);
        }

        public void OpenFromFile(string fileName)
        {
            _fileDataService.OpenFromFile(fileName, _collection);
        }
    }
}