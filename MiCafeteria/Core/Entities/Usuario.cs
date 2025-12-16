using MiCafeteria.Core.Enums;

namespace MiCafeteria.Core.Entities
{
    public class Usuario
    {
        public string Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
