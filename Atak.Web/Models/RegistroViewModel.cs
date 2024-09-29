using System.ComponentModel.DataAnnotations;

namespace Atak.Web.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório.")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não correspondem.")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        public string NomeCompleto { get; set; }
    }
}
