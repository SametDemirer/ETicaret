using System.ComponentModel.DataAnnotations;

namespace ETicaret.Admin.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Lütfen ornek@mail.com şeklinde giriniz.")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
        public string Email { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "En az 4,en fazla 12 karakter girilebilir.")]
        public string Password { get; set; }
    }
}
