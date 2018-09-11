using Api.Models;
using System.Collections.Generic;

namespace Api.Services
{
    interface IBookshelf
    {
        IEnumerable<Book> Books { get; }
        bool Loan(Book book);
        bool Return(Book book);
    }
}
