using Falcare.Cadastro.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Falcare.Cadastro.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<AreaAtuacao> AreasAtuacao { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuração Fornecedor
        builder.Entity<Fornecedor>()
            .HasIndex(f => f.CNPJ).IsUnique();
        
        builder.Entity<Fornecedor>()
            .HasIndex(f => f.CodigoInterno).IsUnique();

        // Relacionamento Many-to-Many Fornecedor <-> AreaAtuacao
        builder.Entity<Fornecedor>()
            .HasMany(f => f.AreasAtuacao)
            .WithMany(a => a.Fornecedores)
            .UsingEntity(j => j.ToTable("FornecedorAreasAtuacao"));

        // Funcionario
        builder.Entity<Funcionario>()
            .HasOne(f => f.Fornecedor)
            .WithMany(p => p.Funcionarios)
            .HasForeignKey(f => f.FornecedorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
