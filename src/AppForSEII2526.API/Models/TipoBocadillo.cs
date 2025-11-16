

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

        public TipoBocadillo(string nombreTipo)
        {
            this.nombreTipo = nombreTipo;
        }

        [Key]
        public int idTipo { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Nombre entre 3 y 20 caracteres", MinimumLength=3)]
        public string nombreTipo { get; set; }

        public IList<BonoBocadillo> BonoBocadillos { get; set; }

    }
}
