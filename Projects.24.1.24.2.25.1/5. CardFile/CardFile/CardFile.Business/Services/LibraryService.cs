using CardFile.Business.Infrastructure;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Services
{
    public class LibraryService
    {
        private readonly BookCollection _collection = new BookCollection();

        public LibraryService()
        {
            MapperInitialization.PreRegister();
        }

        public IEnumerable<Book> GetAll()
        {
            var books = _collection.GetAll();
            return books.Select(FromDto).ToList();
        }

        public IEnumerable<Book> GetDeleted()
        {
            var books = _collection.GetDeleted();
            return books.Select(FromDto).ToList();
        }

        public int Save(Book book)
        {
            return _collection.Save(ToDto(book));
        }

        public bool Delete(int bookId)
        {
            return _collection.Delete(bookId);
        }

        public bool Restore(int bookId)
        {
            return _collection.Restore(bookId);
        }

        public IEnumerable<Book> Search(string text)
        {
            var books = _collection.Search(text);
            return books.Select(FromDto).ToList();
        }

        public bool Issue(int bookId)
        {
            return _collection.Issue(bookId);
        }

        public bool Return(int bookId)
        {
            return _collection.Return(bookId);
        }

        public void SaveToFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.SaveToFile(fileName, _collection);
        }

        public void OpenFromFile(string fileName)
        {
            var fileDataService = new FileDataService();
            fileDataService.OpenFromFile(fileName, _collection);
        }

        private Book FromDto(BookDto book)
        {
            return Mapping.Mapper.Map<Book>(book);
        }

        private BookDto ToDto(Book book)
        {
            return Mapping.Mapper.Map<BookDto>(book);
        }
    }
}
