using System;
using System.Collections.Generic;
using System.Text;
using BookClassLib;
using BookClassLib.Models;
using Microsoft.EntityFrameworkCore;

namespace BookProjectTest
{
     public class BookTestBase : IDisposable
    {
        public readonly BookDatabaseContext context;

        public BookTestBase()
        {
            var options = new DbContextOptionsBuilder<BookDatabaseContext>().UseInMemoryDatabase(databaseName: new Guid().ToString()).Options;
            context = new BookDatabaseContext(options);
            context.Database.EnsureCreated();
            BookInitializer.Initialize(context);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
