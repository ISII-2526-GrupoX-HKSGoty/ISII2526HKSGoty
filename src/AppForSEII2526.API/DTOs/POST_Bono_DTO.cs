using AppForSEII2526.API.DTOs;
using System;

public class POST_Bono_DTO
{
	public POST_Bono_DTO()
	{
        ItemCompra = new List<GET_Bono_DTO>();
    }

    public POST_Bono_DTO(string nombre, string apellido1, string apellido2, CompraBono.MetodoPago metodoPago, IList<GET_Bono_DTO> ItemCompra)
    {
        nombre = nombre?? throw new ArgumentNullException(nameof(nombre));
        apellido1 = apellido1 ?? throw new ArgumentNullException(nameof(apellido1));
        apellido2 = apellido2 ?? throw new ArgumentNullException(nameof(apellido2));
        this.metodoPago = metodoPago; 
        ItemCompra = ItemCompra ?? throw new ArgumentNullException(nameof(ItemCompra));
    }
    [Required]
    public string nombre { get; set; }
    [Required]
    public string apellido1 { get; set; }
    [Required]
    public string apellido2 { get; set; }
    [Required]
    public CompraBono.MetodoPago metodoPago { get; set; }

    public IList<GET_Bono_DTO> ItemCompra { get; set; }

    private double PrecioTotal
    {
        get
        {
            return ItemCompra.Sum(d => d.PVP * d.numero);

        }

    }
}
