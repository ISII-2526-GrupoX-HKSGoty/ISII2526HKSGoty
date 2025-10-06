

namespace AppForSEII2526.API.Models
{
    public class TipoBocadillo
    {
        public TipoBocadillo() { }
        public TipoBocadillo(int idTipo, string nombreTipo, IList<BonoBocadillo> bonoBocadillos)
        {
            this.idTipo = idTipo;
            this.nombreTipo = nombreTipo;
            BonoBocadillos = bonoBocadillos;
        }

        [Key]
        public int idTipo { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Nombre entre 3 y 20 caracteres", MinimumLength=3)]
        public string nombreTipo { get; set; }

        public IList<BonoBocadillo> BonoBocadillos { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is TipoBocadillo bocadillo &&
                   idTipo == bocadillo.idTipo &&
                   nombreTipo == bocadillo.nombreTipo &&
                   EqualityComparer<IList<BonoBocadillo>>.Default.Equals(BonoBocadillos, bocadillo.BonoBocadillos);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(idTipo, nombreTipo, BonoBocadillos);
        }
    }
}
