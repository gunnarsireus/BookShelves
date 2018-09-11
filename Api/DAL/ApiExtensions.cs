using System;
using System.Linq;
using Api.Models;

namespace Api.DAL
{
	public static class ApiExtensions
	{
		public static void EnsureSeedData(this ApiContext context)
		{
			if (!context.Books.Any() || !context.Shelves.Any())
			{
				var shelfId = Guid.NewGuid();
				context.Shelves.Add(new Shelf() { Id = shelfId, Genre = "Fantasy", Location = "Room 1" });
				context.Books.Add(new Book
				{
					Id = Guid.NewGuid(),
					ShelfId = shelfId,
					Title = "The Lord of the Rings",
                    Author= "J.R.R Rolkien",
					ISBN = "978-0544003415"
                });
				context.Books.Add(new Book
				{
					Id = Guid.NewGuid(),
					ShelfId = shelfId,
					Title = "Game of thrones",
                    Author = "George R. R. Martin",
                    ISBN = "978-9175030517"
                });
				context.Books.Add(new Book
				{
					Id = Guid.NewGuid(),
					ShelfId = shelfId,
					Title = "The Chronicles of Narnia",
                    Author="C. S. Lewis",
					ISBN = "978-0007640218"
                });

				shelfId = Guid.NewGuid();
				context.Shelves.Add(new Shelf() { Id = shelfId, Genre = "Adventure", Location = "Room 2" });
				context.Books.Add(new Book
				{
					Id = Guid.NewGuid(),
					ShelfId = shelfId,
					Title = "Hunger Games",
                    Author = "Suzanne Collins",
					ISBN = "978-0439023481"
                });
				context.Books.Add(new Book
				{
					Id = Guid.NewGuid(),
					ShelfId = shelfId,
					Title = "Treasure Island",
                    Author= "Robert Louis Stevenson",
                    ISBN = "978-1505297409"
                });

				shelfId = Guid.NewGuid();
				context.Shelves.Add(new Shelf() { Id = shelfId, Genre = "Science Fiction", Location = "Room 3" });
				context.Books.Add(new Book
				{
					Id = Guid.NewGuid(),
					ShelfId = shelfId,
					Title = "1984",
                    Author = "Georg Orwell",
					ISBN = "978-0451524935"
                });
				context.Books.Add(new Book
				{
					Id = Guid.NewGuid(),
					ShelfId = shelfId,
					Title = "Fahrenheit 451",
                    Author = "Ray Bradbury",
					ISBN = "978-1451673319"
                });
			}
            else
            {
                foreach (var book in context.Books)
                {
                    book.Disabled = false;
                }
            }
            context.SaveChanges();
        }
	}
}
