using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppForSEII2526.API.Models
{
    public class TipoProducto
    {
        [Key]
        public int TipoProductoId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        public List<Producto> Productos { get; set; }

        // Constructor vacío requerido por EF
        public TipoProducto()
        {
            Productos = new List<Producto>();
        }

        // Constructor completo útil para pruebas y diagramas
        public TipoProducto(int tipoProductoId, string nombre, List<Producto> productos)
        {
            TipoProductoId = tipoProductoId;
            Nombre = nombre;
            Productos = productos;
        }

        public override bool Equals(object? obj)
        {
            return obj is TipoProducto tipo &&
                   TipoProductoId == tipo.TipoProductoId &&
                   Nombre == tipo.Nombre &&
                   EqualityComparer<List<Producto>>.Default.Equals(Productos, tipo.Productos);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TipoProductoId, Nombre, Productos);
        }
    }
}
