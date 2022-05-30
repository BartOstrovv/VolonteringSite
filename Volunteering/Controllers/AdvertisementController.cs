using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Volunteering.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly AdvertisementService _adService;
        private readonly CommentService _commentSevice;
        private readonly UserManager<User> _userManager;
        private static int EditableAdverisementId = -1;
        public AdvertisementController(AdvertisementService serv, CommentService commentService, UserManager<User> userManager)
        {
            _adService = serv;
            _commentSevice = commentService;
            _userManager = userManager; 
        }

        public async Task<IActionResult> Index()
        {
            return View((await _adService.GetAllAsync()).OrderBy(x => x.CreatedDate));
        }

        public async Task<IActionResult> Details(int id)
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

       /* public ActionResult Edit(int id)
        {
            var ad = _adService.FindAdvertisementAsync(id).Result;
            return View(ad);
        }*/
        [Authorize/*(Roles ="Admin, Owner")*/]
        public async Task<IActionResult> Edit(int id)
        {
            /*if (ModelState.IsValid)
            {
                _adService.EditAddvertisement(ad, ad.Id);
                return RedirectToAction("Index");
            }*/
            return View(await _adService.FindAdvertisementAsync(id));
        }

        [Authorize]
        public async Task<IActionResult> AddComment(int adId, string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                adId = EditableAdverisementId;
                EditableAdverisementId = -1;
                string userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
                var op = _commentSevice.NewComment(text, adId, userId).Result;
                if (op.IsSuccessful)
                    return RedirectToAction("Details", new {id = adId});
            }
            else
            {
                EditableAdverisementId = adId;
            }
            return View(await _adService.FindAdvertisementAsync(adId));
        }
        public async Task<ActionResult> FindAds(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return View("FindResult", (await _adService.FindAds(text)));
            return View();
        }
    }
}
