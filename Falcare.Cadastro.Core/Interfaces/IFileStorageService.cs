using System.IO;
using System.Threading.Tasks;

namespace Falcare.Cadastro.Core.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(Stream content, string fileName, string folder);
    bool DeleteFile(string filePath);
}
