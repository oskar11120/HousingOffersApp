using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Models
{
    public class UserUpdateRequestContentModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string LoginNew { get; set; }
        public string PasswordNew { get; set; }
        public string PhoneNumberNew { get; set; }
    }
}
