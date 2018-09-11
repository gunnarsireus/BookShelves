using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
	public class Book
	{
		public Book()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("se-SE"));
			InShelf = true;
		}
		public Guid Id { get; set; }
		public Guid ShelfId { get; set; }
		public string CreationTime { get; set; }
		public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
		public bool InShelf { get; set; }
		public bool Disabled { get; set; } //Used to block changes of InShelf status
	}
}
