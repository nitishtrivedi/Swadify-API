using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swadify_API.DTOs;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;
using System.Security.Claims;

namespace Swadify_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service) => _service = service;

        /// <summary>Get menu by restaurant</summary>
        [HttpGet("restaurant/{restaurantId:int}")]
        public async Task<IActionResult> GetByRestaurant(int restaurantId,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 50,
            [FromQuery] int? categoryId = null, [FromQuery] bool? isVeg = null)
        {
            var result = await _service.GetMenuByRestaurantAsync(restaurantId, page, pageSize, categoryId, isVeg);
            return Ok(result);
        }

        /// <summary>Get menu item by ID</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetMenuItemByIdAsync(id);
            if (result == null) return NotFound(ApiResponse<MenuItemResponseDto>.NotFound());
            return Ok(ApiResponse<MenuItemResponseDto>.Ok(result));
        }

        /// <summary>Get all menu categories</summary>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _service.GetMenuCategoriesAsync();
            return Ok(ApiResponse<List<MenuCategoryDto>>.Ok(result));
        }

        /// <summary>Create a new menu item</summary>
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateMenuItemDto dto)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.CreateMenuItemAsync(requesterId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<MenuItemResponseDto>.Created(result));
        }

        /// <summary>Update a menu item</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuItemDto dto)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.UpdateMenuItemAsync(id, requesterId, dto);
            return Ok(ApiResponse<MenuItemResponseDto>.Ok(result, "Menu item updated."));
        }

        /// <summary>Delete a menu item</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _service.DeleteMenuItemAsync(id, requesterId);
            return Ok(ApiResponse<object>.Ok(null!, "Menu item deleted."));
        }

        /// <summary>Upload menu item image</summary>
        [HttpPost("{id:int}/image")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _service.UploadMenuItemImageAsync(id, requesterId, file);
            return Ok(ApiResponse<object>.Ok(null!, "Image uploaded."));
        }
    }
}
