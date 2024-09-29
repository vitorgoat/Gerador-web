using Atak.Application.DTOs;
using Atak.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Atak.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;

   
        public UsuarioService(UserManager<UsuarioAplicacao> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UsuarioDTO> RegistrarUsuarioAsync(string email, string senha, string nomeCompleto, DateTime? dataNascimento)
        {

            var usuario = new UsuarioAplicacao
            {
                UserName = email,
                Email = email,
                NomeCompleto = nomeCompleto,
                DataNascimento = dataNascimento
            };

    
            var resultado = await _userManager.CreateAsync(usuario, senha);

      
            if (!resultado.Succeeded)
            {
 
                return null;
            }


            return new UsuarioDTO

            {
                Email = usuario.Email,
                NomeCompleto = usuario.NomeCompleto,
                DataNascimento = usuario.DataNascimento
            };
        }
    }
}
