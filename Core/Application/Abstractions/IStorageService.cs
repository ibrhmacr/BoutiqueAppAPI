using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions;

public interface IStorageService
{
    Task<List<(string fileName, string path)>> UploadAsync(int productId, IFormFileCollection files);
    //Name parametresi alip ona gore de bir isim calismasi yapabilirsin.
    //Veritabanina kaydetmek icin fileName ve dosyanin pathini donuyoruz.

    Task DeleteAsync(int productId, string fileName);

    List<ProductImageDTO> GetProductFiles(int productId);

    Task<bool> HasFileAsync(int productId, string fileName);
}