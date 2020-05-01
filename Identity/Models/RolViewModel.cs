using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class RolViewModel
    {
        [Required(ErrorMessage ="Ad Alanı Gereklidr")]
        [Display(Name="Ad :")]
        public string Name { get; set; }
        
    }
}
