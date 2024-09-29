using Microsoft.AspNetCore.Mvc;
using Atak.API.Models;

namespace Atak.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        [HttpPost("gerar")]
        public async Task<IActionResult> GerarExcel([FromBody] ExcelModel model)
        {
            return Ok(new { message = "Arquivo Excel gerado com sucesso." });
        }
    }
}
