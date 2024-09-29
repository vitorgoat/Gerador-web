using Microsoft.AspNetCore.Mvc;
using Atak.API.Models;

namespace Atak.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarEmail([FromBody] EmailModel model)
        {
            return Ok(new { message = "E-mail enviado com sucesso." });
        }
    }
}
