using Falcare.Cadastro.Core.Enums;

namespace Falcare.Cadastro.Core.Entities;

public class Funcionario
{
    public int Id { get; set; }
    
    // Relacionamento com Fornecedor
    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; } = null!;

    // Informações de Cadastro
    public string PropositoCadastro { get; set; } = "Integração"; // Integração / Outros
    public NaturezaAtividade NaturezaAtividade { get; set; }
    public string? OutraNaturezaDescricao { get; set; }

    // Responsável Legal
    public string NomeResponsavelLegal { get; set; } = string.Empty;
    public string EmailResponsavel { get; set; } = string.Empty;

    // Dados Pessoais do Funcionário
    public string Nome { get; set; } = string.Empty;
    public string? Cargo { get; set; }
    public DateTime? DataNascimento { get; set; }

    // Carteira de Trabalho (CTPS)
    public string CTPS_Tipo { get; set; } = "Digital"; // "Digital" ou "Fisica"
    public string? CTPS_NumeroSerie { get; set; }
    public string? CTPS_UF { get; set; }

    // RG - Registro Geral
    public string? RG { get; set; }
    public DateTime? RG_DataVencimento { get; set; }

    // CPF - Cadastro de Pessoa Física
    public string? CPF { get; set; }
    public DateTime? CPF_DataVencimento { get; set; }

    // CNH - Carteira Nacional de Habilitação
    public string? CNH { get; set; }
    public DateTime? CNH_DataVencimento { get; set; }

    // ASO - Atestado de Saúde Ocupacional
    public DateTime? ASO_DataVencimento { get; set; }

    // Perguntas de Segurança e Conformidade
    public bool TrabalhaComEletricidade { get; set; }
    public bool MovimentacaoCarga { get; set; } // NR11 - Movimentação de cargas
    public bool CaldeirasVasosPressao { get; set; } // NR13 - Caldeiras e vasos de pressão
    public bool TrabalhoAltura { get; set; } // NR35 - Trabalho em altura

    // Metadados
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }
    public bool Ativo { get; set; } = true;

    // Relacionamento com Documentos
    public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
