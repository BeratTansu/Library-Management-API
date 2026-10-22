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
        public AuthService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

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

        public LoginResponseDto? Login(LoginDto loginDto)
        {
            var email = loginDto.Email.Trim().ToLowerInvariant();

            var user = _userRepository.GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return null;
            }

            var token = _tokenService.CreateToken(user);
            return new LoginResponseDto { Token = token };
        }
    }
}
