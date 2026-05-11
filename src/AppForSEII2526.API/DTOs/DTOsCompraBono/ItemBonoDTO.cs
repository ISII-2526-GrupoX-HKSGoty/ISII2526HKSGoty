
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

        public ItemBonoDTO(int id, double PVP, int nBocadillos, string nombreBono, int cantidad, string tipoBocadillo)
        {
            this.bonoId = id;
            this.PVP = PVP;
            this.nBocadillos = nBocadillos;
            this.nombreBono = nombreBono;
            this.cantidad = cantidad;
            this.tipoBocadillo = tipoBocadillo;
        }

        public int bonoId { get; set; }
        public double PVP { get; set; }
        public int nBocadillos { get; set; }
        public string nombreBono { get; set; }
        public int cantidad { get; set; }
        public string tipoBocadillo { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ItemBonoDTO dTO &&
                   PVP == dTO.PVP &&
                   nBocadillos == dTO.nBocadillos &&
                   nombreBono == dTO.nombreBono &&
                   cantidad == dTO.cantidad &&
                   tipoBocadillo == dTO.tipoBocadillo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PVP, nBocadillos, nombreBono, cantidad, tipoBocadillo);
        }
    }
}
