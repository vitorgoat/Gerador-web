using Atak.Application.DTOs;


namespace Atak.Application.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> RegistrarUsuarioAsync(string email, string senha, string nomeCompleto, DateTime? dataNascimento);
    }
}
