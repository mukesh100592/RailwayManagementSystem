using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RailwayManagementSystem.Models;
using RailwayManagementSystem.Services;

namespace RailwayManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LoginService _loginService;

        public HomeController(ILogger<HomeController> logger, LoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adminprocessing([Bind("Username,Password")] Login login)
        {
            string status = "";
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RMS;Integrated Security=True";
            String a = login.Username;
            String b = login.Password;
            using (SqlConnection con = new SqlConnection(connection))
            using (SqlCommand cmd1 = new SqlCommand("select * from admin where username=@username and password=@password"))
            {
                cmd1.Connection = con;
                con.Open();
                cmd1.Parameters.AddWithValue("@username", a);
                cmd1.Parameters.AddWithValue("@password", b);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                _loginService.Username = null;
                _loginService.IsAdminLoggedIn = false;
                _loginService.IsUserLoggedIn = false;

                if (sdr1.HasRows)
                {
                    sdr1.Read();
                    status = "Login successful.";
                    String username = (String)sdr1["username"];
                    TempData["username"] = username;
                    _loginService.Username = username;
                    _loginService.IsAdminLoggedIn = true;

                    return RedirectToActionPermanent("FullBookingHistory", "Home");
                }
                else
                {
                    TempData["adminmsg"] = "INVALID USERNAME/PASSWORD";
                    return RedirectToActionPermanent("AdminLogin", "Home");
                }

            }
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
