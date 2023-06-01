using Application.Abstractions;
using Application.Repositories.Address;
using Application.Repositories.Cart;
using Application.Repositories.CartItem;
using Application.Repositories.Category;
using Application.Repositories.Order;
using Application.Repositories.Product;
using Application.Repositories.ProductImageFile;
using Application.Repositories.SubCategory;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Concretes;
using Persistence.Contexts;
using Persistence.Repositories.Address;
using Persistence.Repositories.Cart;
using Persistence.Repositories.CartItem;
using Persistence.Repositories.Category;
using Persistence.Repositories.Order;
using Persistence.Repositories.Product;
using Persistence.Repositories.ProductImageFile;
using Persistence.Repositories.SubCategory;

namespace Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<BoutiqueAppAPIDbContext>(options =>
            options.UseNpgsql(Configuration.ConnectionString));

        services.AddIdentity<AppUser,AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<BoutiqueAppAPIDbContext>().AddDefaultTokenProviders();

        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        services.AddScoped<ISubCategoryReadRepository, SubCategoryReadRepository>();
        services.AddScoped<ISubCategoryWriteRepository, SubCategoryWriteRepository>();

        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

        services.AddScoped<ICartReadRepository, CartReadRepository>();
        services.AddScoped<ICartWriteRepository, CartWriteRepository>();

        services.AddScoped<ICartItemReadRepository, CartItemReadRepository>();
        services.AddScoped<ICartItemWriteRepository, CartItemWriteRepository>();

        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

        services.AddScoped<IAddressReadRepository, AddressReadRepository>();
        services.AddScoped<IAddressWriteRepository, AddressWriteRepository>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IOrderService, OrderService>();

    }
    
    
}