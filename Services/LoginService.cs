namespace RailwayManagementSystem.Services
{
    public class LoginService
    {
        public LoginService()
        {
                
        }

        public bool IsUserLoggedIn { get; set; }
        public bool IsAdminLoggedIn { get; set; }
        public string Username { get; set; }// logged in user's username



    }
}
