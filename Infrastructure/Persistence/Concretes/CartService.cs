using Application.Abstractions;
using Application.DTOs.Cart;
using Application.Repositories.Cart;
using Application.Repositories.CartItem;
using Application.Repositories.Order;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Concretes;

public class CartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly ICartReadRepository _cartReadRepository;
    private readonly ICartWriteRepository _cartWriteRepository;
    private readonly ICartItemReadRepository _cartItemReadRepository;
    private readonly ICartItemWriteRepository _cartItemWriteRepository;

    public CartService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, ICartReadRepository cartReadRepository, ICartWriteRepository cartWriteRepository, ICartItemReadRepository cartItemReadRepository, ICartItemWriteRepository cartItemWriteRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _orderReadRepository = orderReadRepository;
        _cartReadRepository = cartReadRepository;
        _cartWriteRepository = cartWriteRepository;
        _cartItemReadRepository = cartItemReadRepository;
        _cartItemWriteRepository = cartItemWriteRepository;
    }


    public async Task<List<CartItem>> GetAllCartItemAsync()
    {
        Cart? cart = await ContextUser();
        Cart? result = await _cartReadRepository.Table.Include(c => c.CartItems).ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.Id == cart.Id);

        return result.CartItems.ToList();
    }

    public async Task AddCartItemToCartAsync(AddCartItemDTO cartItem)
    {
        Cart? cart = await ContextUser();
        if (cart != null)
        {
            CartItem _cartItem =
                await _cartItemReadRepository.GetSingleAsync(bi =>
                    bi.CartId == cart.Id && bi.ProductId == cartItem.ProductId);
            
            if (_cartItem != null)
                _cartItem.Quantity++;
            else
            {
                await _cartItemWriteRepository.AddAsync(new()
                {
                    CartId = cart.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                });
            }

            await _cartItemWriteRepository.SaveAsync();
        }
    }

    public async Task UpdateQuantityAsync(UpdateQuantityDTO updateQuantity)
    {
        CartItem cartItem = await _cartItemReadRepository.GetByIdAsync(updateQuantity.BasketItemId);
        if (cartItem != null)
        {
            cartItem.Quantity = updateQuantity.Quantity;
            await _cartItemWriteRepository.SaveAsync();
        }
    }

    public async Task RemoveCartItemAsync(int cartItemId)
    {
        CartItem cartItem = await _cartItemReadRepository.GetByIdAsync(cartItemId);
        if (cartItem != null)
        {
            _cartItemWriteRepository.Remove(cartItem);
            await _cartItemWriteRepository.SaveAsync();
        }
    }

    public Cart GetUserActiveBasket
    {
        get
        {
            Cart cart = ContextUser().Result;
            return cart;
        }
    }

    private async Task<Cart> ContextUser()
    {
        var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            AppUser? user = await _userManager.Users.Include(u => u.Carts)
                .FirstOrDefaultAsync(u => u.UserName == username);

            var basket = from cart in user.Carts
                join order in _orderReadRepository.Table on cart.Id equals order.Id
                    into CartOrder
                from order in CartOrder.DefaultIfEmpty()
                select new
                {
                    Cart = cart,
                    Order = order
                };

            Cart targetCart = new();
            if (basket.Any(b => b.Order == null))
                targetCart = basket.FirstOrDefault(b => b.Order is null).Cart;
            else
                user.Carts.Add(targetCart);
            
            await _cartWriteRepository.SaveAsync();
            return targetCart;
        }

        throw new Exception("Unexpected error occur");
    }
}