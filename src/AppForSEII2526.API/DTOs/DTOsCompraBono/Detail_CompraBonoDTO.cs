using static AppForSEII2526.API.Models.CompraBono;

namespace AppForSEII2526.API.DTOs.DTOsCompraBono
{
    public class Detail_CompraBonoDTO : CompraBonoDTO
    {

        public Detail_CompraBonoDTO(int id, DateTime compraT,double precioTotal,string nombre, string apellido1, string apellido2, MetodoPago metodoPago, IList<ItemBonoDTO> items):base(nombre,apellido1,apellido2,metodoPago,items)
        {

            this.ID = id;
            this.CompraT = compraT;
            this.precioTotal = precioTotal;

        }
        public int ID { get; set; }
        public double precioTotal { get; set; }
        public DateTime CompraT { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Detail_CompraBonoDTO dTO &&
                   base.Equals(obj) &&
                   ID == dTO.ID &&
                   precioTotal == dTO.precioTotal &&
                   CompraT.Date == dTO.CompraT.Date;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Items, nombreCliente, apellido1Cliente, apellido2Cliente, metdoPago, ID, precioTotal, CompraT);
        }
    }
}
