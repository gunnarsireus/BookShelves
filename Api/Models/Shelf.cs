using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
	public class Shelf
	{
		public Shelf()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("se-SE"));
		}
		public Guid Id { get; set; }
		public string CreationTime { get; set; }
		public string Genre { get; set; }
		public string Location { get; set; }

		public ICollection<Book> Books { get; set; }
	}
}
