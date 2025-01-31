//using ApplicationCore.DapperEntity;
//using ApplicationCore.UseCases.Products.CreateProducts;
//using ApplicationCore.UseCases.Products.ReadProducts;
//using ApplicationCore.UseCases.Products.UpdateProducts;
//using FluentValidation;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Localization;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace InventoryManagementSystem.Controllers
//{
//    public class ApiController : Controller
//    {





//        private IStringLocalizer<ProductsController> _stringLocalizer;

//        private readonly IMediator _mediator;
//        private readonly IValidator<CreateProductsRequest> _validator;
//        private readonly IValidator<UpdateProductsRequest> _updatevalidator;
//        private readonly IWebHostEnvironment _hostingEnvironment; 
//        private readonly IConfiguration _configuration;
//        public ApiController(IMediator mediator, IStringLocalizer<ProductsController> stringLocalizer,
//            IValidator<CreateProductsRequest> validator, IValidator<UpdateProductsRequest> updatevalidator,
//            IWebHostEnvironment hostingEnvironment,IConfiguration configuration )
//        {
//            _stringLocalizer = stringLocalizer;
//            _mediator = mediator;
//            _validator = validator;
//            _updatevalidator = updatevalidator;
//            _hostingEnvironment = hostingEnvironment;
//            _configuration = configuration;
//        }


//        [Route("ApiController/Result")]
//        [HttpGet]
//        [Authorize]
//        public async Task<IActionResult> Result(int catid, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var request = new ReadProductsRequest();
//                request.catid = catid;
//                var s = await _mediator.Send(request, cancellationToken);
//                ViewBag.SelectedCategoryId = catid;

//                return Json(s);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, "Internal server error");
//            }
//        }

     

//        [HttpPost("token")]
//        public IActionResult GenerateToken([FromBody] UserCredentials credentials)
//        {
//            // Validate the user credentials (hardcoded for simplicity)
//            if (credentials.Username == "user" && credentials.Password == "password")
//            {
//                var token = GenerateJwtToken(credentials.Username);
//                return Ok(new { Token = token });
//            }

//            return Unauthorized("Invalid credentials");
//        }

//        private string GenerateJwtToken(string username)
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//            new Claim(JwtRegisteredClaimNames.Sub, username),
//            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//            new Claim(JwtRegisteredClaimNames.Name)
//        };

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddHours(1),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
   

//    public class UserCredentials
//    {
//        public string Username { get; set; }
//        public string Password { get; set; }
//    }


//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
