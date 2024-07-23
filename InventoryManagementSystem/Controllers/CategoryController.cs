using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using ApplicationCore.DapperEntity;
using ApplicationCore.Contract;

namespace InventoryManagementSystem.Controllers
{
    [Authorize(Roles ="Administrator") ]

    public class CategoryController : Controller
    {  private readonly ICategoryRepository _categoryRepository;
        public readonly ILogger<CategoryController> logger;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> _log)
        {
            _categoryRepository = categoryRepository;
            logger = _log;
        }

        [Route("Category/Result")]
        [HttpGet]
        public async Task<IActionResult> Result()
        {
            try
            {
            //    logger.LogInformation("Category/Result endpoint visited at {DT}", DateTime.UtcNow);
            //    logger.LogInformation("About page visited at {DT}",
            //DateTime.UtcNow.ToLongTimeString());
                var records = await _categoryRepository.GetAll();
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
        public IActionResult Save(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Category = new Category
                    {
                        Name = category.Name
                        
                    };
                    _categoryRepository.Create(Category);
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
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetrecordforUpdate(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        public IActionResult Edit(Category category)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var Category = new Category
                    {
                        Id = category.Id,
                        Name = category.Name,
                       
                    };
                    _categoryRepository.Update(Category);
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

        public IActionResult Delete(int id)
        {
            _categoryRepository.DeleteRecord(id);
            return RedirectToAction("Result");

        }

    }
}
