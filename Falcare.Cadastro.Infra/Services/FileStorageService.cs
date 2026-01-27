using System;
using System.IO;
using System.Threading.Tasks;
using Falcare.Cadastro.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Falcare.Cadastro.Infra.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;

    public FileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveFileAsync(Stream content, string fileName, string folder)
    {
        var uploadPath = Path.Combine(_env.WebRootPath, "uploads", folder);
        
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var extension = Path.GetExtension(fileName);
        var safeFileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(uploadPath, safeFileName);

        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await content.CopyToAsync(fileStream);
        }

        return Path.Combine("uploads", folder, safeFileName).Replace("\\", "/");
    }

    public bool DeleteFile(string filePath)
    {
        var fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/').Replace("/", "\\"));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return true;
        }
        return false;
    }
}
