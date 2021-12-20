using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClassLib;
using BookClassLib.Models;

namespace BookProjectTest
{
    class BookReposFake : Ibooks
    {
        private readonly List<Book> _BookList;

        public BookReposFake()
        {
            _BookList = new List<Book>()
            {
                new Book(){BookNo=1,Author="venkat",Name="somerandom",Publisher="New"},
                new Book(){BookNo=2,Author="venkatesh",Name="somerandoms",Publisher="New1"},
                new Book(){BookNo=3,Author="Koushik",Name="somerandossm",Publisher="Ne2w"}


            };
        }
        public void Delete(int id)
        {
            Book bb= _BookList.Find(a => a.BookNo == id);
            _BookList.Remove(bb);
        }

        public List<Book> Get()
        {
            return _BookList;
        }

        public Book GetByID(int id)
        {
            return _BookList.Find(a => a.BookNo == id);
        }

        public Book post(Book book)
        {
            _BookList.Add(book);
            return  book; 
        }

        public void put(Book model)
        {
            var existingStudent = _BookList.Where(s => s.BookNo == model.BookNo)
                                                  .FirstOrDefault<Book>();
                existingStudent.Author = model.Author;
                existingStudent.Name = model.Name;
            
        }
    }
}
