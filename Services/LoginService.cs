using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Data;
using RailwayManagementSystem.Models;

namespace RailwayManagementSystem.Services
{
    public class LoginService
    {
        private RMSContext _context;

        public LoginService()
        {
            LoggedInUsername = string.Empty;
        }
        public void InjectServices(RMSContext context)
        {
            _context = context;
        }
        public static bool IsLoggedIn { get; private set; }
        public static bool IsAdmin { get; private set; }
        public static string LoggedInUsername { get; private set; }

        public async Task<bool> Login(LoginInfo loginInfo)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Username == loginInfo.Username && u.Password == loginInfo.Password);
            if (user != null)
            {
                IsLoggedIn = true;
                LoggedInUsername = user.Username;
                user.IsActive = true;
                IsAdmin = user.IsAdmin;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                IsLoggedIn = false;
            }
            return IsLoggedIn;
        }

        public async Task<bool> Logout()
        {
            IsLoggedIn = false;
            LoggedInUsername = string.Empty;
            IsAdmin = false;
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == LoginService.LoggedInUsername);
            if (user != null)
            {
                user.IsActive = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return !IsLoggedIn;
        }
    }
}