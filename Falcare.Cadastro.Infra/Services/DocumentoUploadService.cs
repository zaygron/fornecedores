using Falcare.Cadastro.Core.Entities;
using Falcare.Cadastro.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Falcare.Cadastro.Infra.Services;

public interface IDocumentoUploadService
{
    Task<(bool Success, string Path, string Error)> UploadDocumentoAsync(IFormFile arquivo, int funcionarioId, TipoDocumento tipo);
    Task<bool> DeletarDocumentoAsync(string path);
    string GetUploadDirectory();
}

public class DocumentoUploadService : IDocumentoUploadService
{
    private readonly string _uploadDirectory;
    private readonly long _maxFileSize = 10 * 1024 * 1024; // 10 MB
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };

    public DocumentoUploadService()
    {
        _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "documentos");
        
        // Criar diretório se não existir
        if (!Directory.Exists(_uploadDirectory))
        {
            Directory.CreateDirectory(_uploadDirectory);
        }
    }

    public async Task<(bool Success, string Path, string Error)> UploadDocumentoAsync(
        IFormFile arquivo, 
        int funcionarioId, 
        TipoDocumento tipo)
    {
        try
        {
            // Validar arquivo
            var validacao = ValidarArquivo(arquivo);
            if (!validacao.IsValid)
            {
                return (false, string.Empty, validacao.Errors.First());
            }

            // Criar nome único para o arquivo
            var extensao = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
            var nomeArquivo = $"{funcionarioId}_{tipo}_{DateTime.UtcNow:yyyyMMddHHmmss}{extensao}";
            var caminhoCompleto = Path.Combine(_uploadDirectory, nomeArquivo);

            // Salvar arquivo
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return (true, $"/uploads/documentos/{nomeArquivo}", string.Empty);
        }
        catch (Exception ex)
        {
            return (false, string.Empty, $"Erro ao fazer upload: {ex.Message}");
        }
    }

    public async Task<bool> DeletarDocumentoAsync(string path)
    {
        try
        {
            var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.TrimStart('/'));
            
            if (File.Exists(caminhoCompleto))
            {
                File.Delete(caminhoCompleto);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public string GetUploadDirectory()
    {
        return _uploadDirectory;
    }

    private ValidationResult ValidarArquivo(IFormFile arquivo)
    {
        var erros = new List<string>();

        // Validar se arquivo foi fornecido
        if (arquivo == null || arquivo.Length == 0)
        {
            erros.Add("Arquivo não foi fornecido");
            return new ValidationResult { IsValid = false, Errors = erros };
        }

        // Validar tamanho
        if (arquivo.Length > _maxFileSize)
        {
            erros.Add($"Arquivo excede o tamanho máximo de 10 MB");
        }

        // Validar extensão
        var extensao = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(extensao))
        {
            erros.Add($"Tipo de arquivo não permitido. Extensões aceitas: {string.Join(", ", _allowedExtensions)}");
        }

        // Validar MIME type
        var mimeTypesPermitidos = new[] { "image/jpeg", "image/png", "application/pdf" };
        if (!mimeTypesPermitidos.Contains(arquivo.ContentType?.ToLowerInvariant() ?? ""))
        {
            erros.Add("Tipo MIME do arquivo não é válido");
        }

        return new ValidationResult
        {
            IsValid = erros.Count == 0,
            Errors = erros
        };
    }
}
