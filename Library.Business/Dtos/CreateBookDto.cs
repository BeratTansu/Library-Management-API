using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public DateOnly PublicationDate { get; set; }
    }
}
