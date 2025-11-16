using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
    public class Compra_Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Required]
        public int IdCompra { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey(nameof(IdProducto))]
        public Producto Producto { get; set; }

        [ForeignKey(nameof(IdCompra))]
        public Compra Compra { get; set; }
    }
}
