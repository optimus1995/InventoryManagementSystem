using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using ApplicationCore.UseCases.Category.CreateCategory;
using MediatR;
using ApplicationCore.UseCases.Category.UpdateCategory;
using ApplicationCore.UseCases.Category.ReadCategory;
using ApplicationCore.UseCases.Category.DeleteCategory;
using ApplicationCore.UseCases.Category.FetchCategory;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
namespace InventoryManagementSystem.Controllers
{
    [Authorize(Roles = "ADMIN,Admin,  SUPERADMIN, SuperAdmin")]

    public class CategoryController : Controller
    {
        public readonly ILogger<CategoryController> logger;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateCategoryRequest> _validator;
        private readonly IValidator<UpdateCategoryRequest> _validatorUpdate;
        public CategoryController(ILogger<CategoryController> _log, IMediator mediator,
            IValidator<CreateCategoryRequest> validator, IValidator<UpdateCategoryRequest> validatorUpdate)
        {
            logger = _log;
            _mediator = mediator;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorUpdate = validatorUpdate;

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
    // Validate the command using FluentValidation
    var result = await _validator.ValidateAsync(command);

            // If validation fails, add the errors to ModelState
            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(command); // Return the view with the command object to display errors
            }


    try
    {
        // Proceed with the command if the model state is valid
        if (ModelState.IsValid)
        {
            command.IsActive = 1;
            var response = await _mediator.Send(command, cancellation);
            return RedirectToAction("Result");
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }

    // If the ModelState is invalid after validation, return BadRequest
    return BadRequest(ModelState);
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


        public async  Task<IActionResult> Edit(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var result =await  _validatorUpdate.ValidateAsync(request);

            // If validation fails, add the errors to ModelState
            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(request); // Return the view with the command object to display errors
            }



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
