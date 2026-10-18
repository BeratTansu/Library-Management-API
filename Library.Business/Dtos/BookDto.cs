using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateOnly PublicationDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
