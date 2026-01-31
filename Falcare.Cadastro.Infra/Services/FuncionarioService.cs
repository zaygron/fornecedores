using Falcare.Cadastro.Core.Entities;
using Falcare.Cadastro.Core.Interfaces;
using Falcare.Cadastro.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Falcare.Cadastro.Infra.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly ApplicationDbContext _context;

    public FuncionarioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Funcionario>> GetByFornecedorIdAsync(int fornecedorId)
    {
        return await _context.Funcionarios
            .Where(f => f.FornecedorId == fornecedorId && f.Ativo)
            .Include(f => f.Documentos)
            .OrderBy(f => f.Nome)
            .ToListAsync();
    }

    public async Task<Funcionario?> GetByIdAsync(int id)
    {
        return await _context.Funcionarios
            .Include(f => f.Fornecedor)
            .Include(f => f.Documentos)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task AddAsync(Funcionario funcionario)
    {
        funcionario.DataCadastro = DateTime.UtcNow;
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Funcionario funcionario)
    {
        funcionario.DataAtualizacao = DateTime.UtcNow;
        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync();
    }



    public async Task DeleteAsync(int id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario != null)
        {
            funcionario.Ativo = false;
            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Funcionario>> GetAllAsync()
    {
        return await _context.Funcionarios
            .Where(f => f.Ativo)
            .Include(f => f.Fornecedor)
            .Include(f => f.Documentos)
            .OrderBy(f => f.Nome)
            .ToListAsync();
    }
}
