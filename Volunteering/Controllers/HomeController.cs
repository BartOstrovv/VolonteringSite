using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Volunteering.Models;
using Volunteering.ViewModels;

namespace Volunteering.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, UserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
            _environment = webHostEnvironment;
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

        public async Task<IActionResult> UserInfo(string id)
        {
            var us = await _userService.FindUserAsync(id);
            return View(us);
        }

        [Authorize/*(Roles ="Admin, Owner")*/]
        
        public async Task<IActionResult> EditInfo(PersonDataViewModel info)
        {
            var id = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var currUser = await _userService.FindUserAsync(id);

            if (info.IsEmpty())
            {
                info.Init(currUser?.PersonData);
                return View(info);
            }
            
            var rootPath = _environment.WebRootPath;
            rootPath = Path.Combine(rootPath, "Images");
            var savePath = Path.Combine(rootPath, info.Photo.FileName);
            Request.Form.Files[0].CopyTo(System.IO.File.Create(savePath));
            var path = Path.Combine("/Images", info.Photo.FileName);

            if (currUser != null)
            {
                var newPersonInfo = new PersonData()
                {
                    Name = info.Name,
                    Surname = info.Surname,
                    Mobile = info.Mobile,
                    Age = info.Age,
                    Photo = new Photo()
                    {
                        PhotoPath = path
                    },
                    Address = new Address()
                    {
                        Country = info.Country,
                        City = info.City,
                        Street = info.City,
                        Build = info.Build
                    }
                };
                currUser.PersonData = newPersonInfo;
                await _userService.UpdateAsync(currUser);
            }
            return RedirectToAction("Cabinet");
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