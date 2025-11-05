

namespace AppForSEII2526.API.DTOs.DTOs_PedirBocadillo
{
    public class DetallesPedidoDTO:CrearPedidoDTO
    {
        public DetallesPedidoDTO()
        {
        }

        public DetallesPedidoDTO(string nombre, Metodo_Pago metodoPago, string apellido1, string? apellido2, DateTime fechaPedido, decimal precioTotal, IList<ArticuloPedidoDTO> articuloPedido) : base(nombre, metodoPago, apellido1, apellido2, articuloPedido)
        {
            FechaPedido = fechaPedido;
            PrecioTotal = precioTotal;
        }

        public DateTime FechaPedido { get; set; }

        public decimal PrecioTotal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetallesPedidoDTO pedido &&
                   base.Equals(obj) &&
                   nombre == pedido.nombre &&
                   Metodo_Pago == pedido.Metodo_Pago &&
                   apellido1 == pedido.apellido1 &&
                   apellido2 == pedido.apellido2 &&
                   EqualityComparer<IList<ArticuloPedidoDTO>>.Default.Equals(ArticuloPedido, pedido.ArticuloPedido) &&
                   FechaPedido == pedido.FechaPedido &&
                   PrecioTotal == pedido.PrecioTotal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), nombre, Metodo_Pago, apellido1, apellido2, ArticuloPedido, FechaPedido, PrecioTotal);
        }
    }
}
