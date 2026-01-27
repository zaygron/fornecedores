using System.ComponentModel.DataAnnotations;
using Falcare.Cadastro.Core.Enums;

namespace Falcare.Cadastro.Core.Entities;

public class Fornecedor
{
    public int Id { get; set; }

    // Dados de Sistema
    [MaxLength(20)]
    public string? CodigoInterno { get; set; } // TCyyxxxx
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public StatusFornecedor Status { get; set; } = StatusFornecedor.Pendente;
    
    // Vinculo com login (Identity User Id)
    // Pode ser nulo inicialmente até o usuário ser criado, ou criado junto
    public string? UserId { get; set; }
    public AppUser? User { get; set; }

    // Dados da Empresa
    public required string CNPJ { get; set; }
    public string? InscricaoEstadual { get; set; }
    public required string NomeEmpresa { get; set; } // Razão Social
    public string? NomeFantasia { get; set; }
    
    // Contato
    public string? NomeContato { get; set; }
    public string? EmailContato { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }

    // Endereço
    public string? CEP { get; set; }
    public string? Endereco { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    // Relacionamentos
    public ICollection<AreaAtuacao> AreasAtuacao { get; set; } = new List<AreaAtuacao>();
    public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    
    // Documentos podem ser filtrados pelo OwnerId e OwnerType na tabela de Documentos, 
    // mas também podemos ter uma nav property se configurarmos discriminadores.
    // Para simplificar, vamos filtrar via query ou usar [NotMapped] helper se precisar.
}
