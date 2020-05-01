using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserUpdateViewModel
    {

        [Display(Name="Email :")]
        [Required(ErrorMessage = "Email Alanı Gereklidir")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir mail adresi giriniz")]
        public string Email { get; set; }

        [Display(Name = "Telefon :")]
        public string PhoneNumber { get; set; }

        public string PictureUrl { get; set; }
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = "İsim alanı Gereklidir")]
        [Display(Name = "İsim :")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim alanı Gereklidir")]
        [Display(Name = "Soyisim :")]
        public string SurName { get; set; }

    }
}
