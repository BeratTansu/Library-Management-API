using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
