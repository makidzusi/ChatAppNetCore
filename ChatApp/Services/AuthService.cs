using ChatApp.Config;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using ChatApp.Contracts;
using ChatApp.DataAccess.Entities;

namespace ChatApp.Services
{

    public class AuthService
    {
        private readonly UserService _userSerivce;
        private readonly TokenService _tokenService;
        public AuthService(UserService userSerivce, TokenService tokenService)
        {
            _userSerivce = userSerivce;
            _tokenService = tokenService;

        }

        public async Task<User?> RegisterAsync(RegisterDTO registerDTO)
        {
            var result = await _userSerivce.CreateUserAsync(registerDTO);

            return result;
        }
        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO loginModel)

        {
            
            var user = await _userSerivce.GetUserByEmailAsync(loginModel.Email);
            if (user == null) return null;
            if(user.Password != loginModel.Password) return null;

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Email) };
            var jwt = _tokenService.CreateToken(claims, TimeSpan.FromDays(14));

            var response = new LoginResponseDTO
            {
                access_token = jwt,
                username = user.Email
            };

            return response;
        }

    }
}
