using Api.DAL;
using Api.Data;
using Api.Models;
using System.Collections.Generic;

namespace Api.Services
{
    public class Bookshelf : IBookshelf
    {
        private readonly BookUnitOfWork _unitOfWork;
        public Bookshelf(IApiContext context)
        {
            _unitOfWork = new BookUnitOfWork(context);
        }

        private IEnumerable<Book> books;

        public IEnumerable<Book> Books
        {
            get { return _unitOfWork.Books.GetAll(); }
            set { books = value; }
        }

        public bool Loan(Book book)
        {
            var oldBook = _unitOfWork.Books.Get(book.Id);
            if (oldBook == null) return false;
            oldBook = book;
            oldBook.Disabled = false;
            oldBook.InShelf = false;
            _unitOfWork.Books.Update(oldBook); //LoanedTo has to be set earlier
            return true;
        }

        public bool Return(Book book)
        {
            var oldBook = _unitOfWork.Books.Get(book.Id);
            if (oldBook == null) return false;
            oldBook = book;
            oldBook.Disabled = false;
            oldBook.InShelf = true;
            oldBook.LoanedTo = "";
            _unitOfWork.Books.Update(oldBook);
            return true;
        }
    }
}
