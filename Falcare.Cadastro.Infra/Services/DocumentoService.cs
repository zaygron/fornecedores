using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Falcare.Cadastro.Core.Entities;
using Falcare.Cadastro.Core.Enums;
using Falcare.Cadastro.Core.Interfaces;
using Falcare.Cadastro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Falcare.Cadastro.Infra.Services;

public class DocumentoService : IDocumentoService
{
    private readonly ApplicationDbContext _context;
    private readonly IFileStorageService _fileStorage;

    public DocumentoService(ApplicationDbContext context, IFileStorageService fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task<Documento> UploadDocumentoFuncionarioAsync(int funcionarioId, TipoDocumento tipo, Stream fileStream, string fileName, string contentType, string uploadedByUserId)
    {
        // 1. Save file
        var folder = $"funcionarios/{funcionarioId}";
        var path = await _fileStorage.SaveFileAsync(fileStream, fileName, folder);

        // 2. Create Entity
        var documento = new Documento
        {
            OwnerType = "Funcionario",
            OwnerId = funcionarioId,
            Tipo = tipo,
            ArquivoNomeOriginal = fileName,
            ArquivoMimeType = contentType,
            ArquivoPath = path,
            Status = StatusDocumento.Enviado,
            UploadedByUserId = uploadedByUserId,
            DataUpload = DateTime.UtcNow
        };

        _context.Documentos.Add(documento);
        await _context.SaveChangesAsync();

        return documento;
    }

    public async Task<List<Documento>> GetDocumentosFuncionarioAsync(int funcionarioId)
    {
        return await _context.Documentos
            .Where(d => d.OwnerType == "Funcionario" && d.OwnerId == funcionarioId)
            .OrderByDescending(d => d.DataUpload)
            .ToListAsync();
    }

    public async Task DeleteDocumentoAsync(int id)
    {
        var doc = await _context.Documentos.FindAsync(id);
        if (doc != null)
        {
            _fileStorage.DeleteFile(doc.ArquivoPath);
            _context.Documentos.Remove(doc);
            await _context.SaveChangesAsync();
        }
    }
}
