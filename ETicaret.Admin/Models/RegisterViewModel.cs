using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ETicaret.Admin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "En fazla 100 karakter girilebilir.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "EMail alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
        [EmailAddress(ErrorMessage = "Lütfen ornek@gmail.com şelinde giriş yapınız.")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Password alanı zorunludur.")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "En az 4, en fazla 12 karakter girilebilir.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password alanı zorunludur.")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "En az 4, en fazla 12 karakter girilebilir.")]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil !")]
        public string ConfirmPassword { get; set; }

       [DisplayName("Profile Picture")]
        public IFormFile Picture { get; set; }
        public string ImageString { get; set; }
        public byte[] ImageByteArray { get; set; }

    }
}
