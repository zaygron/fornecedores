namespace Falcare.Cadastro.Core.Entities;

public class AreaAtuacao
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    
    public ICollection<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
}
