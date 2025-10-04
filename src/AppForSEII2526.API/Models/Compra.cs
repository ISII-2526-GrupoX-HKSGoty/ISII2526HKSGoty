namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        [StringLength(20, ErrorMessage = "Maxímo 50, minimo 10", MinimumLength = 5)]
        public string Apellido_1Cliente { get; set; }

        [StringLength(20, ErrorMessage = "Maxímo 50, minimo 10", MinimumLength = 5)]
        public string Apellido_2Cliente { get; set; }

        public int Id { get; set; }
        public DateTime FechaCompra { get; set; }
        public int nBocadillos { get; set; }

        [StringLength(20, ErrorMessage = "Maxímo 50, minimo 10", MinimumLength = 5)]
        public string NombreCliente { get; set; }

        [Precision(10, 2)]
        public decimal PrecioTotal { get; set; }

    }
}
