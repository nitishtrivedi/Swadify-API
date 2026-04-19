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
    [Authorize(Roles = "Customer")]
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public CartController(ICartService service) => _service = service;

        /// <summary>Get current cart</summary>
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var result = await _service.GetCartAsync(UserId);
            return Ok(ApiResponse<CartResponseDto>.Ok(result));
        }

        /// <summary>Add item to cart</summary>
        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddToCartDto dto)
        {
            var result = await _service.AddToCartAsync(UserId, dto);
            return Ok(ApiResponse<CartResponseDto>.Ok(result, "Item added to cart."));
        }

        /// <summary>Update cart item quantity (0 to remove)</summary>
        [HttpPut("items/{cartItemId:int}")]
        public async Task<IActionResult> UpdateItem(int cartItemId, [FromBody] UpdateCartItemDto dto)
        {
            var result = await _service.UpdateCartItemAsync(UserId, cartItemId, dto);
            return Ok(ApiResponse<CartResponseDto>.Ok(result, "Cart updated."));
        }

        /// <summary>Remove a specific cart item</summary>
        [HttpDelete("items/{cartItemId:int}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            await _service.RemoveCartItemAsync(UserId, cartItemId);
            return Ok(ApiResponse<object>.Ok(null!, "Item removed."));
        }

        /// <summary>Clear entire cart</summary>
        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            await _service.ClearCartAsync(UserId);
            return Ok(ApiResponse<object>.Ok(null!, "Cart cleared."));
        }
    }
}
