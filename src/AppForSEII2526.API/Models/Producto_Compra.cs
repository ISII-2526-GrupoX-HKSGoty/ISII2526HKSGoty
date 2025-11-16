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

        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }

        [ForeignKey(nameof(CompraId))]
        public Compra Compra { get; set; }

        // Constructor vacío para EF Core
        public Producto_Compra() { }

        // Constructor completo recomendado para tu diagrama y documentación
        public Producto_Compra(int id, int productoId, int compraId, int cantidad, Producto producto, Compra compra)
        {
            Id = id;
            ProductoId = productoId;
            CompraId = compraId;
            Cantidad = cantidad;
            Producto = producto;
            Compra = compra;
        }

        public override bool Equals(object? obj)
        {
            return obj is Producto_Compra pc &&
                   Id == pc.Id &&
                   ProductoId == pc.ProductoId &&
                   CompraId == pc.CompraId &&
                   Cantidad == pc.Cantidad &&
                   EqualityComparer<Producto>.Default.Equals(Producto, pc.Producto) &&
                   EqualityComparer<Compra>.Default.Equals(Compra, pc.Compra);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ProductoId, CompraId, Cantidad, Producto, Compra);
        }
    }
}
