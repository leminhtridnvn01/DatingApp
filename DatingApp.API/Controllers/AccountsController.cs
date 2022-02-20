using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using asp_net_core.DatingApp.DatingApp.API.Database;
using asp_net_core.DatingApp.DatingApp.API.Database.Entities;
using asp_net_core.DatingApp.DatingApp.API.DTOs;
using asp_net_core.DatingApp.DatingApp.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core.DatingApp.DatingApp.API.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountsController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }
        [HttpPost("register")]
        public ActionResult<string> Register(RegisterDTO registerDTO)
        {
            registerDTO.Username.ToLower();
            if (_context.Users.Any(u => u.Username == registerDTO.Username))
            {
                return BadRequest("Username is exited!");
            }
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(new UserResponseDTO
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            });
        }
        [HttpPost("login")]
        public ActionResult<string> Login(LoginDTO loginDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginDTO.Username.ToLower());
            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (var i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return Ok(new UserResponseDTO
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            });
        }

    }
}