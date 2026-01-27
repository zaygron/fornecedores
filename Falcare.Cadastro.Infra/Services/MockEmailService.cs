using Falcare.Cadastro.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Falcare.Cadastro.Infra.Services;

public class MockEmailService : IEmailService
{
    private readonly ILogger<MockEmailService> _logger;

    public MockEmailService(ILogger<MockEmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        _logger.LogWarning("EMAIL MOCK SENDING to {To} with subject {Subject}", to, subject);
        _logger.LogWarning("BODY: {Body}", body);
        
        await File.WriteAllTextAsync("invite.txt", body);
    }
}
