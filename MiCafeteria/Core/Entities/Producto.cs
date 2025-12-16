using System.ComponentModel.DataAnnotations;

namespace MiCafeteria.Core.Entities
{
    public class Producto : Auditoria
    {
        public int Id { get; set; }

        
        //required Es un atributo para decir que no puede ser null
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Precio { get; set; }

        //public int CategoriaID { get; set; }

        //Propiedad de navegacion
        #region Propiedades de navegacion   
        //[Required]
        public Categoria Categoria { get; set; }

        #endregion  Propiedades de navegacion


    }
}

