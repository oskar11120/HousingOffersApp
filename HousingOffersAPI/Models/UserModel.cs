﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Models
{
    public class UserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Dictionary<string, string> ContactInfos { get; set; }
    }
}
