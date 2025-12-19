using System.ComponentModel.DataAnnotations;

namespace MiCafeteria.Models
{
    public class Categoria
    {
        [StringLength(10)]
        public String nombre { get; set; }

        [StringLength(20)]
        public String descripcion { get; set; }


        public Categoria() { }
        public Categoria(String nombre, String descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }
    }
}
