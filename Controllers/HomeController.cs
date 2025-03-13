using System.Diagnostics;
using System.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Data;
using RailwayManagementSystem.Models;
using RailwayManagementSystem.Services;

namespace RailwayManagementSystem.Controllers
{
    /// <summary>
    /// Controller for the home page
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LoginService _loginService;
        private readonly RMSContext _rMSContext;

        public HomeController(ILogger<HomeController> logger, LoginService loginService, RMSContext rMSContext)
        {
            _logger = logger;
            _loginService = loginService;
            _rMSContext = rMSContext;
            _loginService.InjectServices(_rMSContext);
        }

        /// <summary>
        /// Opens the home page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // if user is logged in then redirect to index page
            if (LoginService.IsLoggedIn)
            {
                return View();
            }
            // if user is not logged in then redirect to login page
            return RedirectToAction(nameof(Login));
        }

        /// <summary>
        /// Opens the login page
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Opens the privacy page
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: HomeController/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginProcess([Bind("Username,Password")] LoginInfo loginInfo)
        {
            if (ModelState.IsValid)
            {
                if (await _loginService.Login(loginInfo))
                {
                    // if login is successful then redirect to create booking page
                    ModelState.Clear();
                    return RedirectToAction("Create", "BookingInfoes");
                }
            }
            return View(nameof(Login));
        }

        // GET: HomeController/Logout
        /// <summary>
        /// Logs out the user and opens the login page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            if (await _loginService.Logout())
            {
                // if logout is successful then redirect to login page
                return RedirectToAction(nameof(Login));
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
