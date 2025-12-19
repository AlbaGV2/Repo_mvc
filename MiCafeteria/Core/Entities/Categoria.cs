using System.ComponentModel.DataAnnotations;

namespace MiCafeteria.Core.Entities
{
    public class Categoria : Auditoria
    {
        //pongo key cuando el id no se llama Id o idcategoria y hay que poner otro nombre y hay que hacerlo por api fluent en db context
        //[Key]
        public int Id { get; set; }

        [StringLength(40)]
        //required Es un atributo para decir que no puede ser null
        [Required]
        public string Nombre { get; set; }

        [StringLength(40)]
        //[Required(ErrorMessage = "Esto es mi error")]
        public string Descripcion { get; set; }


        #region Propiedades de navegacion   

        public List<Producto> Productos { get; set; }

        #endregion  Propiedades de navegacion

    }
}
