
namespace AppForSEII2526.API.DTOs
{
    public class Item_DTO
    {
        public Item_DTO(int compraId, int bonoId,string nombre, TipoBocadillo tipoBocadillo, double precio, int cantidad)
        {
            this.compraId = compraId;
            this.bonoId = bonoId;
            this.nombre = nombre;
            TipoBocadillo = tipoBocadillo;
            this.precio = precio;
            this.cantidad = cantidad;
        }
        public int compraId { get; set; }

        public int bonoId { get; set; }

        public string nombre { get; set; }

        public TipoBocadillo TipoBocadillo { get; set; }

        public double precio { get; set; }

        public int cantidad { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Item_DTO dTO &&
                   nombre == dTO.nombre &&
                   EqualityComparer<TipoBocadillo>.Default.Equals(TipoBocadillo, dTO.TipoBocadillo) &&
                   precio == dTO.precio &&
                   cantidad == dTO.cantidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nombre, TipoBocadillo, precio, cantidad);
        }
    }
}
