using AutoMapper;
using Library.Business.Dtos;
using Library.DataAccess.Repositories;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserDto? Register(RegisterDto registerDto)
        {
            var email = registerDto.Email.Trim().ToLowerInvariant();

            if (_userRepository.GetByEmail(email) != null)
            {
                return null;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                FirstName = registerDto.FirstName.Trim(),
                LastName = registerDto.LastName.Trim(),
                Email = email,
                PasswordHash = passwordHash
            };

            _userRepository.Add(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
