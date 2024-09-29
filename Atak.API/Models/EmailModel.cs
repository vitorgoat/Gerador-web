using System.ComponentModel.DataAnnotations;

namespace Atak.API.Models
{
    public class EmailModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }
}
