using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Atak.API.Models;
using Atak.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Atak.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        private readonly SignInManager<UsuarioAplicacao> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<UsuarioAplicacao> userManager, SignInManager<UsuarioAplicacao> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = new UsuarioAplicacao
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto
            };

            var resultado = await _userManager.CreateAsync(usuario, model.Password);
            if (resultado.Succeeded)
            {
                await _signInManager.SignInAsync(usuario, isPersistent: false);
                return Ok(new { message = "Usuário registrado e autenticado com sucesso." });
            }

            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(string.Empty, erro.Description);

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                var usuario = await _userManager.FindByEmailAsync(model.Email);
                var token = GenerateJwtToken(usuario);
                return Ok(new { Token = token });
            }

            ModelState.AddModelError(string.Empty, "Falha ao fazer login. Verifique seu e-mail e senha e tente novamente.");
            return BadRequest(ModelState);
        }

        private string GenerateJwtToken(UsuarioAplicacao usuario)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JwtBearerTokenSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName),
                    new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtBearerTokenSettings:ExpiryTimeInMinutes"])),
                Issuer = _configuration["JwtBearerTokenSettings:Issuer"],
                Audience = _configuration["JwtBearerTokenSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
