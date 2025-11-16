

using System.Threading.Tasks.Dataflow;

namespace AppForSEII2526.API.Models
{
    public class BonoBocadillo
    {
        public BonoBocadillo() { }
        public BonoBocadillo(int bonoId, int cantidadDisponible, int nBocadillos, string nombre, double pVP, IList<BonosComprados> bonosComprados, TipoBocadillo tipoBocadillo)
        {
            BonoId = bonoId;
            this.cantidadDisponible = cantidadDisponible;
            this.nBocadillos = nBocadillos;
            this.nombre = nombre;
            PVP = pVP;
            BonosComprados = bonosComprados;
            TipoBocadillo = tipoBocadillo;
        }

        public BonoBocadillo(int cantidadDisponible, int nBocadillos, string nombre, double pVP, TipoBocadillo tipoBocadillo)
        {
            this.cantidadDisponible = cantidadDisponible;
            this.nBocadillos = nBocadillos;
            this.nombre = nombre;
            this.PVP = pVP;
            this.TipoBocadillo = tipoBocadillo;
        }

        [Key]
        public int BonoId { get; set; }

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

        public IList<BonosComprados> BonosComprados { get; set; }

        public TipoBocadillo TipoBocadillo { get; set; }

    }
}
