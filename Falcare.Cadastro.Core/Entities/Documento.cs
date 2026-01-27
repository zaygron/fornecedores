using Falcare.Cadastro.Core.Enums;

namespace Falcare.Cadastro.Core.Entities;

public class Documento
{
    public int Id { get; set; }
    
    // Polimorfismo Manual
    public string OwnerType { get; set; } = string.Empty; // "Fornecedor" ou "Funcionario"
    public int OwnerId { get; set; } // FK lógica

    public TipoDocumento Tipo { get; set; }
    
    public DateTime? DataEmissao { get; set; }
    public DateTime? DataValidade { get; set; }
    
    public required string ArquivoNomeOriginal { get; set; }
    public required string ArquivoMimeType { get; set; }
    public required string ArquivoPath { get; set; } // Caminho físico ou chave
    
    public StatusDocumento Status { get; set; } = StatusDocumento.Pendente;
    public string? ObservacaoAprovacao { get; set; }
    
    public DateTime DataUpload { get; set; } = DateTime.UtcNow;
    public string? UploadedByUserId { get; set; } // Quem enviou
}
