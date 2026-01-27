using Falcare.Cadastro.Core.Entities;

namespace Falcare.Cadastro.Core.Interfaces;

public interface IFornecedorService
{
    Task<Fornecedor> CreateDraftAsync(string nomeEmpresa, string cnpj, string emailContato);
    Task<string> SendInvitationAsync(int fornecedorId);
    Task<Fornecedor?> GetByUserIdAsync(string userId);
    Task UpdateAsync(Fornecedor fornecedor);
    Task<List<Fornecedor>> GetAllAsync();
    Task<Fornecedor?> GetByIdAsync(int id);
}
