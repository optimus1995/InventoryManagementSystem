using InventoryManagementSystem.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using ApplicationCore.Contract;
using AutoMapper;
using NUglify.Helpers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokenServices _jwtTokenServices;



        public UsersAPIController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor, IJwtTokenServices jwtTokenServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor= httpContextAccessor;
            _jwtTokenServices= jwtTokenServices;


        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register( UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                return Ok(new { Message = "Registration successful" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login( UserLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false); 

            

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var userid = user?.Id;

                // Ensure the user is authenticated (if using JWT or cookies)
                var userData = new
                {
                     LockedUser = user.LockoutEnd,
                    UserId = user.Id,
                 //   UserName = user?.UserName,
                    Email = user?.Email,
                    // Add other fields as needed
                };
                var token = _jwtTokenServices.GenerateToken(userData.UserId, userData.Email);

                return Ok(new { Message = "Login successful", token });

            }
            if (result.IsLockedOut)
                return Ok(new { Message = " User Is Locked" });

            return Unauthorized(new { Message = "Invalid login attempt" });
        }
        // POST: api/Account/Login
        [HttpPost("TokenVerification")]
        public async Task<IActionResult> TokenVerification([FromBody] TokenModel token)
        {
        

            var result = _jwtTokenServices.ValidateToken(token.Token);
            if (result==true)

            {
                return Ok(new { Message = "Token is valid" });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid token" });
            }
        }
    }
}
public class TokenModel
{

    public string Token { get; set; }
}