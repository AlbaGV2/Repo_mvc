using System.ComponentModel.DataAnnotations;

namespace MiCafeteria.Models
{
    public class MiLogin
    {

        public String Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public String Pass { get; set; }
    }
}
