using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Falcare.Cadastro.Core.Entities;
using Falcare.Cadastro.Core.Enums;

namespace Falcare.Cadastro.Core.Interfaces;

public interface IDocumentoService
{
    Task<Documento> UploadDocumentoFuncionarioAsync(int funcionarioId, TipoDocumento tipo, Stream fileStream, string fileName, string contentType, string uploadedByUserId);
    Task<List<Documento>> GetDocumentosFuncionarioAsync(int funcionarioId);
    Task DeleteDocumentoAsync(int id);
}
