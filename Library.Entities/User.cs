using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Member";
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
