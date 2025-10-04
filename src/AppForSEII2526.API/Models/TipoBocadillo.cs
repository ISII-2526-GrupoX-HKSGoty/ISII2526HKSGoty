
namespace AppForSEII2526.API.Models
{
    public class TipoBocadillo
    {

        [Key]
        public int idTipo { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Nombre entre 3 y 20 caracteres", MinimumLength=3)]
        public string nombreTipo { get; set; }

        public TipoBocadillo(int idTipo, string nombreTipo)
        {
            this.idTipo = idTipo;
            this.nombreTipo = nombreTipo;
        }

        public override bool Equals(object? obj)
        {
            return obj is TipoBocadillo bocadillo &&
                   idTipo == bocadillo.idTipo &&
                   nombreTipo == bocadillo.nombreTipo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(idTipo, nombreTipo);
        }
    }
}
