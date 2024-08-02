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
using Microsoft.CodeAnalysis.Options;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IEmailSender, EmailServices>();

//builder.Services.AddSingleton<LanguageServices>();
builder.Services.AddRazorPages();


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


var supportedCultures = new[] { "ar" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedUICultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);





//builder.Services.AddScoped<Serilog.ILogger ,Logger>();

builder.Services.AddSingleton<IEmailSender, EmailServices>();
builder.Services.AddSingleton<DapperContext>();
//builder.Services.AddSingleton<Log.Log......ger>();

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();  
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//0builder.Services.AddScoped<IProductsRepository, ProductsRepository>(); 
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddSerilog();
builder.Host.UseSerilog();
builder.Services.AddDefaultIdentity<IdentityUser>(option=> option .SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddRazorPages()
//    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
//    .AddViewLocalization()
//    .AddDataAnnotationsLocalization(options => 
//    options.DataAnnotationLocalizerProvider =(type, factory) 
    
//    )
//    ;


////builder.Services.AddControllersWithViews().AddViewLocalization();
//builder.Services.AddLocalization(options =>
//{
//    var supportedCultures = new[]
//    {
//new CultureInfo("en-US"),
//new CultureInfo ("ur-PK"),
//new CultureInfo ("en-UK")
//};
////var localizationOptions - new RequestLocalizationOptions
////builder.Services.Configure<RequestLocalizationOptions>(options =>
////options.DefaultRequestCulture = new RequestCulture("en"),
////options.SupportedCulture
//    }
//); 



 
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
