using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserSignUpViewModel
    {
        //önerilmeyen kullanım şeklidir sadece örnek açısından yapılmıştır
        [Display(Name="Kullanıcı Adı:")]
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez")]
        public string UserName { get; set; }


        [Display(Name = "Şifre:")]
        [Required(ErrorMessage = "Parola boş geçilemez")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar:")]
        [Compare("Password",ErrorMessage = "Paralolar eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Ad:")]
        [Required(ErrorMessage = "Ad Adı Giriniz")]
        public string Name { get; set; }

        [Display(Name = "Soyad:")]
        [Required(ErrorMessage = "Soyad Adı Giriniz")]
        public string SurName { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email Adı Giriniz")]
        public string Email { get; set; }
    }
}
