using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Api.Data;
using Api.DAL;

namespace Api.Data
{
    public class BookUnitOfWork:IBookUnitOfWork
    {
	    private readonly ApiContext _context;

	    public BookUnitOfWork(ApiContext context)
	    {
		    _context = context;
		    Books = new BookRepository(_context);
		    Shelfves = new ShelfRepository(_context);
		}

	    public void Dispose()
	    {
		   _context.Dispose();
	    }

	    public IBookRepository Books { get; private set; }
	    public IShelfRepository Shelfves { get; private set; }
		public int Complete()
		{
			return _context.SaveChanges();
		}
    }
}
