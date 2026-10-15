using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateOnly PublicationDate { get; set; }
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
