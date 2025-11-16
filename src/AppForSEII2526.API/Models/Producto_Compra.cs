using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
    public class Producto_Compra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int CompraId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PVP { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }

        [ForeignKey(nameof(CompraId))]
        public Compra Compra { get; set; }
    }
}
