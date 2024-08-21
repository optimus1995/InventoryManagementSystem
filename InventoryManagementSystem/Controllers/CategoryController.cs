using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using ApplicationCore.DapperEntity;
using ApplicationCore.Contract;
using ApplicationCore.UseCases.Category.Create;
using MediatR;
using ApplicationCore.UseCases.Category.Delete;
using ApplicationCore.UseCases.Category.Update;
using System.Threading;
using ApplicationCore.UseCases.Category.Read;

namespace InventoryManagementSystem.Controllers
{
    [Authorize(Roles = "ADMIN,Admin,  SUPERADMIN, SuperAdmin")]

    public class CategoryController : Controller
    {  private readonly ICategoryRepository _categoryRepository;
        public readonly ILogger<CategoryController> logger;
        private readonly IMediator _mediator;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> _log,IMediator mediator )
        {
            _categoryRepository = categoryRepository;
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
                Log.Information("Result", records);

            }
            catch (Exception ex)
            {
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
       

        [HttpPost]
        //done
        public async Task<IActionResult> Save(CreateCategoryRequest command,CancellationToken cancellation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    command.IsActive = 1;
                    var response = await _mediator.Send(command,cancellation);
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
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var request= new FetchCategoryRequest();
            request.Id= id;
           var response =  _mediator.Send(request, cancellationToken);
            var category = await _categoryRepository.GetrecordforUpdate(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        public IActionResult Edit(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                if (ModelState.IsValid)
                {
                   
                  //  _categoryRepository.Update(Category);
                    _mediator.Send(request,cancellationToken);
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

        public async Task<IActionResult> Delete(int id)
        {
    //        var request = new DeleteCategoryRequest { Id = id };
            await _mediator.Send(id);

            return RedirectToAction("Result");
            }

    }
}
