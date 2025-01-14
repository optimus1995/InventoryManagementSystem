using ApplicationCore.UseCases.Products.CreateProducts;
using ApplicationCore.UseCases.Products.ReadProducts;
using ApplicationCore.UseCases.Products.UpdateProducts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiProductController : ControllerBase
    {

        private IStringLocalizer<ProductsController> _stringLocalizer;

        private readonly IMediator _mediator;
        private readonly IValidator<CreateProductsRequest> _validator;
        private readonly IValidator<UpdateProductsRequest> _updatevalidator;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public WebApiProductController(IMediator mediator, IStringLocalizer<ProductsController> stringLocalizer,
            IValidator<CreateProductsRequest> validator, IValidator<UpdateProductsRequest> updatevalidator,
            IWebHostEnvironment hostingEnvironment)
        {
            _stringLocalizer = stringLocalizer;

            _mediator = mediator;
            _validator = validator;
            _updatevalidator = updatevalidator;
            _hostingEnvironment = hostingEnvironment;
        }

        //done

        [Route("ApiController/Result")]
        [HttpGet]
        public async Task<IActionResult> Result(int catid, CancellationToken cancellationToken)
        {
            try
            {
                var request = new ReadProductsRequest();
                request.catid = catid;
                var s = await _mediator.Send(request, cancellationToken);
              return Ok(s);


                //return View(viewModel);




            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }




    }
}
