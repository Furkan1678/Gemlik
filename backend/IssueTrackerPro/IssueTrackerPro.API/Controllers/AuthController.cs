using Microsoft.AspNetCore.Http;
using IssueTrackerPro.Application.Features.User.Commands;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Infrastructure.Services.Authentication;
using IssueTrackerPro.Infrastructure.Services.Security;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        public AuthController(IPasswordHasher passwordHasher, JwtTokenService jwtTokenService, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userRepository.GetByUsername(request.Username);
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash)) // 30. satır
            {
                return Unauthorized("Invalid credentials");
            }
            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new { token, role = user.Role });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}