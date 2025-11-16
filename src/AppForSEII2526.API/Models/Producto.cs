using AppForSEII2526.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre del producto")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(300)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required]
        [Range(0.01, 10000)]
        [Display(Name = "Precio unitario")]
        public decimal Precio { get; set; }

        [Required]
        [ForeignKey("TipoProducto")]
        [Display(Name = "Tipo de producto")]
        public int TipoProductoId { get; set; }

        public int Stock {  get; set; }

        public TipoProducto? TipoProducto { get; set; }
    }
}
