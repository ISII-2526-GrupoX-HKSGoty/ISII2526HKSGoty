




namespace AppForSEII2526.API.DTOs.DTOs_PedirBocadillo
{
    public class DetallesPedidoDTO:CrearPedidoDTO
    {
        public DetallesPedidoDTO()
        {
        }

        public DetallesPedidoDTO(string nombre, Metodo_Pago metodoPago, string apellido1, string? apellido2, DateTime fechaPedido, List<ArticuloPedidoDTO> articuloPedido, decimal precioTotal) : base(nombre, metodoPago, apellido1, apellido2, articuloPedido)
        {
            FechaPedido = fechaPedido;
            PrecioTotal = precioTotal;
        }

        public DateTime FechaPedido { get; set; }
        public decimal PrecioTotal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetallesPedidoDTO dTO &&
                   base.Equals(obj) &&
                   nombre == dTO.nombre &&
                   Metodo_Pago == dTO.Metodo_Pago &&
                   apellido1 == dTO.apellido1 &&
                   apellido2 == dTO.apellido2 &&
                   ArticuloPedido.SequenceEqual(dTO.ArticuloPedido) &&
                   FechaPedido == dTO.FechaPedido &&
                   PrecioTotal == dTO.PrecioTotal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), nombre, Metodo_Pago, apellido1, apellido2, ArticuloPedido, FechaPedido, PrecioTotal);
        }
    }
}
