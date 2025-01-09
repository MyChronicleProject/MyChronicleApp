using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyChronicle.API.DTOs;
using MyChronicle.API.Services;
using MyChronicle.Domain;
using System.Security.Claims;

namespace MyChronicle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized();
            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (result)
            {
                return new UserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = _tokenService.CreateToken(user)
                };
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if(await _userManager.Users.AnyAsync(x => x.UserName == registerDTO.UserName))
            {
                return BadRequest("Username is taken");
            }

            var user = new AppUser
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                return new UserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = _tokenService.CreateToken(user)
                };
            }

            return BadRequest("Problem with user registration");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
