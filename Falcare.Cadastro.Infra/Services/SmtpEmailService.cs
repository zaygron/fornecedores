using Falcare.Cadastro.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Falcare.Cadastro.Infra.Services;

/// <summary>
/// Serviço de envio de e-mail real usando SMTP
/// </summary>
public class SmtpEmailService : IEmailService
{
    private readonly ILogger<SmtpEmailService> _logger;
    private readonly IConfiguration _configuration;

    public SmtpEmailService(ILogger<SmtpEmailService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            // Obter configurações do appsettings.json
            var smtpServer = _configuration["Email:SmtpServer"] ?? throw new InvalidOperationException("Email:SmtpServer not configured");
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
            var smtpUsername = _configuration["Email:SmtpUsername"] ?? throw new InvalidOperationException("Email:SmtpUsername not configured");
            var smtpPassword = _configuration["Email:SmtpPassword"] ?? throw new InvalidOperationException("Email:SmtpPassword not configured");
            var fromEmail = _configuration["Email:FromEmail"] ?? smtpUsername;
            var fromName = _configuration["Email:FromName"] ?? "Falcare Cadastro";
            var enableSsl = bool.Parse(_configuration["Email:EnableSsl"] ?? "true");

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.EnableSsl = enableSsl;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.Timeout = 10000; // 10 segundos

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(fromEmail, fromName);
                    mailMessage.To.Add(new MailAddress(to));
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    await client.SendMailAsync(mailMessage);

                    _logger.LogInformation("E-mail enviado com sucesso para {To} com assunto '{Subject}'", to, subject);
                }
            }
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "Erro ao enviar e-mail SMTP para {To}", to);
            throw new InvalidOperationException($"Erro ao enviar e-mail: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao enviar e-mail para {To}", to);
            throw;
        }
    }
}
