using Library.DataAccess.Context;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }

        private readonly LibraryDbContext _context;

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
