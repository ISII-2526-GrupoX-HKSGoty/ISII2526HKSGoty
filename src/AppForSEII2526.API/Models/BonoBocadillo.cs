
namespace AppForSEII2526.API.Models
{
    public class BonoBocadillo
    {

        [Key]
        public int BonoId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage ="no acepta valores menores a 0")]
        public int cantidadDisponible { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public int nBocadillos { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Nombre entre 3 y 20 caracteres", MinimumLength = 3)]
        public string nombre { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public double PVP { get; set; }

        public BonoBocadillo(int bonoId, int cantidadDisponible, int nBocadillos, string nombre, double pVP)
        {
            BonoId = bonoId;
            this.cantidadDisponible = cantidadDisponible;
            this.nBocadillos = nBocadillos;
            this.nombre = nombre;
            PVP = pVP;
        }

        public override bool Equals(object? obj)
        {
            return obj is BonoBocadillo bocadillo &&
                   BonoId == bocadillo.BonoId &&
                   cantidadDisponible == bocadillo.cantidadDisponible &&
                   nBocadillos == bocadillo.nBocadillos &&
                   nombre == bocadillo.nombre &&
                   PVP == bocadillo.PVP;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BonoId, cantidadDisponible, nBocadillos, nombre, PVP);
        }
    }
}
