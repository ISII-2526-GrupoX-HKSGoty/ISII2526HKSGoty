using static AppForSEII2526.API.Models.CompraBono;

namespace AppForSEII2526.API.DTOs.DTOsCompraBono
{
    public class CompraBonoDTO
    {
        public CompraBonoDTO(string nombre, string apellido1, string apellido2, MetodoPago metodoPago, IList<ItemBonoDTO>items)
        {
            this.nombreCliente = nombre;
            this.apellido1Cliente = apellido1;
            this.apellido2Cliente = apellido2 ?? throw new ArgumentNullException(nameof(apellido2));
            this.metdoPago = metodoPago;
            this.Items = items;
        }
        public CompraBonoDTO()
        {
            Items = new List<ItemBonoDTO>();
        }
        public IList<ItemBonoDTO> Items { get; set; }
        [Required]
        public string nombreCliente { get; set; }
        [Required]
        public string apellido1Cliente { get; set; }
        [Required]
        public string apellido2Cliente { get; set; }
        [Required]
        public MetodoPago metdoPago { get; set; }

    }
}
