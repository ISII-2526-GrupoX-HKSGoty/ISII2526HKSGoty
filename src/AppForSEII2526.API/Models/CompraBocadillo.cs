

namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BocadilloId), nameof(CompraId))]
    public class CompraBocadillo
    {
        public Bocadillo Bocadillo { get; set; }
        public int BocadilloId { get; set; }
        public int Cantidad { get; set; }
        public Compra Compra { get; set; }
        public int CompraId { get; set; }
        public string NombreBocadillo { get; set; }
        [Precision(10, 2)]
        public decimal Precio { get; set; }        

        public CompraBocadillo()
        {
            
        }

        public override bool Equals(object? obj)
        {
            return obj is CompraBocadillo bocadillo &&
                   EqualityComparer<Bocadillo>.Default.Equals(Bocadillo, bocadillo.Bocadillo) &&
                   BocadilloId == bocadillo.BocadilloId &&
                   Cantidad == bocadillo.Cantidad &&
                   EqualityComparer<Compra>.Default.Equals(Compra, bocadillo.Compra) &&
                   CompraId == bocadillo.CompraId &&
                   NombreBocadillo == bocadillo.NombreBocadillo &&
                   Precio == bocadillo.Precio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Bocadillo, BocadilloId, Cantidad, Compra, CompraId, NombreBocadillo, Precio);
        }
    }
}
