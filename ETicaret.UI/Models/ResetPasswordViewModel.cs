using System.ComponentModel.DataAnnotations;

namespace ETicaret.UI.Models
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Password alanı zorunludur.")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "En az 4, en fazla 12 karakter girilebilir.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password alanı zorunludur.")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "En az 4, en fazla 12 karakter girilebilir.")]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil.")]
        public string ConfirmPassword { get; set; }
    }
}
