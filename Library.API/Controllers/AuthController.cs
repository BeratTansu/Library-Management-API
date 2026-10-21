using FluentValidation;
using Library.Business.Dtos;
using Library.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthService authService, IValidator<RegisterDto> registerValidator)
        {
            _authService = authService;
            _registerValidator = registerValidator;
        }

        private readonly IAuthService _authService;
        private readonly IValidator<RegisterDto> _registerValidator;

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var validationResult = _registerValidator.Validate(registerDto);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            var userDto = _authService.Register(registerDto);

            if (userDto == null)
            {
                return Conflict("A user with this email already exists");
            }

            return StatusCode(StatusCodes.Status201Created, userDto);
        }
    }
}
