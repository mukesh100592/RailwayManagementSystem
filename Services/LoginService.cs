﻿using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Data;
using RailwayManagementSystem.Models;

namespace RailwayManagementSystem.Services
{
    public class LoginService
    {
        private RMSContext _context;
        private static Dictionary<int, string> _userNames = new Dictionary<int, string>();

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
        private User CurrentUser { get; set; }
        public int GetCurrentUserId()
        {
            return CurrentUser?.Id ?? 0;
        }
        public async Task<bool> Login(LoginInfo loginInfo)
        {
            CurrentUser = await _context.User
                .FirstOrDefaultAsync(u => u.Username == loginInfo.Username && u.Password == loginInfo.Password);
            if (CurrentUser != null)
            {
                IsLoggedIn = true;
                LoggedInUsername = CurrentUser.Username;
                CurrentUser.IsActive = true;
                IsAdmin = CurrentUser.IsAdmin;
                _context.Update(CurrentUser);
                await _context.SaveChangesAsync();
                LoadUserNames();
            }
            else
            {
                IsLoggedIn = false;
            }
            return IsLoggedIn;
        }

        private void LoadUserNames()
        {
            _userNames = _context.User.ToDictionary(u => u.Id, u => u.Username);
        }

        public async Task<bool> Logout()
        {
            IsLoggedIn = false;
            LoggedInUsername = string.Empty;
            IsAdmin = false;
            CurrentUser = await _context.User.FirstOrDefaultAsync(u => u.Username == LoginService.LoggedInUsername);
            if (CurrentUser != null)
            {
                CurrentUser.IsActive = false;
                _context.Update(CurrentUser);
                await _context.SaveChangesAsync();
            }
            return !IsLoggedIn;
        }

        public static string GetUserName(int userId)
        {
            var user = _userNames.FirstOrDefault(u => u.Key == userId);
            return user.Value != null ? user.Value : "Unknown User";
        }


    }
}