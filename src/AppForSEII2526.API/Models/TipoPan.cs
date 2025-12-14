namespace AppForSEII2526.API.Models
{
    public class TipoPan
    {
        public string Nombre { get; set;}

        [Key]
        public int Id { get; set; }
        

        public IList<Bocadillo> Bocadillos { get; set; }

        public TipoPan() { }
        public TipoPan(string nombre)
        {
            Nombre = nombre;
        }
        public TipoPan(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public override bool Equals(object? obj)
        {
            return obj is TipoPan pan &&
                   Nombre == pan.Nombre &&
                   Id == pan.Id;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Id);
        }
    }
}
