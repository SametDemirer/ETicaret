
using System.ComponentModel.DataAnnotations;

namespace ETicaret.UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "EMail alanı zorunludur.")]
        [StringLength(60, ErrorMessage = "En fazla 60 karakter girilebilir.")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Password alanı zorunludur.")]
        [StringLength(12, ErrorMessage = "En fazla 12 karakter girilebilir.")]
        public string Password { get; set; }
    }
}
