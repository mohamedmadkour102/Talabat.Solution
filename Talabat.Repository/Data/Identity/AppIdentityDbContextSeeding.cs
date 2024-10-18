using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Data.Identity
{
    public static class AppIdentityDbContextSeeding
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Mohamed Madkour",
                    Email = "Mohamed.madkour@gmail.com",
                    UserName = "MohamedMA22",
                    PhoneNumber = "01122334455"
                    
                };
                await _userManager.CreateAsync(user , "Pa$$W0rd");
            }

        }
    }
}
