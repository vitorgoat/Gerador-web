using Microsoft.AspNetCore.Mvc;
using Atak.Application.Services;
using Atak.API.Models;

namespace Atak.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportarController : ControllerBase
    {
        private readonly GeradorDeDados _geradorDeDados;
        private readonly ServicoDeExcel _servicoDeExcel;
        private readonly EmailService _emailService;
        private readonly ILogger<ExportarController> _logger;

        public ExportarController(GeradorDeDados geradorDeDados, ServicoDeExcel servicoDeExcel, EmailService emailService, ILogger<ExportarController> logger)
        {
            _geradorDeDados = geradorDeDados;
            _servicoDeExcel = servicoDeExcel;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet("gerar-excel")]
        public IActionResult GerarExcel([FromQuery] ExcelModel model)
        {
            if (model.Quantidade < 10 || model.Quantidade > 1000)
            {
                return BadRequest("A quantidade deve estar entre 10 e 1000 registros.");
            }

            try
            {
                var clientes = _geradorDeDados.GerarClientes(model.Quantidade);
                var dadosExcel = _servicoDeExcel.GerarExcel(clientes);
                var nomeArquivo = "Clientes.xlsx";

                return File(dadosExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao gerar o Excel: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao gerar o arquivo Excel. Tente novamente.");
            }
        }

        [HttpPost("enviar-email")]
        public async Task<IActionResult> EnviarEmail([FromBody] EmailModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest("O endereço de e-mail é obrigatório.");
            }

            try
            {
                var clientes = _geradorDeDados.GerarClientes(model.Quantidade);
                var dadosExcel = _servicoDeExcel.GerarExcel(clientes);

                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "Arquivo");
                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                var caminhoArquivo = Path.Combine(caminhoPasta, "Clientes.xlsx");
                System.IO.File.WriteAllBytes(caminhoArquivo, dadosExcel);

                var emailEnviado = await _emailService.EnviarEmailAsync(
                    emailDestinatario: model.Email,
                    assunto: "Gerador Web - Dados Gerados",
                    conteudoTexto: "Olá,\n\nSegue em anexo o arquivo com os dados gerados pelo Gerador Web.",
                    conteudoHtml: $"<p>Olá,</p><p>Segue em anexo o arquivo com os dados gerados pelo Gerador Web. E o link do meu github.</p><p>Link do projeto: <a href='{model.Link}'>{model.Link}</a></p>",
                    caminhoAnexo: caminhoArquivo
                );

                if (System.IO.File.Exists(caminhoArquivo))
                {
                    System.IO.File.Delete(caminhoArquivo);
                }

                if (emailEnviado)
                {
                    return Ok(new { success = true, message = "E-mail enviado com sucesso." });
                }
                else
                {
                    return Ok(new { success = false, message = "Não foi possível enviar o e-mail. Tente novamente." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao enviar e-mail: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao enviar o e-mail. Tente novamente.");
            }
        }
    }
}
