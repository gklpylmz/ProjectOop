using System.ComponentModel.DataAnnotations;

namespace ProjectOop.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage ="Lütfen İsim Değeri Giriniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen Soyisim Değeri Giriniz")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Lütfen Mail Giriniz")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Değeri Giriniz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre Değeri Giriniz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen Şifreyi Tekrar Giriniz")]
        [Compare("Password", ErrorMessage ="Lütfen şifrelerin eşleştiğinden emin olun")]
        public string ConfirmPassword { get; set; }
    }
}
