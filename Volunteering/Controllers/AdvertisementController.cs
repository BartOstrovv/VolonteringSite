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
        private readonly DonationService _donatService;
        private readonly UserManager<User> _userManager;
        private static int EditableAdverisementId = -1;
        public AdvertisementController(AdvertisementService serv, CommentService commentService, UserManager<User> userManager, DonationService donationService)
        {
            _adService = serv;
            _commentSevice = commentService;
            _userManager = userManager; 
            _donatService = donationService;
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

        [Authorize/*(Roles ="Admin, Owner")*/]
        [HttpPost]
        public async Task<IActionResult> Edit(Advertisement ad)
        {
            if (!String.IsNullOrEmpty(ad.Title) && !String.IsNullOrEmpty(ad.Body))
            {
                await _adService.EditAddvertisement(ad);
                return RedirectToAction("Details", new { id = ad.Id });
            }
            return View(await _adService.FindAdvertisementAsync(ad.Id));
        }

        public async Task<IActionResult> Edit(int id)
        {
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
            return View();
        }

        public ActionResult Donat(int id)
        {
            EditableAdverisementId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Donat(Donation donat)
        {
            donat.AdvertisementId = EditableAdverisementId;
            var ad = await _adService.FindAdvertisementAsync(EditableAdverisementId);
            if (!ad.Close)
            {
                ad.CurrentMoney += donat.Sum;
                ad.Close = ad.CurrentMoney >= ad.NeedMoney;
                await _adService.EditAddvertisement(ad);
            }
            await _donatService.NewDonat(EditableAdverisementId, _userManager.GetUserAsync(HttpContext.User).Result.Id, donat.Comment, donat.DateTime, donat.Sum);
            EditableAdverisementId = -1;
            return RedirectToAction("Details", new { id = donat.AdvertisementId });
        }
        public async Task<ActionResult> FindAds(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return View("FindResult", (await _adService.FindAds(text)));
            return View();
        }
    }
}
