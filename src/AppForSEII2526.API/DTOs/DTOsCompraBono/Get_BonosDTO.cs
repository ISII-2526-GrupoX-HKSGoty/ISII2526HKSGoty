namespace AppForSEII2526.API.DTOs.DTOsCompraBono
{
    public class Get_BonosDTO
    {
        public Get_BonosDTO(string nombre, double PVP, int nBocadillos, TipoBocadillo tipo)
        {

            this.Nombre = nombre;
            this.PVP = PVP;
            this.nBocadillos = nBocadillos;
            this.tipoBocadillo = tipo;

        }

        [StringLength(20, ErrorMessage = "Nombre entre 3 y 20 caracteres", MinimumLength = 3)]
        public string Nombre { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public double PVP { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "no acepta valores menores a 0")]
        public int nBocadillos { get; set; }

        public TipoBocadillo tipoBocadillo { get; set; }
    }
}
