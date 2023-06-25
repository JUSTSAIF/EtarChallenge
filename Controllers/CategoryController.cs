using System.Security.Claims;
using EtarChallenge.Dto.Category;
using EtarChallenge.Services.CategoriesService;
using EtarChallenge.Validations.Category;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EtarChallenge.Controllers
{
    [ApiController, Authorize]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext DataContext;
        private ICategoriesService CategoryService;
        private IValidator<CategoryDto> CategoryValidation;
        public CategoryController(DataContext dataContext, ICategoriesService _categoryService, IValidator<CategoryDto> _CategoryValidation)
        {
            DataContext = dataContext;
            CategoryService = _categoryService;
            CategoryValidation = _CategoryValidation;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResDto?>> Get([FromRoute] int id)
        {
            var validator = new CategoryValidation(DataContext);
            ValidationResult result = await validator.ValidateAsync(new CategoryDto { id = id }, options =>
            {
                options.IncludeProperties(x => x.id);
            });

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return await CategoryService.Index(id);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResDto?>> Create([FromBody] CategoryDto data)
        {
            var validator = new CategoryValidation(DataContext);
            ValidationResult result = await validator.ValidateAsync(data, options =>
            {
                options.IncludeProperties(x => x.name);
                options.IncludeProperties(x => x.description);
            });


            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var res = await CategoryService.Create(
                data.name,
                data.description,
                Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)
            );

            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CategoryDto data)
        {
            ValidationResult result = await CategoryValidation.ValidateAsync(data);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await CategoryService.Update(data.id, data.name, data.description);

            return Ok(new
            {
                Status = "UPDATED_SUCCESS"
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var validator = new CategoryValidation(DataContext);
            ValidationResult result = await validator.ValidateAsync(new CategoryDto { id = id }, options =>
            {
                options.IncludeProperties(x => x.id);
            });

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            
            await CategoryService.Delete(id);

            return Ok(new
            {
                Status = "DELETED_SUCCESS"
            });
        }
    }
}