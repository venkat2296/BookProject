using BookClassLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookClassLib
{
      public interface Ibooks
    {
        List<Book> Get();
        Book GetByID(int id);
        Book post(Book book);
        void put(Book book);
        void Delete(int id);
    }
}
