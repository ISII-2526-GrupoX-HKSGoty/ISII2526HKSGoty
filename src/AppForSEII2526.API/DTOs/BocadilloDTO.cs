namespace AppForSEII2526.API.DTOs
{
    public class BocadilloDTO//GET
    {
        public string Nombre { get; set; }
        public Tamaño Tamaño { get; set; }
        public string TipoPanNombre { get; set; }

        [Precision(10, 2)]
        public decimal PVP { get; set; }
        public int Id { get; set; }

        public BocadilloDTO()
        {

        }

        public BocadilloDTO(int id, string nombre, string tipoPanNombre, Tamaño tamaño, decimal PVP)
        {
            Id = id;
            Nombre = nombre;
            Tamaño = tamaño;
            TipoPanNombre = tipoPanNombre;
            this.PVP = PVP;

        }

        public override bool Equals(object? obj)
        {
            return obj is BocadilloDTO dTO &&
                   Id == dTO.Id &&
                   Nombre == dTO.Nombre &&
                   Tamaño == dTO.Tamaño &&
                   TipoPanNombre == dTO.TipoPanNombre &&
                   PVP == dTO.PVP;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Tamaño, TipoPanNombre, PVP);
        }
    }
}
