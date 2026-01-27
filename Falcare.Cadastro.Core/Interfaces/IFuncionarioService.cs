using Falcare.Cadastro.Core.Entities;

namespace Falcare.Cadastro.Core.Interfaces;

public interface IFuncionarioService
{
    Task<List<Funcionario>> GetByFornecedorIdAsync(int fornecedorId);
    Task<Funcionario?> GetByIdAsync(int id);
    Task AddAsync(Funcionario funcionario);
    Task UpdateAsync(Funcionario funcionario);
    Task DeleteAsync(int id);
}
