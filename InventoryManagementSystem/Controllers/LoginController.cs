using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DocumentFormat.OpenXml.Spreadsheet;


namespace InventoryManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginController(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            _roleManager = roleManager;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        // [HttpGet]
        // public IActionResult Login()
        // {
        //     var redirectUrl = Url.Action("GoogleResponse", "Login");
        //     var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        //     return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        // }
        //[Route("signin-google")]

        // public async Task<IActionResult> GoogleResponse()
        // {
        //     var info = await _signInManager.GetExternalLoginInfoAsync();
        //     try
        //     {
        //         if (info == null)

        //         {
        //             return RedirectToAction(nameof(Login));
        //         }

        //         var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        //         if (result.Succeeded)
        //         {
        //             return RedirectToAction("Index", "Home");
        //         }

        //         var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //         var user = await _userManager.FindByEmailAsync(email);

        //         if (user == null)
        //         {
        //             user = new IdentityUser
        //             {
        //                 UserName = email,
        //                 Email = email
        //             };

        //             var createResult = await _userManager.CreateAsync(user);
        //             if (createResult.Succeeded)
        //             {
        //                 await _userManager.AddLoginAsync(user, info);
        //             }
        //         }
        //         else
        //         {
        //             var userLogins = await _userManager.GetLoginsAsync(user);
        //             if (userLogins.All(login => login.LoginProvider != info.LoginProvider || login.ProviderKey != info.ProviderKey))
        //             {
        //                 await _userManager.AddLoginAsync(user, info);
        //             }
        //         }

        //         await _signInManager.SignInAsync(user, isPersistent: false);
        //     }
        //     catch (Exception ex) { 
        //     Console.WriteLine(ex);  
        //     }
        //     return RedirectToAction("Index", "Home");
        // }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? ReturnUrl = null)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }





        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(action: "ExternalLoginCallback", controller: "Login", values: new { ReturnUrl = returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl, string? remoteError)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                     user = new IdentityUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            
                     };

                        await userManager.CreateAsync(user);
                    }

                    await userManager.AddLoginAsync(user, info);

                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on info@dotnettutorials.net";

                return View("Error");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> EditRole(string roleId)
        //{
        //    //First Get the role information from the database
        //    IdentityRole? role = await _roleManager.FindByIdAsync(roleId);
        //    if (role == null)
        //    {
        //        // Handle the scenario when the role is not found
        //        return View("Error");
        //    }

        //    //Populate the EditRoleViewModel from the data retrived from the database
        //    var model = new EditRoleViewModel
        //    {
        //        Id = role.Id,
        //        RoleName = role.Name,
        //        Description = role.Description,
        //        Users = new List<string>()
        //        // You can add other properties here if needed
        //    };

        //    // Retrieve all the Users
        //    foreach (var user in _userManager.Users.ToList())
        //    {
        //        // If the user is in this role, add the username to
        //        // Users property of EditRoleViewModel. 
        //        // This model object is then passed to the view for display
        //        if (await _userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            model.Users.Add(user.UserName);
        //        }
        //    }

        //    return View(model);
        //}




    }
}

