using Falcare.Cadastro.Core.Entities;
using Falcare.Cadastro.Core.Interfaces;
using Falcare.Cadastro.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;

namespace Falcare.Cadastro.Infra.Services;

public class FornecedorService : IFornecedorService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public FornecedorService(ApplicationDbContext context, UserManager<AppUser> userManager, IEmailService emailService, IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<Fornecedor> CreateDraftAsync(string nomeEmpresa, string cnpj, string emailContato)
    {
        var codigo = await GenerateNextCode();
        
        var fornecedor = new Fornecedor
        {
            NomeEmpresa = nomeEmpresa,
            CNPJ = cnpj,
            EmailContato = emailContato,
            CodigoInterno = codigo,
            DataCadastro = DateTime.UtcNow
        };

        _context.Fornecedores.Add(fornecedor);
        await _context.SaveChangesAsync();
        
        return fornecedor;
    }

    public async Task<string> SendInvitationAsync(int fornecedorId)
    {
        var fornecedor = await _context.Fornecedores.FindAsync(fornecedorId);
        if (fornecedor == null) throw new ArgumentException("Fornecedor not found");
        if (string.IsNullOrEmpty(fornecedor.EmailContato)) throw new ArgumentException("Email is required");

        // 1. Check if user exists, if not create a placeholder user logic OR just generate a token to create user.
        // Simplified flow: The link will point to a "Register" page with the FornecedorId or a specific token.
        // Better: Pre-create the user without password?
        // Let's go with: Create user with random password, generate reset password token.
        
        var existingUser = await _userManager.FindByEmailAsync(fornecedor.EmailContato);
        if (existingUser == null)
        {
            existingUser = new AppUser { UserName = fornecedor.EmailContato, Email = fornecedor.EmailContato, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(existingUser); // No password yet
            if (!result.Succeeded) throw new Exception("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            
            await _userManager.AddToRoleAsync(existingUser, "Fornecedor");
        }

        // Link the user to fornecedor if not linked
        if (fornecedor.UserId != existingUser.Id)
        {
            fornecedor.UserId = existingUser.Id;
            await _context.SaveChangesAsync();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
        var baseUrl = _configuration["AppBaseUrl"] ?? "https://localhost:7107";
        var callbackUrl = $"{baseUrl}/definir-senha?userId={existingUser.Id}&token={Uri.EscapeDataString(token)}";

        await _emailService.SendEmailAsync(fornecedor.EmailContato, "Convite Falcare - Definição de Senha", 
            $"Bem vindo {fornecedor.NomeEmpresa}. Seu código é {fornecedor.CodigoInterno}.<br>Clique aqui para definir sua senha: <a href='{callbackUrl}'>Definir Senha</a>");

        return callbackUrl;
    }

    public async Task<Fornecedor?> GetByUserIdAsync(string userId)
    {
        return await _context.Fornecedores
            .Include(f => f.AreasAtuacao)
            .FirstOrDefaultAsync(f => f.UserId == userId);
    }

    public async Task UpdateAsync(Fornecedor fornecedor)
    {
        _context.Fornecedores.Update(fornecedor);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Fornecedor>> GetAllAsync()
    {
        return await _context.Fornecedores.OrderByDescending(f => f.DataCadastro).ToListAsync();
    }

    public async Task<Fornecedor?> GetByIdAsync(int id)
    {
        return await _context.Fornecedores
            .Include(f => f.AreasAtuacao)
            .Include(f => f.Funcionarios)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    private async Task<string> GenerateNextCode()
    {
        // TCyyxxxx
        var year = DateTime.UtcNow.ToString("yy");
        var prefix = $"TC{year}";
        
        var lastCode = await _context.Fornecedores
            .Where(f => f.CodigoInterno != null && f.CodigoInterno.StartsWith(prefix))
            .OrderByDescending(f => f.CodigoInterno)
            .Select(f => f.CodigoInterno)
            .FirstOrDefaultAsync();

        int sequence = 1;
        if (lastCode != null && lastCode.Length >= 7)
        {
            // TC26115 -> TC(2)yy(2)xxxx(3+)
            var seqStr = lastCode.Substring(4); // TCyy (4 chars)
            if (int.TryParse(seqStr, out int lastSeq))
            {
                sequence = lastSeq + 1;
            }
        }

        return $"{prefix}{sequence:D3}"; // 001, 002...
    }
}
