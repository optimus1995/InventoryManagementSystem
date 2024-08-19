//using Infrastructure.Data;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.EntityFrameworkCore;
//using Serilog;
//using Serilog.Core;
//using ApplicationCore.Contract;
//using ApplicationCore.Context;
//using Infrastructure.Repository;
//using Infrastructure.Services;
//using Microsoft.CodeAnalysis.Options;
//using Microsoft.AspNetCore.Localization;
//using System.Globalization;
//using Microsoft.AspNetCore.Mvc.Razor;
//using Microsoft.Extensions.Localization;
//using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
//using Microsoft.Extensions.Options;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddTransient<IEmailSender, EmailServices>();

//builder.Services.AddRazorPages();

//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new[]
//    {
//        new CultureInfo("en-US"),
//        new CultureInfo("fr"),
//    };

//    options.DefaultRequestCulture = new RequestCulture("fr");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;

//    // Remove existing providers and add QueryStringRequestCultureProvider
//    options.RequestCultureProviders.Clear();
//    options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
//});

//builder.Services.AddControllersWithViews()
//    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
//    .AddDataAnnotationsLocalization();


//// Enable localization middleware














////builder.Services.AddLocalization(options =>
////{
////    options.ResourcesPath = "Resources";
////});

////builder.Services.Configure<RequestLocalizationOptions>(options =>
////{
////    //var supportedCultures = new[]
////    //{ new CultureInfo( "fr"),
////    //    new CultureInfo("en-US")

////    //};
////    //options.DefaultRequestCulture = new RequestCulture("fr");
////    //options.SupportedCultures= supportedCultures;

////}



////);




// builder.Services.Configure<RequestLocalizationOptions>(options =>
//            {
//                options.SetDefaultCulture("de-DE");
//options.AddSupportedUICultures("en-US", "de-DE");
//options.FallBackToParentUICultures = true;
//options.RequestCultureProviders.Clear();
//            });

////builder.Services.AddControllersWithViews()
////    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
////    .AddDataAnnotationsLocalization();



////builder.Services.AddScoped<Serilog.ILogger ,Logger>();

//builder.Services.AddSingleton<IEmailSender, EmailServices>();
//builder.Services.AddSingleton<DapperContext>();
////builder.Services.AddSingleton<Log.Log......ger>();

//builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();  
//builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
//builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
////0builder.Services.AddScoped<IProductsRepository, ProductsRepository>(); 
////builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
////    .AddEntityFrameworkStores<ApplicationDbContext>();
////builder.Services.AddControllersWithViews()
////    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix) ;

//builder.Services.AddSerilog();
//builder.Host.UseSerilog();
//builder.Services.AddDefaultIdentity<IdentityUser>(option=> option .SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();
////builder.Services.AddRazorPages()
////    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
////    .AddViewLocalization()
////    .AddDataAnnotationsLocalization(options => 
////    options.DataAnnotationLocalizerProvider =(type, factory) 

////    )
////    ;


//////builder.Services.AddControllersWithViews().AddViewLocalization();
////builder.Services.AddLocalization(options =>
////{
////    var supportedCultures = new[]
////    {
////new CultureInfo("en-US"),
////new CultureInfo ("ur-PK"),
////new CultureInfo ("en-UK")
////};
//////var localizationOptions - new RequestLocalizationOptions
//////builder.Services.Configure<RequestLocalizationOptions>(options =>
//////options.DefaultRequestCulture = new RequestCulture("en"),
//////options.SupportedCulture
////    }
////); 




//builder.Services.AddAuthentication()
//.AddGoogle(options =>
//{
//    options.ClientId = "989657770936-sqru9cdhum2bcsorj11l844r3naj8par.apps.googleusercontent.com";


//    options.ClientSecret = "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";
//});
//var app = builder.Build();
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .CreateBootstrapLogger();
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
//app.UseRequestLocalization(localizationOptions);


////localizationOptions
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

//app.Run();
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
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.DependencyInjection;
using ApplicationCore.UseCases.Category.Create;
using MediatR;

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
        //   builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        //. 
        builder.Services.AddRazorPages();
        //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryHandler).Assembly));

        //        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        //    Assembly.GetExecutingAssembly(),
        //    Assembly.Load("ApplicationCore") 
        // //   Assembly.Load("Infrastructure")   // Additional assemblies if needed
        //));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        
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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Enable localization middleware
        var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
        app.UseRequestLocalization(localizationOptions);

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
       // app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}

#region Localization

#endregion
