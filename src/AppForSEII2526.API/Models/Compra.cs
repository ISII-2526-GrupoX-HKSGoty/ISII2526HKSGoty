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
        public List<ComprarBocadillo> Bocadillos { get; set; }

        public Compra()
        {
            Bocadillos = new List<ComprarBocadillo>();
        }
        public override bool Equals(object? obj)
        {
            return obj is Compra compra &&
                   Apellido_1Cliente == compra.Apellido_1Cliente &&
                   Apellido_2Cliente == compra.Apellido_2Cliente &&
                   Id == compra.Id &&
                   FechaCompra == compra.FechaCompra &&
                   nBocadillos == compra.nBocadillos &&
                   NombreCliente == compra.NombreCliente &&
                   PrecioTotal == compra.PrecioTotal &&
                   EqualityComparer<List<ComprarBocadillo>>.Default.Equals(Bocadillos, compra.Bocadillos);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Apellido_1Cliente, Apellido_2Cliente, Id, FechaCompra, nBocadillos, NombreCliente, PrecioTotal, Bocadillos);
        }
    }
}
