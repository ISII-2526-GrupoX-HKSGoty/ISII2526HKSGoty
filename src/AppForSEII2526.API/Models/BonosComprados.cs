namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BonoId), nameof(CompraBonoId))]
    public class BonosComprados
    {
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


    }
}
