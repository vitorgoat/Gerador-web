using System.ComponentModel.DataAnnotations;

namespace Atak.API.Models
{
    public class RegistroModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string NomeCompleto { get; set; }
    }
}
