using Application.DTOs.Address;
using Domain.Entities;

namespace Application.Abstractions;

public interface IAddressService
{
    Task CreateAddressAsync(CreateAddressDTO createAddress);
    
    Task RemoveAddressAsync(int addressId);

    Task<List<Address>> GetUserAddresses();

    Task<Address> GetAddressById(int id);
}