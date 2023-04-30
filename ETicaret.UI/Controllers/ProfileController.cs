using ETicaret.Data.Common;
using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using ETicaret.Services.Interfaces;
using ETicaret.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.UI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IRepository<Customer> _repository;

        public ProfileController(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public IActionResult MyProfile()
        {
            var User = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var currentUser = _repository.Get(User);
            var profile = new CustomerViewModel()
            {
                Adress = currentUser.Adress,
                City = currentUser.City,
                Country = currentUser.Country,
                EMail = currentUser.EMail,
                ImageByteArray = currentUser.Picture,
                Name = currentUser.Name,
                Surname = currentUser.Surname,
                Phone = currentUser.Phone,

            };
            profile.ImageString = Convert.ToBase64String(profile.ImageByteArray);
            return View(profile);
        }
        [HttpPost]
        public IActionResult Edit(CustomerViewModel model)
        {
            var User = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var CurrentUser = _repository.Get(User);

            if (model.Picture != null)
            {
                var ms = new MemoryStream();
                model.Picture.CopyTo(ms);
                CurrentUser.Picture = ms.ToArray();
            }
            else
            {
                CurrentUser.Picture = CurrentUser.Picture;
            }

            CurrentUser.Name = model.Name;
            CurrentUser.Surname = model.Surname;
            CurrentUser.EMail = model.EMail;
            CurrentUser.IsActive = true;
            CurrentUser.UpdatedDate = DateTime.Now;
            CurrentUser.UpdatedById = User;
            CurrentUser.Adress = model.Adress;
            CurrentUser.City = model.City;
            CurrentUser.Country = model.Country;
            CurrentUser.Phone = model.Phone;

            var newClaimName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            newClaimName = model.Name;
            var newClaimSurname = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname).Value;
            newClaimSurname = model.Surname;

            var result = _repository.Update(CurrentUser);
            if (result)
                TempData["ProfileUpdateState"] = AlertService.ShowAlert(AlertStates.Success, "Güncelleme başarılı.");
            else
                TempData["ProfileUpdateState"] = AlertService.ShowAlert(AlertStates.Danger, "Güncelleme esnasında bir hata oluştu.");

            return RedirectToAction(nameof(MyProfile));

        }
    }
}
