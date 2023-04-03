using Application.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Abstractions;

public interface IUserService
{
    Task<bool> RegisterUser(RegisterUserDTO registerUser);
}