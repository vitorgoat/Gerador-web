using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace Atak.Application.Services
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly ILogger<EmailService> _logger;

        public EmailService(SmtpClient smtpClient, ILogger<EmailService> logger)
        {
            _smtpClient = smtpClient;
            _logger = logger;
        }

        public async Task<bool> EnviarEmailAsync(
            string emailDestinatario,
            string assunto,
            string conteudoTexto,
            string conteudoHtml,
            string caminhoAnexo = null)
        {
            var remetente = "vitor.brito.braga@hotmail.com";
            var mensagem = new MailMessage
            {
                From = new MailAddress(remetente, "Gerador Web"),
                Subject = assunto,
                Body = conteudoHtml,
                IsBodyHtml = true
            };

            mensagem.To.Add(emailDestinatario);

            if (!string.IsNullOrEmpty(caminhoAnexo))
            {
                try
                {
                    if (File.Exists(caminhoAnexo))
                    {
                        var anexo = new Attachment(caminhoAnexo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        mensagem.Attachments.Add(anexo);
                    }
                    else
                    {
                        _logger.LogWarning($"O caminho do anexo não existe: {caminhoAnexo}");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Não foi possível adicionar o anexo: {e.Message}");
                }
            }

            try
            {
                await _smtpClient.SendMailAsync(mensagem);
                return true;
            }
            catch (SmtpException e)
            {
                _logger.LogError($"Erro ao enviar e-mail (SMTP): {e.Message}");
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro inesperado ao enviar e-mail: {e.Message}");
                return false;
            }
            finally
            {
                mensagem.Dispose();
            }
        }
    }
}
