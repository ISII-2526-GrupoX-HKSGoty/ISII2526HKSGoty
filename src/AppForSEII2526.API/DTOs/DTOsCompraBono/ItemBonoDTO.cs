namespace AppForSEII2526.API.DTOs.DTOsCompraBono
{
    public class ItemBonoDTO
    {
        public ItemBonoDTO(double PVP,int nBocadillos, string nombreBono,int cantidad, string tipoBocadillo)
        {
            this.PVP = PVP;
            this.nBocadillos = nBocadillos;
            this.nombreBono = nombreBono;
            this.cantidad = cantidad;
            this.tipoBocadillo = tipoBocadillo;
        }

        public double PVP { get; set; }
        public int nBocadillos { get; set; }
        public string nombreBono { get; set; }
        public int cantidad { get; set; }
        public string tipoBocadillo { get; set; }
    }
}
