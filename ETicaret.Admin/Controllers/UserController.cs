using ETicaret.Admin.Models;
using ETicaret.Data;
using ETicaret.Data.Common;
using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using ETicaret.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.Admin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ETicaretDbContext _dbContext;
        private readonly IRepository<User> _repository;

        public UserController(IRepository<User> repository, ETicaretDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public IActionResult Profile()
        {

            var User = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var CurrentUser = _repository.Get(User);
            var profile = new UserViewModel()
            {
                Name = CurrentUser.Name,
                Surname = CurrentUser.Surname,
                Role = CurrentUser.Role.ToString(),
                EMail = CurrentUser.EMail,
                ImageString = Convert.ToBase64String(CurrentUser.Picture)
            };
            return View(profile);
        }


        [HttpPost]
        public IActionResult Edit(UserViewModel model)
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
            CurrentUser.CreatedById = CurrentUser.CreatedById;
            CurrentUser.IsActive = true;
            CurrentUser.UpdatedDate = DateTime.Now;
            CurrentUser.UpdatedById = User;

            var result = _repository.Update(CurrentUser);
            if (result)
                TempData["ProfileUpdateState"] = AlertService.ShowAlert(AlertStates.Success, "Güncelleme başarılı.");
            else
                TempData["ProfileUpdateState"] = AlertService.ShowAlert(AlertStates.Danger, "Güncelleme esnasında bir hata oluştu.");

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        public IActionResult ChangePassword(UserViewModel model)
        {
            var User = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var CurrentUser = _repository.Get(User);
            if (model.Password != CurrentUser.Password)
            {
                TempData["Password"] = AlertService.ShowAlert(AlertStates.Danger, "Parolanız doğru değil");
                return RedirectToAction(nameof(Profile));
            }
            CurrentUser.Password = model.NewPassword;
            var result = _repository.Update(CurrentUser);
            if (result)
            {
                TempData["Password"] = AlertService.ShowAlert(AlertStates.Success, "Parolanız başarıyla değiştirildi.");
            }
            return RedirectToAction(nameof(Profile));
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
