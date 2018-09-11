using System.Collections.Generic;

namespace Client.Models.HomeViewModel
{
    public class HomeViewModel : Book
    {
	    public List<Shelf> Shelves { get; set; }
	}
}