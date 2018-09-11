using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Api.Models;
using Api.Data;
using Api.DAL;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly BookUnitOfWork _unitOfWork;
        public BookController(ApiContext context)
        {
            _unitOfWork = new BookUnitOfWork(context);
        }
        // GET api/Book
        [HttpGet]
        [EnableCors("AllowAllOrigins")]
        public IEnumerable<Book> GetBooks()
        {
            return _unitOfWork.Books.GetAll();
        }

        // GET api/Book/5
        [HttpGet("{id}")]
        [EnableCors("AllowAllOrigins")]
        public Book GetBook(string id)
        {
            return _unitOfWork.Books.Get(new Guid(id));
        }

        // POST api/Book
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public void AddBook([FromBody] Book book)
        {
            _unitOfWork.Books.Add(book);
            _unitOfWork.Complete();
        }

        // PUT api/Book/5
        [HttpPut("{id}")]
        [EnableCors("AllowAllOrigins")]
        public void UpdateBook([FromBody] Book book)
        {
            _unitOfWork.Books.Update(book);
            _unitOfWork.Complete();
        }

        // DELETE api/Book/5
        [HttpDelete("{id}")]
        [EnableCors("AllowAllOrigins")]
        public void DeleteBook(string id)
        {
            var account = _unitOfWork.Books.Get(new Guid(id));
            _unitOfWork.Books.Remove(account);
            _unitOfWork.Complete();
        }
    }
}