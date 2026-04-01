using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.DataCollection
{
    public class BookCollection
    {
        private readonly List<BookDto> _books = new List<BookDto>();

        internal int NextId { get; set; } = 5;

        public BookCollection()
        {
            _books.Add(new BookDto
            {
                Id = 1,
                Title = "Война и мир",
                Author = "Лев Толстой",
                Genre = "Роман",
                Year = 1869,
                Copies = 10,
                AddedDate = new DateTime(2020, 1, 15),
                DeletedDate = null,
            });
            _books.Add(new BookDto
            {
                Id = 2,
                Title = "Преступление и наказание",
                Author = "Фёдор Достоевский",
                Genre = "Роман",
                Year = 1866,
                Copies = 8,
                AddedDate = new DateTime(2020, 2, 20),
                DeletedDate = null,
            });
            _books.Add(new BookDto
            {
                Id = 3,
                Title = "Мастер и Маргарита",
                Author = "Михаил Булгаков",
                Genre = "Роман",
                Year = 1967,
                Copies = 12,
                AddedDate = new DateTime(2021, 3, 10),
                DeletedDate = null,
            });
            _books.Add(new BookDto
            {
                Id = 4,
                Title = "1984",
                Author = "Джордж Оруэлл",
                Genre = "Антиутопия",
                Year = 1949,
                Copies = 15,
                AddedDate = new DateTime(2021, 5, 5),
                DeletedDate = null,
            });

            MapperInitialization.PreRegister();
        }

        public IEnumerable<BookDto> GetAll()
        {
            return _books.Where(b => b.DeletedDate == null).Select(b => b.Clone()).ToList();
        }

        public IEnumerable<BookDto> GetDeleted()
        {
            return _books.Where(b => b.DeletedDate != null).Select(b => b.Clone()).ToList();
        }

        public int Save(BookDto book)
        {
            if (book.Id == 0)
            {
                var newBook = book.Clone();
                var id = NextId++;
                newBook.Id = id;
                _books.Add(newBook);
                return id;
            }

            var index = _books.FindIndex(b => b.Id == book.Id);
            if (index < 0)
            {
                return -1;
            }

            _books[index] = book.Clone();
            return book.Id;
        }

        public bool Delete(int bookId)
        {
            var index = _books.FindIndex(b => b.Id == bookId);
            if (index < 0)
            {
                return false;
            }

            _books[index].DeletedDate = DateTime.Now;
            return true;
        }

        public bool Restore(int bookId)
        {
            var index = _books.FindIndex(b => b.Id == bookId && b.DeletedDate != null);
            if (index < 0)
            {
                return false;
            }

            _books[index].DeletedDate = null;
            return true;
        }

        public IEnumerable<BookDto> Search(string text)
        {
            return _books
                .Where(b => b.DeletedDate == null)
                .Where(b => b.Title.ToLower().Contains(text.ToLower()) ||
                            b.Author.ToLower().Contains(text.ToLower()))
                .Select(b => b.Clone())
                .ToList();
        }

        public bool Issue(int bookId)
        {
            var index = _books.FindIndex(b => b.Id == bookId && b.DeletedDate == null);
            if (index < 0 || _books[index].Copies <= 0)
            {
                return false;
            }

            _books[index].Copies--;
            return true;
        }

        public bool Return(int bookId)
        {
            var index = _books.FindIndex(b => b.Id == bookId && b.DeletedDate == null);
            if (index < 0)
            {
                return false;
            }

            _books[index].Copies++;
            return true;
        }

        internal void ReplaceAll(IEnumerable<BookDto> newCollection)
        {
            _books.Clear();
            _books.AddRange(newCollection);
            NextId = _books.Max(b => b.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<BookDto> newCollection, int nextId)
        {
            _books.Clear();
            _books.AddRange(newCollection);
            NextId = nextId;
        }
    }
}
