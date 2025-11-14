namespace AppForSEII2526.API.Models
{
        public class TipoProducto
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;

            // Navegación (opcional si luego lo necesitas)
            public ICollection<Producto> Productos { get; set; }
        }
    }
