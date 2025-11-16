


namespace AppForSEII2526.API.DTOs.DTOs_PedirBocadillo
{
    public class DetallesPedidoDTO:CrearPedidoDTO
    {
        public DetallesPedidoDTO()
        {
        }

        public DetallesPedidoDTO( string nombre, Metodo_Pago metodoPago, string apellido1, string? apellido2, DateTime fechaPedido, IList<ArticuloPedidoDTO> articuloPedido, float precioTotal) : base(nombre, metodoPago, apellido1, apellido2, articuloPedido)
        {
            FechaPedido = fechaPedido;
            PrecioTotal = precioTotal;
        }

        public DateTime FechaPedido { get; set; }
        public float PrecioTotal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetallesPedidoDTO dTO &&
                   base.Equals(obj) &&
                   nombre == dTO.nombre &&
                   Metodo_Pago == dTO.Metodo_Pago &&
                   apellido1 == dTO.apellido1 &&
                   apellido2 == dTO.apellido2 &&
                   EqualityComparer<IList<ArticuloPedidoDTO>>.Default.Equals(ArticuloPedido, dTO.ArticuloPedido) &&
                   Id == dTO.Id &&
                   FechaPedido == dTO.FechaPedido;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(nombre);
            hash.Add(Metodo_Pago);
            hash.Add(apellido1);
            hash.Add(apellido2);
            hash.Add(ArticuloPedido);
            hash.Add(Id);
            hash.Add(FechaPedido);
            return hash.ToHashCode();
        }
    }
}
