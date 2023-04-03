using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions;

public interface IFileService
{
    Task<ProductDirectoryDTO> CreateFileDirectory(int productId);
}