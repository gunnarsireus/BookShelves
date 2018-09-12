using Api.Models;
using System;
using System.Linq;

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
                    InShelf = true,
                    Disabled = false,
                    Title = "The Lord of the Rings",
                    Author = "J.R.R Rolkien",
                    ISBN = "978-0544003415",
                    LoanedTo = ""
                });
                context.Books.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    ShelfId = shelfId,
                    InShelf = true,
                    Disabled = false,
                    Title = "Game of thrones",
                    Author = "George R. R. Martin",
                    ISBN = "978-9175030517",
                    LoanedTo = ""
                });
                context.Books.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    ShelfId = shelfId,
                    InShelf = true,
                    Disabled = false,
                    Title = "The Chronicles of Narnia",
                    Author = "C. S. Lewis",
                    ISBN = "978-0007640218",
                    LoanedTo = ""
                });

                shelfId = Guid.NewGuid();
                context.Shelves.Add(new Shelf() { Id = shelfId, Genre = "Adventure", Location = "Room 2" });
                context.Books.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    ShelfId = shelfId,
                    InShelf = true,
                    Disabled = false,
                    Title = "Hunger Games",
                    Author = "Suzanne Collins",
                    ISBN = "978-0439023481",
                    LoanedTo = ""
                });
                context.Books.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    ShelfId = shelfId,
                    InShelf = true,
                    Disabled = false,
                    Title = "Treasure Island",
                    Author = "Robert Louis Stevenson",
                    ISBN = "978-1505297409",
                    LoanedTo = ""
                });

                shelfId = Guid.NewGuid();
                context.Shelves.Add(new Shelf() { Id = shelfId, Genre = "Science Fiction", Location = "Room 3" });
                context.Books.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    ShelfId = shelfId,
                    InShelf = true,
                    Disabled = false,
                    Title = "1984",
                    Author = "Georg Orwell",
                    ISBN = "978-0451524935",
                    LoanedTo = ""
                });
                context.Books.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    ShelfId = shelfId,
                    InShelf = true,
                    Disabled = false,
                    Title = "Fahrenheit 451",
                    Author = "Ray Bradbury",
                    ISBN = "978-1451673319",
                    LoanedTo = ""
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
