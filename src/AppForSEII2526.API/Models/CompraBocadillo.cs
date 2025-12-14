

namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BocadilloId), nameof(CompraId))]
    public class CompraBocadillo
    {
        public Bocadillo Bocadillo { get; set; }
        public int BocadilloId { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public Compra Compra { get; set; }
        public int CompraId { get; set; }
        public string NombreBocadillo { get; set; }
        [Precision(10, 2)]
        public decimal Precio { get; set; }  
        public List<TipoPan> TipoPan { get; set; } = new List<TipoPan>();


        public CompraBocadillo()
        {
            
        }


        public CompraBocadillo(int bocadilloId, int cantidad, Compra compra)
        {
            BocadilloId = bocadilloId;
            Cantidad = cantidad;
            Compra = compra;
            CompraId = compra.CompraId;
        }

        public CompraBocadillo(int bocadilloId, int cantidad, Compra compra, int compraId, string nombreBocadillo, decimal precio)
        {   
            BocadilloId = bocadilloId;
            Cantidad = cantidad;
            Compra = compra;
            CompraId = compraId;
            NombreBocadillo = nombreBocadillo;
            Precio = precio;
        }

        public CompraBocadillo(Bocadillo bocadillo, Compra compra, int cantidad)
        {
            Bocadillo = bocadillo;
            BocadilloId = bocadillo.Id;
            Compra = compra;
            Cantidad = cantidad;
            CompraId = compra.CompraId;
            NombreBocadillo = bocadillo.nombre;
            Precio = bocadillo.PVP;
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
