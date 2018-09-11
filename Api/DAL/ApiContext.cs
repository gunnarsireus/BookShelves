using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.DAL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options)
            : base(options)
        {
        }

	    public DbSet<Book> Books { get; set; }

	    public DbSet<Shelf> Shelves { get; set; }
	}
}