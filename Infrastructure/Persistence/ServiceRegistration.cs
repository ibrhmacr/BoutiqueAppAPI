using Application.Abstractions;
using Application.Repositories.Category;
using Application.Repositories.Product;
using Application.Repositories.ProductImageFile;
using Application.Repositories.SubCategory;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Concretes;
using Persistence.Contexts;
using Persistence.Repositories.Category;
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
        }).AddEntityFrameworkStores<BoutiqueAppAPIDbContext>();

        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        services.AddScoped<ISubCategoryReadRepository, SubCategoryReadRepository>();
        services.AddScoped<ISubCategoryWriteRepository, SubCategoryWriteRepository>();

        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
    }
    
    
}