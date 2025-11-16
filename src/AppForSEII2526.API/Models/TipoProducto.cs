using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.Models
{
    public class TipoProducto
    {
        [Key]
        public int TipoProductoId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre del tipo")]
        public string Nombre { get; set; }

        // Relación uno a muchos con Producto
        //public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
