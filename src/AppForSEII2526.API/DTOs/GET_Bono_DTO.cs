
namespace AppForSEII2526.API.DTOs
{
    public class GET_Bono_DTO
    {
        public GET_Bono_DTO(string nombre, double PVP, int numero, TipoBocadillo tipo) {

            this.nombre = nombre;
            this.PVP = PVP;
            this.numero = numero;
            this.tipo = tipo;

        }

        public string nombre { get; set; }

        public double PVP { get; set; }

        public int numero { get; set; }

        public TipoBocadillo tipo { get; set; }

        public override bool Equals(object? obj)
        {
            return obj  is GET_Bono_DTO dTO &&
                   nombre == dTO.nombre &&
                   PVP == dTO.PVP &&
                   numero == dTO.numero &&
                   tipo == dTO.tipo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nombre, PVP, numero, tipo);
        }
    }
}
