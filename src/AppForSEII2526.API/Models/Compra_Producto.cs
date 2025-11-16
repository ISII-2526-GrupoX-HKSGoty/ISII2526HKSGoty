using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
    public class Compra_Producto
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
        public decimal PrecioUnitario { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }

        [ForeignKey(nameof(CompraId))]
        public Compra Compra { get; set; }

        // Constructor vacío necesario para EF Core
        public Compra_Producto() { }

        // Constructor completo recomendado para tu diagrama y documentación
        public Compra_Producto(int id, int productoId, int compraId, int cantidad, decimal precioUnitario, Producto producto, Compra compra)
        {
            Id = id;
            ProductoId = productoId;
            CompraId = compraId;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Producto = producto;
            Compra = compra;
        }

        public override bool Equals(object? obj)
        {
            return obj is Compra_Producto cp &&
                   Id == cp.Id &&
                   ProductoId == cp.ProductoId &&
                   CompraId == cp.CompraId &&
                   Cantidad == cp.Cantidad &&
                   PrecioUnitario == cp.PrecioUnitario &&
                   EqualityComparer<Producto>.Default.Equals(Producto, cp.Producto) &&
                   EqualityComparer<Compra>.Default.Equals(Compra, cp.Compra);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ProductoId, CompraId, Cantidad, PrecioUnitario, Producto, Compra);
        }
    }
}
