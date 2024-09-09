
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using ApplicationCore.Contract;
using ApplicationCore.Context;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Reflection;
using InventoryManagementSystem.Controllers;
using ApplicationCore.Mapping;
using MediatR;
using FluentValidation.AspNetCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        #region Localization
        //Step 1
        builder.Services.AddTransient< LanguageServices>();

        builder.Services.AddTransient<IStringLocalizer, StringLocalizer<SharedResource>>();
        builder.Services.AddScoped<IStringLocalizer<HomeController>, StringLocalizer<HomeController>>();

        //   builder.Services.AddScoped(IStringLocalizer<HomeController> , stringLocalizer>();
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        builder.Services.AddMvc()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                    return factory.Create("SharedResource", assemblyName.Name);
                };
            });

        builder.Services.Configure<RequestLocalizationOptions>(
            options =>
            {
                var supportedCultures = new List<CultureInfo>
                    {
                            new CultureInfo("fr-FR"),
                            new CultureInfo("en-US"),
                            new CultureInfo("ur-UR")
                    };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US" );
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });
        #endregion
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddTransient<IEmailSender, EmailServices>();
        builder.Services.AddRazorPages();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        builder.Services.AddFluentValidation(config =>
        {
            config.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
        builder.Services.AddSingleton<IEmailSender, EmailServices>();
        builder.Services.AddSingleton<DapperContext>();

        builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
        builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
        builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        builder.Services.AddScoped<IEmployeesRepository, EmployeeRepository>();

        builder.Services.AddSerilog();
        builder.Host.UseSerilog();
       
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "989657770936-sqru9cdhum2bcsorj11l844r3naj8par.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

        var app = builder.Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
        app.UseRequestLocalization(localizationOptions);

      //  app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}"); app.MapRazorPages();

        app.Run();
    }
}


#region Localization

#endregion
