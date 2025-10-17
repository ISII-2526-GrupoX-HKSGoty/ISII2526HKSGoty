

namespace AppForSEII2526.API.Models
{
    [PrimaryKey (nameof( BocadilloId), nameof(ResenyaId))]
    public class ResenyaBocadillo
    {
        
        public int BocadilloId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "no acepta valores menores a 1 o superiores a 10")]
        public int Puntuacion { get; set; }
        public int ResenyaId { get; set; }

        public Bocadillo Bocadillo { get; set; }
        public Resenya Resenya { get; set; }
        public ResenyaBocadillo() { }

        public ResenyaBocadillo(int bocadilloId, int puntuacion, int resenyaId, Bocadillo bocadillo, Resenya resenya)
        {
            BocadilloId = bocadilloId;
            Puntuacion = puntuacion;
            ResenyaId = resenyaId;
            Bocadillo = bocadillo;
            Resenya = resenya;
        }

        public override bool Equals(object? obj)
        {
            return obj is ResenyaBocadillo bocadillo &&
                   BocadilloId == bocadillo.BocadilloId &&
                   Puntuacion == bocadillo.Puntuacion &&
                   ResenyaId == bocadillo.ResenyaId &&
                   EqualityComparer<Bocadillo>.Default.Equals(Bocadillo, bocadillo.Bocadillo) &&
                   EqualityComparer<Resenya>.Default.Equals(Resenya, bocadillo.Resenya);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BocadilloId, Puntuacion, ResenyaId, Bocadillo, Resenya);
        }
    }
}
