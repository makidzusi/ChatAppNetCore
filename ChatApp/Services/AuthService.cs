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
        public AuthService(UserService userSerivce)
        {
            _userSerivce = userSerivce;
        }

        public async Task<User?> RegisterAsync(RegisterDTO registerDTO)
        {
            var result = await _userSerivce.CreateUserAsync(registerDTO);

            return result;
        }
        public async Task<LoginResponse?> LoginAsync(LoginDTO loginModel)

        {
            var people = new List<Person>
             {
                new Person { Email = "tom@gmail.com", Password = "11111"},
                new Person {Email = "bob@gmail.com", Password= "55555"},
                new Person {Email = "sam@gmail.com", Password = "22222"}
            };

            // находим пользователя 
            Person? person = people.FirstOrDefault(p => p.Email == loginModel.Email && p.Password == loginModel.Password);
            // если пользователь не найден, отправляем статусный код 401
            if (person is null) return null;

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, person.Email) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(14)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new LoginResponse
            {
                access_token = encodedJwt,
                username = person.Email
            };

            return response;
        }

    }
    public class LoginResponse
    {
        public string access_token { get; set; }
        public string username { get; set; }

    }
}
