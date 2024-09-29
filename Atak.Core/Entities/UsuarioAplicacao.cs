using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Atak.Core.Entities
{
    public class UsuarioAplicacao : IdentityUser<int>
    {

        [StringLength(100)]
        public string NomeCompleto { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }

    }
}
