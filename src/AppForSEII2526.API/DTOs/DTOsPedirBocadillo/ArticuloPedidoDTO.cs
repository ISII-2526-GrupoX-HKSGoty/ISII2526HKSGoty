
namespace AppForSEII2526.API.DTOs.DTOs_PedirBocadillo
{
    public class ArticuloPedidoDTO
    {
        public int Id { get; set; }
        public string nombreBocadillo { get; set; }

        public int Cantidad { get; set; }
        public decimal PVP { get; set; }
        public string TipoPan { get; set; }

        public ArticuloPedidoDTO()
        {
        }

        public ArticuloPedidoDTO(int id, string nombreBocadillo, int cantidad, decimal pVP, string tipoPan)
        {
            Id = id;
            this.nombreBocadillo = nombreBocadillo;
            Cantidad = cantidad;
            PVP = pVP;
            TipoPan = tipoPan;
        }

        public override bool Equals(object? obj)
        {
            return obj is ArticuloPedidoDTO dTO &&
                   nombreBocadillo == dTO.nombreBocadillo &&
                   Cantidad == dTO.Cantidad &&
                   PVP == dTO.PVP &&
                   TipoPan == dTO.TipoPan;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nombreBocadillo, Cantidad, PVP, TipoPan);
        }
    }
}
