using ChatApp.Contracts;
using ChatApp.DataAccess;
using ChatApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Services
{
    public class UserService
    {
        private readonly ChatAppContext _context;
        public UserService(ChatAppContext context) { 
            _context = context;
        }

        public async Task<User?> CreateUserAsync(RegisterDTO registerDTO)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == registerDTO.Email);
            if (existingUser != null)
            {
                return null;
            }
            var user = new User { Email= registerDTO.Email, Name = registerDTO.Name, Password = registerDTO.Password };
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}
