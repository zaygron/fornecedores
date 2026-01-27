using Microsoft.AspNetCore.Identity;

namespace Falcare.Cadastro.Core.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
}
