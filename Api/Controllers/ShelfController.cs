using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Models;
using Api.DAL;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ShelfController : Controller
    {
        private readonly BookUnitOfWork _unitOfWork;
        public ShelfController(ApiContext context)
        {
            _unitOfWork = new BookUnitOfWork(context);
        }
        // GET api/Shelf
        [HttpGet]
        public IEnumerable<Shelf> GetShelves()
        {
            return _unitOfWork.Shelfves.GetAll();
        }

        // GET api/Shelf/5
        [HttpGet("{id}")]
        public Shelf GetShelf(string id)
        {
            return _unitOfWork.Shelfves.Get(new Guid(id));
        }

        // POST api/Shelf
        [HttpPost]
        public void AddShelf([FromBody] Shelf shelf)
        {
            _unitOfWork.Shelfves.Add(shelf);
            _unitOfWork.Complete();
        }

        // PUT api/Shelf/5
        [HttpPut("{id}")]
        public void UpdateShelf([FromBody] Shelf shelf)
        {
            _unitOfWork.Shelfves.Update(shelf);
            _unitOfWork.Complete();
        }

        // DELETE api/Shelf/5
        [HttpDelete("{id}")]
        public void DeleteShelf(string id)
        {
            var account = _unitOfWork.Shelfves.Get(new Guid(id));
            _unitOfWork.Shelfves.Remove(account);
            _unitOfWork.Complete();
        }
    }
}