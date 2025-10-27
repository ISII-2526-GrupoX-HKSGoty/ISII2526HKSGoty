
namespace AppForSEII2526.API.DTOs
{
    public class GET_CrearResenaDTO
    {

        public GET_CrearResenaDTO(string nombre, decimal pVP, TipoPan tipoPan, Tamaño tamaño)
        {
            this.nombre = nombre;
            PVP = pVP;
            this.tipoPan = tipoPan;
            this.tamaño = tamaño;
        }

        public string nombre { get; set; }
        public decimal PVP { get; set; }
        public TipoPan tipoPan { get; set; }

        public Tamaño tamaño { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is GET_CrearResenaDTO dTO &&
                   nombre == dTO.nombre &&
                   PVP == dTO.PVP &&
                   EqualityComparer<TipoPan>.Default.Equals(tipoPan, dTO.tipoPan) &&
                   tamaño == dTO.tamaño;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nombre, PVP, tipoPan, tamaño);
        }
    }
}
