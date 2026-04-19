using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Interfaces;

namespace Swadify_API.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _db;

        public CartService(AppDbContext db) => _db = db;

        public async Task<CartResponseDto> GetCartAsync(int customerId)
        {
            var cart = await GetOrNullCartAsync(customerId);
            if (cart == null) return new CartResponseDto();
            return MapToDto(cart);
        }

        public async Task<CartResponseDto> AddToCartAsync(int customerId, AddToCartDto dto)
        {
            var menuItem = await _db.MenuItems.Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == dto.MenuItemId)
                ?? throw new KeyNotFoundException("Menu item not found.");

            if (!menuItem.IsAvailable)
                throw new InvalidOperationException("This item is currently unavailable.");

            // Check if user has a cart from a different restaurant
            var existingCart = await GetOrNullCartAsync(customerId);
            if (existingCart != null && existingCart.RestaurantId != menuItem.RestaurantId)
                throw new InvalidOperationException("Your cart has items from another restaurant. Please clear cart first.");

            if (existingCart == null)
            {
                existingCart = new Cart { CustomerId = customerId, RestaurantId = menuItem.RestaurantId };
                _db.Carts.Add(existingCart);
                await _db.SaveChangesAsync();
            }

            var cartItem = await _db.CartItems.FirstOrDefaultAsync(ci => ci.CartId == existingCart.Id && ci.MenuItemId == dto.MenuItemId);

            if (cartItem != null)
            {
                cartItem.Quantity += dto.Quantity;
                cartItem.SpecialInstructions = dto.SpecialInstructions ?? cartItem.SpecialInstructions;
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = existingCart.Id,
                    MenuItemId = dto.MenuItemId,
                    Quantity = dto.Quantity,
                    UnitPrice = menuItem.DiscountedPrice ?? menuItem.Price,
                    SpecialInstructions = dto.SpecialInstructions
                };
                _db.CartItems.Add(cartItem);
            }

            await _db.SaveChangesAsync();
            return await GetCartAsync(customerId);
        }

        public async Task<CartResponseDto> UpdateCartItemAsync(int customerId, int cartItemId, UpdateCartItemDto dto)
        {
            var cartItem = await _db.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.Cart!.CustomerId == customerId)
                ?? throw new KeyNotFoundException("Cart item not found.");

            if (dto.Quantity == 0)
                _db.CartItems.Remove(cartItem);
            else
                cartItem.Quantity = dto.Quantity;

            await _db.SaveChangesAsync();
            return await GetCartAsync(customerId);
        }

        public async Task<bool> ClearCartAsync(int customerId)
        {
            var cart = await GetOrNullCartAsync(customerId);
            if (cart == null) return true;
            _db.Carts.Remove(cart);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCartItemAsync(int customerId, int cartItemId)
        {
            var cartItem = await _db.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.Cart!.CustomerId == customerId)
                ?? throw new KeyNotFoundException("Cart item not found.");

            _db.CartItems.Remove(cartItem);
            await _db.SaveChangesAsync();
            return true;
        }

        private async Task<Cart?> GetOrNullCartAsync(int customerId)
        {
            return await _db.Carts
                .Include(c => c.CartItems!)
                .ThenInclude(ci => ci.MenuItem)
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        private static CartResponseDto MapToDto(Cart cart)
        {
            var items = cart.CartItems?.Select(ci => new CartItemResponseDto
            {
                Id = ci.Id,
                MenuItemId = ci.MenuItemId,
                ItemName = ci.MenuItem?.Name ?? string.Empty,
                ImageUrl = ci.MenuItem?.ImageUrl,
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice,
                SpecialInstructions = ci.SpecialInstructions,
                IsAvailable = ci.MenuItem?.IsAvailable ?? false
            }).ToList() ?? [];

            return new CartResponseDto
            {
                Id = cart.Id,
                RestaurantId = cart.RestaurantId,
                RestaurantName = cart.Restaurant?.Name ?? string.Empty,
                Items = items,
                SubTotal = items.Sum(i => i.TotalPrice),
                TotalItems = items.Sum(i => i.Quantity)
            };
        }
    }
}
