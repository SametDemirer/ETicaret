using ETicaret.Admin.Models;
using ETicaret.Data;
using ETicaret.Data.Common;
using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using ETicaret.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ETicaretDbContext _dbcontext;
        private readonly IRepository<User> _repository;
        private readonly IMailRepository _mailRepository;

        public AccountController(ETicaretDbContext dbcontext, IRepository<User> repository, IMailRepository mailRepository)
        {
            _dbcontext = dbcontext;
            _repository = repository;
            _mailRepository = mailRepository;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_dbcontext.Users.Any(x => x.EMail == model.EMail))
            {
                ViewBag.Alert = AlertService.ShowAlert(AlertStates.Warning, "Aynı email ile daha önce hesap oluşturulmuş, lütfen farklı bir email ile tekrar deneyiniz.");
                return View(model);
            }
            var newUser = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                EMail = model.EMail,
                Password = model.Password,
                Role = Role.Admin,
                CreatedById = 1

            };

            var ms = new MemoryStream();
            model.Picture.CopyTo(ms);
            newUser.Picture = ms.ToArray();

            var isUserAdded = _repository.Create(newUser);

            if (!isUserAdded)
            {
                ViewBag.Alert = AlertService.ShowAlert(AlertStates.Danger, "Kayıt esnasında bir proplem oluştu lütfen bilgilerinizi kontrol edip daha sonra tekrar deneyiniz.");
                return View(model);
            }
            TempData["FromRegisterMessage"] = AlertService.ShowAlert(AlertStates.Success, "Hoşgeldiniz ! Devam etmek için giriş yapınız.");
            return RedirectToAction(nameof(Login));

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _dbcontext.Users.FirstOrDefault(x => x.EMail == model.Email && x.Password == model.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Surname,user.Surname),
                    new Claim(ClaimTypes.Email,user.EMail),
                    new Claim(ClaimTypes.Role,user.Role.ToString()),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.WrongPassword = AlertService.ShowAlert(AlertStates.Danger, "Kullanıcı adı veya parola yanlış lütfen tekrar deneyiniz.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _dbcontext.Users.FirstOrDefault(x => x.EMail == model.Email && x.IsActive);
            if (user == null)
            {
                ViewBag.NullUser = AlertService.ShowAlert(AlertStates.Warning, "Veritabanında bu bilgilere ait kullanıcı bulunamadı.");
                return View(model);
            }
            Guid guidKey = Guid.NewGuid();

            user.PwdKey = guidKey.ToString();

            int result = _dbcontext.SaveChanges();

            if (result > 0)
            {
                var message = $"<h3>Merhaba, Şifre yenileme isteğiniz alınmıştır.</h3></br>" +
                    $"<h3><a href=http://localhost:5022/account/ResetPassword/{guidKey}>Şifrenizi yenilemek için tıklayınız.</a></h3>";

                await _mailRepository.SendMailAsync(model.Email, "Şifre yenileme isteği", message);

                ViewBag.EmailSent = AlertService.ShowAlert(AlertStates.Success, "Sana bir şifre yenileme linki gönderdik.E postanı kontrol edip şifreni yenileyebilirsin.");
            }

            return View(model);
        }

        [Route("Account/ResetPassword/{pwdGuid}")]
        public IActionResult ResetPassword(Guid pwdGuid)
        {
            var users = _dbcontext.Users.FirstOrDefault(x => x.PwdKey == pwdGuid.ToString() && x.PwdKey != null);

            if (users == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("Account/ResetPassword/{pwdGuid}")]
        public IActionResult ResetPassword(Guid pwdGuid, ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _dbcontext.Users.FirstOrDefault(x => x.PwdKey == pwdGuid.ToString());

            if (user != null)
            {
                user.Password = model.Password;
                user.PwdKey = null;
                int sonuc = _dbcontext.SaveChanges();
                if (sonuc <= 0)
                {
                    ViewBag.Warning = AlertService.ShowAlert(AlertStates.Danger, "Şifre yenileme esnasında bir hata oluştu lütfen daha sonra tekrar deneyiniz.");
                    return View(model);
                }
            }
            TempData["RedirectFromReset"] = AlertService.ShowAlert(AlertStates.Success, "Şifre yenileme işleminiz başarılı.Devam etmek için lütfen giriş yapınız.");
            return RedirectToAction(nameof(Login));

        }

    }
}
