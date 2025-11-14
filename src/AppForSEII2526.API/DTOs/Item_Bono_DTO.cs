
namespace AppForSEII2526.API.DTOs
{
    public class Item_Bono_DTO
    {
        public Item_Bono_DTO(int compraId, int bonoId,string nombre, string tipoBocadillo, double precio, int cantidad)
        {
            this.compraId = compraId;
            this.bonoId = bonoId;
            this.nombre = nombre;
            this.tipoBocadillo = tipoBocadillo;
            this.precio = precio;
            this.cantidad = cantidad;
        }
        public int compraId { get; set; }

        public int bonoId { get; set; }

        public string nombre { get; set; }

        public string tipoBocadillo { get; set; }

        public double precio { get; set; }

        public int cantidad { get; set; }

    }
}
