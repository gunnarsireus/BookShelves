using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Models.BookViewModel
{
    public class BookViewModel : Book
    {
        public List<SelectListItem> ShelfSelectList { get; set; }
        public List<Book> Books { get; set; }
    }
}