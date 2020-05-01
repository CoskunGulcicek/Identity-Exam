﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Contexts
{
    public class AppUser:IdentityUser<int>
    {
        public string PictureUrl { get; set; }
        public string Gender { get; set; }
    }
}
