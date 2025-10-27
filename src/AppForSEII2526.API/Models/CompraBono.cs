

namespace AppForSEII2526.API.Models
{
    public class CompraBono
    {
        public CompraBono() { }
        public CompraBono( ApplicationUser user, DateTime releaseDate, int nBonos, MetodoPago metodoPago, IList<BonosComprados> bonosComprados)
        {
            User = user;
            ReleaseDate = releaseDate;
            this.nBonos = nBonos;
            this.metodoPago = metodoPago;
            BonosComprados = bonosComprados;
        }

        [Key] 
        public int CompraBonoId { get; set; }

        public ApplicationUser User { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public int nBonos { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public double PrecioTotalBono { get; set; }

        [Required]
        public MetodoPago metodoPago { get; set; }

        public IList<BonosComprados> BonosComprados { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CompraBono bono &&
                   CompraBonoId == bono.CompraBonoId &&
                   EqualityComparer<ApplicationUser>.Default.Equals(User, bono.User) &&
                   ReleaseDate == bono.ReleaseDate &&
                   nBonos == bono.nBonos &&
                   PrecioTotalBono == bono.PrecioTotalBono &&
                   metodoPago == bono.metodoPago &&
                   EqualityComparer<IList<BonosComprados>>.Default.Equals(BonosComprados, bono.BonosComprados);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CompraBonoId, User, ReleaseDate, nBonos, PrecioTotalBono, metodoPago, BonosComprados);
        }

        public enum MetodoPago
        {
            Tarjeta,
            Paypal,
            GooglePay
        }
    }
}
