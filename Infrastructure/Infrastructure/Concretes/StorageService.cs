using Application.Abstractions;
using Application.DTOs;
using Application.Repositories.ProductImageFile;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Concretes;

public class StorageService : IStorageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IFileService _fileService;
    private readonly IProductImageFileReadRepository _imageFileReadRepository;

    public StorageService(IWebHostEnvironment webHostEnvironment, IFileService fileService, IProductImageFileReadRepository imageFileReadRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _fileService = fileService;
        _imageFileReadRepository = imageFileReadRepository;
    }
    
    public async Task<List<(string fileName, string path)>> UploadAsync(int productId, IFormFileCollection files) 
    {
        var directory = await _fileService.CreateFileDirectory(productId);
        string uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, $"{directory.CategoryName}/{directory.SubCategoryName}/{directory.ProductName}");
        if (!Directory.Exists(uploadDirectory))
            Directory.CreateDirectory(uploadDirectory);
        
        Random r = new();
        List<(string fileName, string path)> results = new();
        foreach (IFormFile file in files)
        {
            string fileNewName = $"{r.Next()}{Path.GetExtension(file.FileName)}";
            string completedDirectory = Path.Combine(uploadDirectory, fileNewName);
            
            await using FileStream fileStream = new(completedDirectory, FileMode.Create, FileAccess.Write, FileShare.None,
                1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();//Stream i bosaltiyoruz.
            results.Add((fileNewName, uploadDirectory));
        }

        return results;
    }

    public async Task DeleteAsync(int productId, string fileName)
    {
        var result = await _fileService.CreateFileDirectory(productId);
        File.Delete(Path.Combine(_webHostEnvironment.WebRootPath,
            $"{result.CategoryName}/{result.SubCategoryName}/{result.ProductName}/{fileName}"));
    }

    public List<ProductImageDTO> GetProductFiles(int productId)
    {
        var result = _imageFileReadRepository.GetWhere(f => f.ProductId == productId).ToList();
        List<ProductImageDTO> list = new();
        foreach (ProductImageFile file in result)
        {
            list.Add(new()
            {
                Path = file.Path,
                ImageName = file.Name,
                ProductId = file.ProductId
            });
        };
        return list;
    }

    public async Task<bool> HasFile(int productId, string fileName)
    {
        var result = await _fileService.CreateFileDirectory(productId);
        return File.Exists(Path.Combine(_webHostEnvironment.WebRootPath,
            $"{result.CategoryName}/{result.SubCategoryName}/{result.ProductName}/{fileName}"));
    }
}