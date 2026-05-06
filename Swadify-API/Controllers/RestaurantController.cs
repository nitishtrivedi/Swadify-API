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
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service) => _service = service;

        /// <summary>Get all active restaurants (with pagination, search, category filter)</summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<RestaurantResponseDto>), 200)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null, [FromQuery] int? categoryId = null, [FromQuery] bool? isOpen = null)
        {
            var result = await _service.GetRestaurantsAsync(page, pageSize, search, categoryId, isOpen);
            return Ok(result);
        }

        [HttpGet("featured")]
        [ProducesResponseType(typeof(ApiResponse<RestaurantResponseDto>), 200)]
        public async Task<IActionResult> GetFeatured()
        {
            var result = await _service.GetFeaturedRestaurantsAsync();
            return Ok(ApiResponse<List<RestaurantResponseDto>>.Ok(result));
        }

        /// <summary>Get restaurant by ID</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<RestaurantResponseDto>), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetRestaurantByIdAsync(id);
            if (result == null) return NotFound(ApiResponse<RestaurantResponseDto>.NotFound("Restaurant not found."));
            return Ok(ApiResponse<RestaurantResponseDto>.Ok(result));
        }

        /// <summary>Get all restaurant categories</summary>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _service.GetCategoriesAsync();
            return Ok(ApiResponse<List<RestaurantCategoryDto>>.Ok(result));
        }

        /// <summary>Get my restaurants (Admin only)</summary>
        //[HttpGet("my")]
        //[Authorize(Roles = "Admin,SuperAdmin")]
        //public async Task<IActionResult> GetMyRestaurants([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        //{
        //    var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        //    var result = await _service.GetMyRestaurantsAsync(ownerId, page, pageSize);
        //    return Ok(result);
        //}

        /// <summary>Create a new restaurant</summary>
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(typeof(ApiResponse<RestaurantResponseDto>), 201)]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
        {
            var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.CreateRestaurantAsync(ownerId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<RestaurantResponseDto>.Created(result));
        }

        /// <summary>Update restaurant details</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRestaurantDto dto)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.UpdateRestaurantAsync(id, requesterId, dto);
            return Ok(ApiResponse<RestaurantResponseDto>.Ok(result, "Restaurant updated."));
        }

        /// <summary>Delete a restaurant</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.DeleteRestaurantAsync(id);
            return Ok(ApiResponse<object>.Ok(null!, result ? "Restaurant deleted." : "Delete failed."));
        }

        /// <summary>Upload restaurant logo</summary>
        [HttpPost("{id:int}/logo")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UploadLogo(int id, IFormFile file)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.UploadLogoAsync(id, requesterId, file);
            return Ok(ApiResponse<object>.Ok(null!, result ? "Logo uploaded." : "Upload failed."));
        }

        /// <summary>Upload restaurant cover image</summary>
        [HttpPost("{id:int}/cover")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UploadCover(int id, IFormFile file)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.UploadCoverImageAsync(id, requesterId, file);
            return Ok(ApiResponse<object>.Ok(null!, result ? "Cover uploaded." : "Upload failed."));
        }

        /// <summary>Toggle restaurant active status</summary>
        [HttpPatch("{id:int}/toggle-active")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isActive = await _service.ToggleActiveStatusAsync(id, requesterId);
            return Ok(ApiResponse<object>.Ok(new { isActive }, isActive ? "Restaurant activated." : "Restaurant deactivated."));
        }

        /// <summary>Verify restaurant (SuperAdmin only)</summary>
        [HttpPatch("{id:int}/verify")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Verify(int id)
        {
            await _service.VerifyRestaurantAsync(id);
            return Ok(ApiResponse<object>.Ok(null!, "Restaurant verified."));
        }
    }
}
