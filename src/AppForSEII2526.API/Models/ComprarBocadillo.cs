namespace AppForSEII2526.API.Models
{
    public class ComprarBocadillo
    {
        public int BocadilloId { get; set; }
        public int Cantidad { get; set; }
        public int CompraId { get; set; }
        public string NombreBocadillo { get; set; }
        public decimal Precio { get; set; }


    }
}
