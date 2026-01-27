using Falcare.Cadastro.Core.Enums;

namespace Falcare.Cadastro.Core.Entities;

public class Funcionario
{
    public int Id { get; set; }
    
    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; } = null!;

    public string? PropositoCadastro { get; set; } // Integração / Outros
    public NaturezaAtividade NaturezaAtividade { get; set; }
    public string? OutraNaturezaDescricao { get; set; }

    // Responsavel Legal (se aplicavel, ou usa dados do fornecer)
    public string? NomeResponsavelLegal { get; set; }
    public string? EmailResponsavel { get; set; }

    // Dados Pessoais
    public string Nome { get; set; } = string.Empty;
    public string? Cargo { get; set; }
    public DateTime? DataNascimento { get; set; }
    
    // Documentos / Identificação
    public string? CTPS_NumeroSerie { get; set; }
    public string? RG { get; set; }
    public DateTime? RG_DataVencimento { get; set; }
    public string? CPF { get; set; }
    public DateTime? CPF_DataVencimento { get; set; } // CPF tem vencimento? diretrizes pedem. (Talvez regularidade?)
    public string? CNH { get; set; }
    public DateTime? CNH_DataVencimento { get; set; }
    public DateTime? ASO_DataVencimento { get; set; }

    // Perguntas de Segurança (Flags)
    public bool TrabalhaComEletricidade { get; set; }
    public bool MovimentacaoCarga { get; set; } // NR11
    public bool CaldeirasVasosPressao { get; set; } // NR13
    public bool TrabalhoAltura { get; set; } // NR35
}
