
namespace AppForSEII2526.API.Models
{
    public class CompraBono
    {
        
        [Key] 
        public int CompraBonoId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public int nBonos { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public double PrecioTotalBono { get; set; }

        [Required]
        public MetodoPago metodoPago { get; set; }

        public CompraBono(int compraBonoId, ApplicationUser user, DateTime releaseDate, int nBonos, double precioTotalBono, MetodoPago metodoPago)
        {
            CompraBonoId = compraBonoId;
            User = user;
            ReleaseDate = releaseDate;
            this.nBonos = nBonos;
            PrecioTotalBono = precioTotalBono;
            this.metodoPago = metodoPago;
        }

        public override bool Equals(object? obj)
        {
            return obj is CompraBono bono &&
                   CompraBonoId == bono.CompraBonoId &&
                   EqualityComparer<ApplicationUser>.Default.Equals(User, bono.User) &&
                   ReleaseDate == bono.ReleaseDate &&
                   nBonos == bono.nBonos &&
                   PrecioTotalBono == bono.PrecioTotalBono &&
                   metodoPago == bono.metodoPago;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CompraBonoId, User, ReleaseDate, nBonos, PrecioTotalBono, metodoPago);
        }
    }
}
