using System.ComponentModel.DataAnnotations;

namespace Atak.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password, ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}
