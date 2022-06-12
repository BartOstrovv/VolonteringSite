using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Volunteering.ViewModels;

namespace Volunteering.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly AdvertisementService _adService;
        private readonly CommentService _commentSevice;
        private readonly DonationService _donatService;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;
        private static int EditableAdverisementId = -1;
        private readonly IWebHostEnvironment _environment;
        public AdvertisementController(AdvertisementService serv, CommentService commentService, UserManager<User> userManager, DonationService donationService, IWebHostEnvironment webHostEnvironment, UserService userService)
        {
            _adService = serv;
            _commentSevice = commentService;
            _userManager = userManager; 
            _donatService = donationService;
            _environment = webHostEnvironment;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var adsWithUser = new List<AdWithAuthorViewModel>();
            var ads = await _adService.GetAllAsync();
            foreach(var ad in ads)
            {
                var us = await _userService.FindUserAsync(ad.UserId);
                if (us == null)
                    continue;
                adsWithUser.Add(new AdWithAuthorViewModel()
                {
                    Title = ad.Title,
                    Body = ad.Body,
                    UserId = us.Id,
                    CreatedDate = ad.CreatedDate,
                    UserPhotoPath = us?.PersonData?.Photo?.PhotoPath,
                    Images = ad?.Images,
                    CurrentMoney = ad?.CurrentMoney,
                    NeedMoney = ad?.NeedMoney,
                    UserName = us?.PersonData?.Name,
                    UserSurname = us?.PersonData?.Surname,
                });
            }
            return View(adsWithUser);
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _adService.FindAdvertisementAsync(id));
        }

       
        [Authorize]
        public async Task<IActionResult> Create(AdvertisementViewModel ad)
        {
            if (!ad.IsEmpty())
            {
                var rootPath = _environment.WebRootPath;
                rootPath = Path.Combine(rootPath, "Images");
                var listPhoto = new List<Photo>();
                foreach (var file in Request.Form.Files)
                {
                    var savePath = Path.Combine(rootPath, file.FileName);
                    file.CopyTo(System.IO.File.Create(savePath));
                    var path = Path.Combine("/Images", file.FileName);
                    listPhoto.Add(new Photo() { PhotoPath = path });
                }

                var adverisementModel = new Advertisement()
                {
                    Title = ad.Title,
                    Body = ad.Body,
                    NeedMoney = ad.NeedMoney,
                    CreatedDate = ad.CreatedDate,
                    DeliveryAddress = new Address()
                    {
                        Country = ad.DeliveryCountry,
                        City = ad.DeliveryCity,
                        Street = ad.DeliveryStreet,
                        Build = ad.DeliveryBuild
                    },
                    Images = listPhoto,
                    UserId = _userManager.GetUserAsync(HttpContext.User).Result.Id
            };
                await _adService.AddAdvertisementAsync(adverisementModel);
    
                return RedirectToAction("Index");
            }

            return View(ad);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Advertisement ad)
        {
            if (!String.IsNullOrEmpty(ad.Title) && !String.IsNullOrEmpty(ad.Body))
            {
                await _adService.UpdateAsync(ad);
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

        public async Task<IActionResult> Donations(int id)
        {
            return View(await _donatService.GetFromAdAsync(id));
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
                await _adService.UpdateAsync(ad);
            }
            await _donatService.NewDonat(EditableAdverisementId, _userManager.GetUserAsync(HttpContext.User).Result.Id, donat.Comment, donat.DateTime, donat.Sum);
            EditableAdverisementId = -1;
            return RedirectToAction("Details", new { id = donat.AdvertisementId });
        }
        public async Task<ActionResult> FindAds(string text)
        {
            if (!String.IsNullOrEmpty(text))
                return View("FindResult", (await _adService.FindByAdContentAsync(text)));
            return View();
        }
    }
}
