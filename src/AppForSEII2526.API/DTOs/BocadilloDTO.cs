namespace AppForSEII2526.API.DTOs
{
    public class BocadilloDTO//GET
    {
        public string Nombre { get; set; }
        public Tamaño Tamaño { get; set; }
        public string TipoPan { get; set; }
        public decimal PVP { get; set; }
        public int Id { get; set; }

        public BocadilloDTO()
        {

        }

        public BocadilloDTO(int id, string nombre, Tamaño tamaño, string tipoPan, decimal PVP)
        {
            Id = id;
            Nombre = nombre;
            Tamaño = tamaño;
            TipoPan = tipoPan;
            this.PVP = PVP;

        }

        public override bool Equals(object? obj)
        {
            return obj is BocadilloDTO dTO &&
                   Id == dTO.Id &&
                   Nombre == dTO.Nombre &&
                   Tamaño == dTO.Tamaño &&
                   TipoPan == dTO.TipoPan &&
                   PVP == dTO.PVP;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Tamaño, TipoPan, PVP);
        }
    }
}
