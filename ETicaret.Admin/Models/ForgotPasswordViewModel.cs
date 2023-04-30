using System.ComponentModel.DataAnnotations;

namespace ETicaret.Admin.Models
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Lürfen ornek@mail.com şeklinde giriniz.")]
        public string Email { get; set; }
    }
}
