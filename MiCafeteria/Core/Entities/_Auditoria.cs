namespace MiCafeteria.Core.Entities
{
    public class Auditoria
    {

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;


    }
}
