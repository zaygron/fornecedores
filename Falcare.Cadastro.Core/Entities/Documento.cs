using Falcare.Cadastro.Core.Enums;

namespace Falcare.Cadastro.Core.Entities;

public class Documento
{
    public int Id { get; set; }
    
    // Polimorfismo Manual - Relacionamento com Fornecedor ou Funcionário
    public string OwnerType { get; set; } = string.Empty; // "Fornecedor" ou "Funcionario"
    public int OwnerId { get; set; } // FK lógica

    // Tipo de Documento
    public TipoDocumento Tipo { get; set; }
    
    // Datas
    public DateTime? DataEmissao { get; set; }
    public DateTime? DataValidade { get; set; }
    
    // Informações do Arquivo
    public string ArquivoNomeOriginal { get; set; } = string.Empty;
    public string ArquivoMimeType { get; set; } = string.Empty;
    public string ArquivoPath { get; set; } = string.Empty; // Caminho físico ou chave
    public long? ArquivoTamanho { get; set; } // Tamanho em bytes

    // Status e Aprovação
    public StatusDocumento Status { get; set; } = StatusDocumento.Pendente;
    public string? ObservacaoAprovacao { get; set; }
    public DateTime? DataAprovacao { get; set; }
    public string? AprovadoPorUserId { get; set; } // Quem aprovou

    // Auditoria
    public DateTime DataUpload { get; set; } = DateTime.UtcNow;
    public string? UploadedByUserId { get; set; } // Quem enviou
    public DateTime? DataAtualizacao { get; set; }

    // Descrição Adicional
    public string? Descricao { get; set; }
}
