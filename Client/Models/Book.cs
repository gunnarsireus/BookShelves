using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Client.Models
{
    public class Book
    {
        public Book()
        {
            CreationTime = DateTime.Now.ToString(new CultureInfo("en-US"));
            InShelf = true;
        }
        public Guid Id { get; set; }
        public Guid ShelfId { get; set; }

        [Display(Name = "Created date")]
        public string CreationTime { get; set; }

        [Remote("TitleAvailable", "book", ErrorMessage = "Title used")]
        public string Title { get; set; }
        public string Author { get; set; }
        public string LoanedTo { get; set; }

        [Remote("ISBNAvailable", "book", ErrorMessage = "ISBN number taken or wrong format")]
        public string ISBN { get; set; }

        [Display(Name = "InShelf")]
        public bool InShelf { get; set; }

        [Display(Name = "In Shelf or Loaned?")]
        public string InShelfNotInShelf => InShelf ? "Available" : "Loaned To: " + LoanedTo;

        public bool Disabled { get; set; } //Used to block changes of InShelf/Offline status
    }
}
