using Library.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Services
{
    public interface IAuthService
    {
        UserDto? Register(RegisterDto registerDto);
    }
}
