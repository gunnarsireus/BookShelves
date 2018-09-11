using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Models.HomeViewModel;

namespace Client.Controllers
{
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
            ViewBag.ApiUrl = Environment.GetEnvironmentVariable("ApiUrl");
            List<Shelf> shelves;
			try
			{
				shelves = await Utils.Get<List<Shelf>>("api/Shelf");
			}
			catch (Exception e)
			{
				TempData["CustomError"] = "Ingen kontakt med servern! bookApi måste startas innan Client kan köras!";
				return View(new HomeViewModel { Shelves = new List<Shelf>()});
			}

			var allbooks = await Utils.Get<List<Book>>("api/book");
            foreach (var book in allbooks)
            {
                book.Disabled = false; //Enable updates of InShelf/Offline
                await Utils.Put<Book>("api/book" + book.Id, book);
            }

            foreach (var Shelf in shelves)
			{
				var Shelfbooks = allbooks.Where(o => o.ShelfId == Shelf.Id).ToList();
				Shelf.Books = Shelfbooks;
			}
			var homeViewModel = new HomeViewModel { Shelves = shelves };
			return View(homeViewModel);
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}