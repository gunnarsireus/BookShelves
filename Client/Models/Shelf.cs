using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Client.Models
{
	public class Shelf
	{
		public Shelf()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("en-US"));
		}
		public Guid Id { get; set; }

		[Display(Name = "Created date")]
		public string CreationTime { get; set; }
		public string Genre { get; set; }
		public string Location { get; set; }

		public ICollection<Book> Books { get; set; }
	}
}
