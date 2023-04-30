using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.UI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Name alanı zorunludur.")]
        [StringLength(30,ErrorMessage ="En fazla 30 karakter girilebilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname alanı zorunludur.")]
        [StringLength(50,ErrorMessage = "En fazla 50 karakter girilebilir.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "EMail alanı zorunludur.")]
        [StringLength(60, ErrorMessage = "En fazla 60 karakter girilebilir.")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Phone alanı zorunludur.")]
        [StringLength(20, ErrorMessage = "En fazla 20 karakter girilebilir.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "City alanı zorunludur.")]
        [StringLength(60, ErrorMessage = "En fazla 60 karakter girilebilir.")]

        public string City { get; set; }

        [Required(ErrorMessage = "Country alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter girilebilir.")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Adress alanı zorunludur.")]
        [StringLength(200, ErrorMessage = "En fazla 200 karakter girilebilir.")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Password alanı zorunludur.")]
        [StringLength(12, ErrorMessage = "En fazla 12 karakter girilebilir.")]
        public string Password { get; set; }


        [Required(ErrorMessage ="PasswordConfirm alanı zorunludur.")]
        [StringLength(12, ErrorMessage = "En fazla 12 karakter girilebilir.")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }


        [DisplayName("Profile Picture")]
        public IFormFile Picture { get; set; }
        public string ImageString { get; set; }
        public byte[] ImageByteArray { get; set; }
    }
}
