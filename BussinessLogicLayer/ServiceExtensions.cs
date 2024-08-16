//using ApplicationCore.Context;
//using ApplicationCore.IdentityModels;
//using ApplicationCore.IdentityUser;
//using Infrastructure.Data;
//using Infrastructure.Seeds;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Persistance.Seeds;
//using Microsoft.EntityFrameworkCore;
//namespace Infrastructure
//{
//    public static class ServiceExtensions
//    {
//        public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
//        {
//            //register services
//            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
//                configuration.GetConnectionString("DefaultConnection")
//            ));

//            services
//                .AddIdentityCore<ApplicationUser>()
//                .AddRoles<ApplicationRole>()
//                .AddEntityFrameworkStores<ApplicationDbContext>();


//            services.AddScoped<ApplicationDbContext>();

//            //Seeds roles and users
//            DefaultRoles.SeedRolesAsync(services.BuildServiceProvider()).Wait();
//            DefaultUsers.SeedUsersAsync(services.BuildServiceProvider()).Wait();
//        }



//    }
//}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.IdentityUser;
using ApplicationCore.IdentityModels;
using Infrastructure.Data;
using Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;
using Persistance.Seeds;

namespace Infrastructure
{
    //public static class ServiceExtensions
    //{
    //    public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
    //    {
    //        // Register the ApplicationDbContext with the correct options
    //        services.AddDbContext<ApplicationDbContext>(options =>
    //            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    //        // Configure Identity with Guid as the key type
    //        services
    //            .AddIdentityCore<ApplicationUser>()
    //            .AddRoles<ApplicationRole>()
    //            .AddEntityFrameworkStores<ApplicationDbContext>()
    //            .AddDefaultTokenProviders();

    //        services.AddScoped<ApplicationDbContext>();

    //        // Seed roles and users (if applicable)
    // //       DefaultRoles.SeedRolesAsync(services.BuildServiceProvider()).Wait();
    //        DefaultUsers.SeedUsersAsync(services.BuildServiceProvider()).Wait();
    //    }
    //}
}
