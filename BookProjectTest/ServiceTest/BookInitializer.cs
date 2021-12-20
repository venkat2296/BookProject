using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookClassLib;
using BookClassLib.Models;

namespace BookProjectTest
{
    class BookInitializer
    {
       

        public static void Initialize(BookDatabaseContext context)
        {
            if(context.Book.Any())
            {
                return;
            }
            Seed(context);


        }

        private static void Seed(BookDatabaseContext context)
        {
             var _BookList = new List<Book>()
            {
                new Book(){BookNo=1,Author="venkat",Name="somerandom",Publisher="New"},
                new Book(){BookNo=2,Author="venkatesh",Name="somerandoms",Publisher="New1"},
                new Book(){BookNo=3,Author="Koushik",Name="somerandossm",Publisher="Ne2w"}


            };

            context.Book.AddRange(_BookList);
            context.SaveChanges();
        }
             


    }
}
