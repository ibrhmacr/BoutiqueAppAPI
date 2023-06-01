using Application.Abstractions;
using Application.DTOs.Address;
using Application.Repositories.Address;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Concretes;

public class AddressService : IAddressService
{
    private readonly IAddressReadRepository _addressReadRepository;
    private readonly IAddressWriteRepository _addressWriteRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public AddressService(IAddressReadRepository addressReadRepository, IAddressWriteRepository addressWriteRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _addressReadRepository = addressReadRepository;
        _addressWriteRepository = addressWriteRepository;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task CreateAddressAsync(CreateAddressDTO createAddress)
    {
        AppUser user = await ContextUser();
        await _addressWriteRepository.AddAsync(new()
        {
            UserId = user.Id,
            AddressName = createAddress.AddressName,
            Description = createAddress.Description
        });
        await _addressWriteRepository.SaveAsync();
    }

    public async Task RemoveAddressAsync(int addressId)
    {
        await _addressWriteRepository.RemoveAsync(addressId);
        await _addressWriteRepository.SaveAsync();
    }

    public async Task<List<Address>> GetUserAddresses()
    {
        AppUser user = await ContextUser();
        var addresses = _addressReadRepository.GetWhere(a => a.UserId == user.Id);
        return addresses.ToList();
    }

    public Task<Address> GetAddressById(int id)
    {
        return _addressReadRepository.GetByIdAsync(id);
    }

    private async Task<AppUser> ContextUser()
    {
        var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return user;
        }
        
        throw new Exception("Unexpected error occur");
    }
}