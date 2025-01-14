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
using Microsoft.Extensions.DependencyInjection;
//using InventoryManagementSystem.CustomMiddleware;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Localization
        // Localization setup (same as before)
        builder.Services.AddTransient<LanguageServices>();
        builder.Services.AddTransient<IStringLocalizer, StringLocalizer<SharedResource>>();
        builder.Services.AddScoped<IStringLocalizer<HomeController>, StringLocalizer<HomeController>>();

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

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("fr-FR"),
                new CultureInfo("en-US"),
                new CultureInfo("ur-UR")
            };

            options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
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

        // Repositories
        builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
        builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
        builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IEmployeesRepository, EmployeeRepository>();
        builder.Services.AddSingleton<IJwtTokenServices, JwtServices>();

        // JWT Service Setup
   //  builder.Services.AddSingleton<IJwtTokenServices>(new JwtService(builder.Configuration["Jwt:SecretKey"])); // Assuming Jwt:SecretKey is stored in appsettings.json

        // Serilog Setup
        builder.Services.AddSerilog();
        builder.Host.UseSerilog();

        // Identity Setup
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Google Authentication Setup
        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "YOUR_GOOGLE_CLIENT_ID";
                options.ClientSecret = "YOUR_GOOGLE_CLIENT_SECRET";
            });

        var app = builder.Build();

        // Serilog Logger Configuration
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

        // Use Static Files, Routing, and Authentication
        app.UseStaticFiles();
        app.UseRouting();

        // Register custom JWT validation middleware (uncommented)
  //      app.UseMiddleware<JwtValidationMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
