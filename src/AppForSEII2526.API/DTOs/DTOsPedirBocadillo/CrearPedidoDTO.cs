using static AppForSEII2526.API.Models.CompraBono;

namespace AppForSEII2526.API.DTOs.DTOs_PedirBocadillo
{
    public class CrearPedidoDTO //post
    {
        public string nombre { get; set; }

        public Metodo_Pago Metodo_Pago { get; set; }

        public string apellido1 { get; set; }
        public string? apellido2 { get; set; }
        public IList<ArticuloPedidoDTO> ArticuloPedido { get; set; }


        public CrearPedidoDTO(string nombre, Metodo_Pago metododepago, string apellido1, string? apellido2, IList<ArticuloPedidoDTO> articulopedido)
        {
            this.nombre = nombre;
            Metodo_Pago = metododepago;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            ArticuloPedido = articulopedido;
        }

        public CrearPedidoDTO()
        {
            ArticuloPedido = new List<ArticuloPedidoDTO>();
        }
        public override bool Equals(object? obj)
        {
            return obj is CrearPedidoDTO dTO &&
                   nombre == dTO.nombre &&
                   Metodo_Pago == dTO.Metodo_Pago &&
                   apellido1 == dTO.apellido1 &&
                   apellido2 == dTO.apellido2 &&
                   ArticuloPedido.SequenceEqual(dTO.ArticuloPedido);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nombre, Metodo_Pago, apellido1, apellido2, ArticuloPedido);
        }
    }
}
