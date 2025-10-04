
namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BonoId), nameof(CompraBonoId))]
    public class BonosComprados
    {
        public BonosComprados(BonoBocadillo bonoBocadillo, int bonoId, CompraBono compraBono, int compraBonoId, int cantidad, int precio)
        {
            BonoBocadillo = bonoBocadillo;
            BonoId = bonoId;
            CompraBono = compraBono;
            CompraBonoId = compraBonoId;
            Cantidad = cantidad;
            Precio = precio;
        }

        public BonoBocadillo BonoBocadillo { get; set; }
        public int BonoId { get; set; }

        public CompraBono CompraBono { get; set; }
        public int CompraBonoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "no acepta valores menores a 1")]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public int Precio { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BonosComprados comprados &&
                   EqualityComparer<BonoBocadillo>.Default.Equals(BonoBocadillo, comprados.BonoBocadillo) &&
                   BonoId == comprados.BonoId &&
                   EqualityComparer<CompraBono>.Default.Equals(CompraBono, comprados.CompraBono) &&
                   CompraBonoId == comprados.CompraBonoId &&
                   Cantidad == comprados.Cantidad &&
                   Precio == comprados.Precio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BonoBocadillo, BonoId, CompraBono, CompraBonoId, Cantidad, Precio);
        }
    }
}
