using System.ComponentModel.DataAnnotations;

namespace ETicaret.UI.Models
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Lürfen ornek@mail.com şeklinde giriniz.")]
        public string EMail { get; set; }
    }
}
