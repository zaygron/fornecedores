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
            .Where(f => f.FornecedorId == fornecedorId)
            .ToListAsync();
    }

    public async Task<Funcionario?> GetByIdAsync(int id)
    {
        return await _context.Funcionarios.FindAsync(id);
    }

    public async Task AddAsync(Funcionario funcionario)
    {
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Funcionario funcionario)
    {
        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync();
    }



    public async Task DeleteAsync(int id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario != null)
        {
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
        }
    }
}
