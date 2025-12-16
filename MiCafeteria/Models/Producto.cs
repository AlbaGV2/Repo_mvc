using System.ComponentModel.DataAnnotations;

namespace MiCafeteria.Models
{
    public class Producto
    {
        //con esto le estoy diciendo que el nombre tiene que ser maximo de 10 caracteres
        [StringLength(10)]
        public String Nombre { get; set; }

        [Range(0.01, 100)]
        public double Precio { get; set; }


        public Producto() { }

        public Producto(String nombre, double precio)
        {
            this.Nombre = nombre;
            this.Precio = precio;
        }
    }
}
