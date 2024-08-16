using ApplicationCore.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;
using ApplicationCore.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.IdentityUser;

namespace Infrastructure.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
            
        {
            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var user = new ApplicationUser();

                user.UserName = "Usamaishtiaq85@gmail.com";
                user.Email = "Usamaishtiaq85@gmail.com";


                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;

                if (userManager.Users.All(x => x.Id != user.Id))
                {
                    var result = await userManager.FindByEmailAsync(user.Email);

                    if (result == null)
                    {
                        await userManager.CreateAsync(user, "Asphalt8");
                        await userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                       
                    }
                }
            }
            catch (Exception ex)
            { 
             Console.WriteLine(ex.ToString());
            }
        }
    }
}
