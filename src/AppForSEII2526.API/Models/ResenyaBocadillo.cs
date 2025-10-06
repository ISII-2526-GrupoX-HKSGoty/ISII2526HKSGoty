

namespace AppForSEII2526.API.Models
{
    [PrimaryKey (nameof( BocadilloId), nameof(ResenyaId))]
    public class ResenyaBocadillo
    {
        
        public int BocadilloId { get; set; }
        [Range(1, 5, ErrorMessage = "no acepta valores menores a 1 o superiores a 5")]
        public int Puntiación { get; set; }
        public int ResenyaId { get; set; }

        public Bocadillo Bocadillo { get; set; }
        public Resenya Resenya { get; set; }
        public ResenyaBocadillo() { }

        public ResenyaBocadillo(int bocadilloId, int puntiación, int resenyaId, Bocadillo bocadillo, Resenya resenya)
        {
            BocadilloId = bocadilloId;
            Puntiación = puntiación;
            ResenyaId = resenyaId;
            Bocadillo = bocadillo;
            Resenya = resenya;
        }

        public override bool Equals(object? obj)
        {
            return obj is ResenyaBocadillo bocadillo &&
                   BocadilloId == bocadillo.BocadilloId &&
                   Puntiación == bocadillo.Puntiación &&
                   ResenyaId == bocadillo.ResenyaId &&
                   EqualityComparer<Bocadillo>.Default.Equals(Bocadillo, bocadillo.Bocadillo) &&
                   EqualityComparer<Resenya>.Default.Equals(Resenya, bocadillo.Resenya);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BocadilloId, Puntiación, ResenyaId, Bocadillo, Resenya);
        }
    }
}
