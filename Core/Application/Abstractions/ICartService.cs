using Application.DTOs.Cart;
using Domain.Entities;

namespace Application.Abstractions;

public interface ICartService
{
    Task<List<CartItem>> GetAllCartItemAsync();

    Task AddCartItemToCartAsync(AddCartItemDTO cartItem);

    Task UpdateQuantityAsync(UpdateQuantityDTO updateQuantity);

    Task RemoveCartItemAsync(int cartItemId);

    Cart GetUserActiveBasket { get; }


}