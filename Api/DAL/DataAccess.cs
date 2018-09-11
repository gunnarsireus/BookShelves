using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.DAL
{
    public class DataAccess
    {
        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder =
            new DbContextOptionsBuilder<ApiContext>();

        public DataAccess()
        {
            _optionsBuilder.UseSqlite("DataSource=App_Data/Books.db");
        }

	    public ICollection<Book> GetBooks()
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    return context.Books.ToList();
		    }
	    }

	    public Book GetBook(Guid id)
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    return context.Books.SingleOrDefault(o => o.Id == id);
		    }
	    }

	    public void AddBook(Book book)
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    context.Books.Add(book);
			    context.SaveChanges();
		    }
	    }

	    public void DeleteBook(Guid id)
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    var Book = GetBook(id);
			    context.Books.Remove(Book);
			    context.SaveChanges();
		    }
	    }

	    public void UpdateBook(Book book)
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    context.Books.Update(book);
			    context.SaveChanges();
		    }
	    }

		public ICollection<Shelf> GetShelves()
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                return context.Shelves.ToList();
            }
        }

        public Shelf GetShelf(Guid id)
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                return context.Shelves.SingleOrDefault(o => o.Id == id);
            }
        }

        public void AddShelf(Shelf shelf)
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                context.Shelves.Add(shelf);
                context.SaveChanges();
            }
        }

        public void DeleteShelf(Guid id)
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var books = GetBooks().Where(c => c.ShelfId == id);
                foreach (var book in books)
                {
                    context.Books.Remove(book);
                }

                var shelf = GetShelf(id);
                context.Shelves.Remove(shelf);
                context.SaveChanges();
            }
        }

        public void UpdateShelf(Shelf shelf)
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                context.Shelves.Update(shelf);
                context.SaveChanges();
            }
        }
    }
}