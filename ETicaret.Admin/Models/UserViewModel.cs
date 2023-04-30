using ETicaret.Data.Enums;
using Microsoft.Build.Graph;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.Admin.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Role { get; set; }
        public IFormFile Picture { get; set; }
        public string ImageString { get; set; }
        public byte[] ImageByteArray { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword",ErrorMessage ="Şifreler aynı değil !")]
        public string ConfirmNewPassword { get; set; }






    }
}
