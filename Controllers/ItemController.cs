using System.Security.Claims;
using EtarChallenge.Dto.Item;
using EtarChallenge.Services.ItemsService;
using EtarChallenge.Validations.Item;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EtarChallenge.Controllers
{
    [ApiController, Authorize]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly DataContext DataContext;
        private IValidator<ItemDto> ItamValidator;
        private IItemsService ItemsService;
        public ItemController(DataContext _dataContext, IValidator<ItemDto> _ItamValidator, IItemsService _itemsService)
        {
            ItamValidator = _ItamValidator;
            ItemsService = _itemsService;
            DataContext = _dataContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemResDto?>> Get([FromRoute] int id)
        {
            var validator = new ItemValidation(DataContext);
            ValidationResult result = await validator.ValidateAsync(new ItemDto { id = id }, options =>
            {
                options.IncludeProperties(x => x.id);
            });

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return await ItemsService.Index(id);
        }

        [HttpPost]
        public async Task<ActionResult<ItemResDto?>> Create([FromBody] ItemDto data)
        {
            var validator = new ItemValidation(DataContext);
            ValidationResult result = await validator.ValidateAsync(data, options =>
            {
                options.IncludeProperties(x => x.name);
                options.IncludeProperties(x => x.description);
                options.IncludeProperties(x => x.price);
                options.IncludeProperties(x => x.catId);
            });

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var res = await ItemsService.Create(
                data.name,
                data.description,
                data.price,
                data.catId,
                Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!)
            );

            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ItemDto data)
        {
            ValidationResult result = await ItamValidator.ValidateAsync(data);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await ItemsService.Update(
                data.id,
                data.name,
                data.description,
                data.price,
                data.catId
            );

            return Ok(new
            {
                Status = "UPDATED_SUCCESS"
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var validator = new ItemValidation(DataContext);
            ValidationResult result = await validator.ValidateAsync(new ItemDto { id = id }, options =>
            {
                options.IncludeProperties(x => x.id);
            });

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await ItemsService.Delete(id);

            return Ok(new
            {
                Status = "DELETED_SUCCESS"
            });
        }

    }
}