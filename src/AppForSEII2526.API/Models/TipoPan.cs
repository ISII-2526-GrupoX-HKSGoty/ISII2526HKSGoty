namespace AppForSEII2526.API.Models
{
    public class TipoPan
    {
        public string Nombre
        {
            get; set;
        }

        public int PanId { get; set; }

        public IList<Bocadillo> Bocadillos { get; set; }

        public TipoPan() { }
        public TipoPan(string nombre)
        {
            Nombre = nombre;
        }

        public override bool Equals(object? obj)
        {
            return obj is TipoPan pan &&
                   Nombre == pan.Nombre &&
                   PanId == pan.PanId;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, PanId);
        }
    }
}
