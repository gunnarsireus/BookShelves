using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Models.ShelfViewModel;

namespace Client.Controllers
{
	public class ShelfController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public ShelfController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}


		// GET: Shelf

		public async Task<IActionResult> Index()
		{
			if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			var shelves = await Utils.Get<List<Shelf>>("api/Shelf");

			foreach (var shelf in shelves)
			{
				var books = await Utils.Get<List<Book>>("api/book");
				books = books.Where(c => c.ShelfId == shelf.Id).ToList();
				shelf.Books = books;
			}

			var shelfViewModel = new ShelfViewModel { Shelves = shelves };

			return View(shelfViewModel);
		}

		// GET: Sheld/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			var shelf = await Utils.Get<Shelf>("api/Shelf/" + id);

			return View(shelf);
		}

		// GET: Shelf/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Shelf/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Address,CreationTime")] Shelf shelf)
		{
			if (!ModelState.IsValid) return View(shelf);
			shelf.Id = Guid.NewGuid();
			await Utils.Post<Shelf>("api/Shelf/", shelf);

			return RedirectToAction(nameof(Index));
		}

		// GET: Shelf/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			var shelf = await Utils.Get<Shelf>("api/Shelf/" + id);
			return View(shelf);
		}

		// POST: Shelf/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreationTime, Name, Address")] Shelf shelf)
		{
			if (!ModelState.IsValid) return View(shelf);
			var oldShelf = await Utils.Get<Shelf>("api/Shelf/" + id);

			oldShelf.Name = shelf.Name;
			oldShelf.Address = shelf.Address;
			await Utils.Put<Shelf>("api/Shelf/" + oldShelf.Id, oldShelf);

			return RedirectToAction(nameof(Index));
		}

		// GET: Shelf/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			var shelf = await Utils.Get<Shelf>("api/Shelf/" + id);
			return View(shelf);
		}

		// POST: Shelf/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			await Utils.Delete<Shelf>("api/Shelf/" + id);
			return RedirectToAction(nameof(Index));
		}

		private async Task<bool> ShelfExists(Guid id)
		{
			var shelves = await Utils.Get<List<Shelf>>("api/Shelf");
			return shelves.Any(e => e.Id == id);
		}
	}
}