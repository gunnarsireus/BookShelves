using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.DAL;
using Api.Models;

namespace Api.Data
{
	public class BookRepository : Repository<Book>, IBookRepository
	{
		public BookRepository(ApiContext context) : base(context)
		{
		}

		public ApiContext ApiContext => Context as ApiContext;

	}
}
