using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Volunteering.Models;

namespace Volunteering.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, UserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Cabinet()
        {
            var userID = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            if (String.IsNullOrEmpty(userID))
                return RedirectToAction("Error");
            return View(await _userService.FindUserAsync(userID));
        }

        public async Task<IActionResult> DonationsList(string id)
        {
            return View((await _userService.FindUserAsync(id)).Donations);
        }

        public async Task<IActionResult> AdsList(string id)
        {
            return View((await _userService.FindUserAsync(id)).Advertisements);
        }

        public async Task<IActionResult> CommentsList(string id)
        {
            return View((await _userService.FindUserAsync(id)).Comments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}