using Falcare.Cadastro.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Falcare.Cadastro.Web.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, true, false);
        if (result.Succeeded)
        {
            return Redirect("/fornecedor/dados-empresa");
        }
        return Redirect("/login?error=InvalidCredentials");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
}
