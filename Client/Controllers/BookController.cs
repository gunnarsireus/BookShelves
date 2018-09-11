using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Client.Models;
using Client.Models.BookViewModel;

namespace Client.Controllers
{
	public class BookController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public BookController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		// GET: book
		public async Task<IActionResult> Index(string id)
		{
			if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			var shelves = await Utils.Get<List<Shelf>>("api/Shelf");

			if (shelves.Any() && id == null)
				id = shelves[0].Id.ToString();
			var selectList = new List<SelectListItem>
			{
				new SelectListItem
				{
					Text = "Choose shelf",
					Value = ""
				}
			};
			selectList.AddRange(shelves.Select(shelf => new SelectListItem
			{
				Text = shelf.Name,
				Value = shelf.Id.ToString(),
				Selected = shelf.Id.ToString() == id
			}));
			var books = new List<Book>();

			if (id != null)
			{
				books = await Utils.Get<List<Book>>("api/book");
				var bookId = new Guid(id);
				books = books.Where(o => o.ShelfId == bookId).ToList();
			}

			var bookListViewModel = new BookViewModel()
			{
				ShelfSelectList = selectList,
				Books = books
			};

			ViewBag.ShelfId = id;
			return View(bookListViewModel);
		}

		// GET: book/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			var book = await Utils.Get<Book>("api/book/" + id);
			var Shelf = await Utils.Get<Shelf>("api/Shelf/" + book.ShelfId);
			ViewBag.ShelfName = Shelf.Name;
			return View(book);
		}

		// GET: book/Create
		public async Task<IActionResult> Create(string id)
		{
			var ShelfId = new Guid(id);
			var book = new Book
			{
				ShelfId = ShelfId,
			};
			var Shelf = await Utils.Get<Shelf>("api/Shelf/" + ShelfId);
			ViewBag.ShelfName = Shelf.Name;
			return View(book);
		}

		// POST: book/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("ShelfId,ISBN,Title,InShelf")] Book book)
		{
			if (!ModelState.IsValid) return View(book);
			book.Id = Guid.NewGuid();
			await Utils.Post<Book>("api/book/", book);

			return RedirectToAction("Index", new { id = book.ShelfId });
		}

		// GET: book/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			var book = await Utils.Get<Book>("api/book/" + id);
			book.Disabled = true; //Prevent updates of InShelf/Offline while editing
			await Utils.Put<Book>("api/book/" + id, book);
			var Shelf = await Utils.Get<Shelf>("api/Shelf/" + book.ShelfId);
			ViewBag.ShelfName = Shelf.Name;
			return View(book);
		}

		// POST: book/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id, InShelf")] Book book)
		{
			if (!ModelState.IsValid) return View(book);
			var oldbook = await Utils.Get<Book>("api/book/" + book.Id);
			oldbook.InShelf = book.InShelf;
			oldbook.Disabled = false; //Enable updates of InShelf/Offline when editing done
			await Utils.Put<Book>("api/book/" + oldbook.Id, oldbook);

			return RedirectToAction("Index", new { id = oldbook.ShelfId });
		}

		// GET: book/Delete/5
		public async Task<IActionResult> Delete(Guid id)
		{
			var book = await Utils.Get<Book>("api/book/" + id);
			return View(book);
		}

		// POST: book/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var book = await Utils.Get<Book>("api/book/" + id);
			await Utils.Delete<Book>("api/book/" + id);
			return RedirectToAction("Index", new { id = book.ShelfId });
		}

		public async Task<bool> ISBNAvailable(string isbn)
		{
			var books = await Utils.Get<List<Book>>("api/book");
			return books.All(c => c.ISBN != isbn);
		}

		public async Task<bool> TitleAvailable(string title)
		{
			var books = await Utils.Get<List<Book>>("api/book");
			return books.All(c => c.Title != title);
		}
	}
}