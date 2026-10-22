using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Dtos
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
