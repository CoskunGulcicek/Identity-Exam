using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserSignInViewModel
    {
        //önerilmeyen kullanım şeklidir sadece örnek açısından yapılmıştır
        [Display(Name = "Kullanıcı Adı :")]
        [Required(ErrorMessage ="Kullanıcı Adı Giriniz")]
        public string UserName { get; set; }
        [Display(Name = "Şifre :")]
        [Required(ErrorMessage = "Şifre Giriniz")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
