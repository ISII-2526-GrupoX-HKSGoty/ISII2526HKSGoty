
namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BocadilloId), nameof(CompraId))]
    public class CompraBocadillo
    {
        public int BocadilloId { get; set; }
        public int Cantidad { get; set; }

        public int CompraId { get; set; }
        public string NombreBocadillo { get; set; }
        [Precision(10, 2)]
        public decimal Precio { get; set; }
        public Bocadillo Bocadillo { get; set; }
        public Compra Compra { get; set; }
        public TipoPan Tipopan { get; set; }
        

        public CompraBocadillo()
        {
            Bocadillo = new Bocadillo();
            Compra = new Compra();
        }
        public override bool Equals(object? obj)
        {
            return obj is CompraBocadillo bocadillo &&
                   BocadilloId == bocadillo.BocadilloId &&
                   Cantidad == bocadillo.Cantidad &&
                   CompraId == bocadillo.CompraId &&
                   NombreBocadillo == bocadillo.NombreBocadillo &&
                   Precio == bocadillo.Precio &&
                   EqualityComparer<Bocadillo>.Default.Equals(Bocadillo, bocadillo.Bocadillo) &&
                   EqualityComparer<Compra>.Default.Equals(Compra, bocadillo.Compra);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(BocadilloId, Cantidad, CompraId, NombreBocadillo, Precio, Bocadillo, Compra);
        }

    }
}
