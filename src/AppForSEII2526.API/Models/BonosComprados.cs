
namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BonoId), nameof(CompraBonoId))]
    public class BonosComprados
    {
        public BonosComprados() { }
        public BonosComprados(BonoBocadillo bonoBocadillo, int bonoId, CompraBono compraBono, int compraBonoId, int cantidad, double precio)
        {
            BonoBocadillo = bonoBocadillo;
            BonoId = bonoId;
            CompraBono = compraBono;
            CompraBonoId = compraBonoId;
            Cantidad = cantidad;
            Precio = precio;
        }

        public BonosComprados(BonoBocadillo bonoBocadillo, CompraBono compraBono, int cantidad)
        {
            BonoBocadillo = bonoBocadillo;
            BonoId = bonoBocadillo.BonoId;
            CompraBono = compraBono;
            CompraBonoId = compraBono.CompraBonoId;
            Cantidad = cantidad;
        }

        public BonoBocadillo BonoBocadillo { get; set; }
        public int BonoId { get; set; }

        public CompraBono CompraBono { get; set; }
        public int CompraBonoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "no acepta valores menores a 1")]
        public int Cantidad { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public double Precio { get; set; }


    }
}
