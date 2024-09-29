using System.Net.Mail;

public class EmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromEmail;

    public EmailService(SmtpClient smtpClient, string fromEmail)
    {
        _smtpClient = smtpClient;
        _fromEmail = fromEmail;
    }

    public async Task<bool> EnviarEmailAsync(string emailDestinatario, string assunto, string conteudoTexto, string conteudoHtml, string caminhoAnexo)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail),
                Subject = assunto,
                Body = conteudoHtml,
                IsBodyHtml = true
            };
            mailMessage.To.Add(emailDestinatario);

            if (!string.IsNullOrEmpty(caminhoAnexo))
            {
                var attachment = new Attachment(caminhoAnexo);
                mailMessage.Attachments.Add(attachment);
            }

            await _smtpClient.SendMailAsync(mailMessage);
            return true;
        }
        catch
        {
            
            return false;
        }
    }
}
