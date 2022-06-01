using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Volunteering.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly AdvertisementService _adService;
        private readonly UserManager<User> _userManager;
        public AdminController(UserService userService, AdvertisementService adService, UserManager<User> userManager)
        {
            _userService = userService;
            _adService = adService;
            _userManager = userManager;
        }


        public ActionResult Index() => View();

        public async Task<IActionResult> Users() => View(await _userService.GetAllAsync());
        public async Task<IActionResult> ClosedAd() => View(await _adService.GetAllByAsync(x => x.Close));
        public async Task<IActionResult> ApprovedAd() => View(await _adService.GetAllByAsync(x => x.Aproved));
        public async Task<IActionResult> NotApprovedAd() => View(await _adService.GetAllByAsync(x => !x.Aproved));
        public async Task<IActionResult> AllAd() => View(await _adService.GetAllAsync());

        public async Task ConfirmAd(int adId)
        {
            var ad = await _adService.FindAdvertisementAsync(adId);
            ad.Aproved = true;
            await _adService.UpdateAsync(ad);
        }

        public async Task RejectAd(int adId)
        {
            var ad = await _adService.FindAdvertisementAsync(adId);
            ad.Aproved = false;
            await _adService.UpdateAsync(ad);
        }

        public async Task CloseAd(int adId)
        {
            var ad = await _adService.FindAdvertisementAsync(adId);
            ad.Close = true;
            await _adService.UpdateAsync(ad);
        }

        public async Task SetUserToAdmin(string userId) => await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(userId), "Admin");
    }
}
