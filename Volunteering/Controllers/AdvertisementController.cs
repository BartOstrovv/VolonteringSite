using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Volunteering.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly AdvertisementService _adService;
        private readonly UserService _userSevice;
        public AdvertisementController(AdvertisementService serv, UserService userService)
        {
            _adService = serv;
            _userSevice = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View((await _adService.GetAllAsync()).OrderBy(x => x.CreatedDate));
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _adService.FindAdvertisementAsync(id));
        }

       
        [Authorize]
        public async Task<IActionResult> Create(Advertisement ad)
        {
            if (ad.Title != null)
            {
                
                await _adService.AddAdvertisementAsync(ad);
    
                return RedirectToAction("Index");
            }

            return View(ad);
        }

        public ActionResult Edit(int id)
        {
            var ad = _adService.FindAdvertisementAsync(id).Result;
            return View(ad);
        }
        [Authorize(Roles ="Admin, Owner")]
        public ActionResult Edit(Advertisement ad)
        {
            if (ModelState.IsValid)
            {
                _adService.EditAddvertisement(ad, ad.Id);
                return RedirectToAction("Index");
            }
            return View(ad);
        }

        [Authorize]
        public ActionResult AddComment(Comment comment, int adId)
        {
            var ad = _adService.AddCommentToAd(comment, adId);
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            _userSevice.AddCommentToUser(currentUser.Identity.Name, comment);
            return RedirectToAction("Edit", ad);
        }
        public async Task<ActionResult> FindAds(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return View("FindResult", (await _adService.FindAds(text)));
            return View();
        }
    }
}
