namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        public Compra()
        {
        }

        public Compra(ApplicationUser user, DateTime fechaCompra, Metodo_Pago metodo_Pago, List<CompraBocadillo> bocadillosComprados)
        {
            User = user;
            FechaCompra = fechaCompra;
            Metodo_Pago = metodo_Pago;
            BocadillosComprados = bocadillosComprados;
        }

        [Required]
        public ApplicationUser User { get; set; }

        [Key]
        public int CompraId { get; set; }

        [Required]
        public DateTime FechaCompra { get; set; }

        [Required]
        public int nBocadillos { get; set; }

        [Required]
        public Metodo_Pago Metodo_Pago { get; set; }

        [Required]
        public float PrecioTotal { get; set; }

        public List<CompraBocadillo> BocadillosComprados { get; set; }

        

        public override bool Equals(object? obj)
        {
            return obj is Compra compra &&
                   EqualityComparer<ApplicationUser>.Default.Equals(User, compra.User) &&
                   CompraId == compra.CompraId &&
                   FechaCompra == compra.FechaCompra &&
                   nBocadillos == compra.nBocadillos &&
                   Metodo_Pago == compra.Metodo_Pago &&
                   PrecioTotal == compra.PrecioTotal &&
                   EqualityComparer<List<CompraBocadillo>>.Default.Equals(BocadillosComprados, compra.BocadillosComprados);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(User, CompraId, FechaCompra, nBocadillos, Metodo_Pago, PrecioTotal, BocadillosComprados);
        }
    }
}
