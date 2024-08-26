using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using ApplicationCore.UseCases.Category.CreateCategory;
using MediatR;
using ApplicationCore.UseCases.Category.UpdateCategory;
using ApplicationCore.UseCases.Category.ReadCategory;
using ApplicationCore.UseCases.Category.DeleteCategory;
using ApplicationCore.UseCases.Category.FetchCategory;
namespace InventoryManagementSystem.Controllers
{
    [Authorize(Roles = "ADMIN,Admin,  SUPERADMIN, SuperAdmin")]

    public class CategoryController : Controller
    {
        public readonly ILogger<CategoryController> logger;
        private readonly IMediator _mediator;

        public CategoryController(ILogger<CategoryController> _log, IMediator mediator)
        {
            logger = _log;
            _mediator = mediator;
        }

        [Route("Category/Result")]
        [HttpGet]
        //done
        public async Task<IActionResult> Result()
        {
            try
            {
                var request = new ReadCategoryRequest();
                var cancellationToken = new CancellationToken();

                var records = await _mediator.Send(request, cancellationToken);

                // Pass the list of records to the view
                return View(records);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured");
                return StatusCode(500, "Internal server error");
            }
        }
        [Route("Category/Save")]
        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        //done
        public async Task<IActionResult> Save(CreateCategoryRequest command, CancellationToken cancellation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    command.IsActive = 1;
                    var response = await _mediator.Send(command, cancellation);
                    return RedirectToAction("Result");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(FetchCategoryRequest request, CancellationToken cancellationToken)
        {
            //var request = new FetchCategoryRequest();
            //request.Id = id;
            var response = await _mediator.Send(request, cancellationToken);
            //     var category = await _categoryRepository.GetrecordforUpdate(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }


        public IActionResult Edit(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    //  _categoryRepository.Update(Category);
                    _mediator.Send(request, cancellationToken);
                    return RedirectToAction("Result");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        //done
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteCategoryRequest();
            request.Id = id;
            //        var request = new DeleteCategoryRequest { Id = id };
            await _mediator.Send(request, cancellationToken);

            return RedirectToAction("Result");
        }

    }
}
