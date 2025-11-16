using AppForSEII2526.API.DTOs;
using System;

public class POST_Bono_DTO
{
	public POST_Bono_DTO()
	{
        ItemCompra = new List<Item_Bono_DTO>();
    }

    public POST_Bono_DTO(string nombre, string apellido1, string apellido2, CompraBono.MetodoPago metodoPago, IList<Item_Bono_DTO> ItemCompra)
    {
        this.nombre = nombre?? throw new ArgumentNullException(nameof(nombre));
        this.apellido1 = apellido1 ?? throw new ArgumentNullException(nameof(apellido1));
        this.apellido2 = apellido2 ?? throw new ArgumentNullException(nameof(apellido2));
        this.metodoPago = metodoPago; 
        this.ItemCompra = ItemCompra ?? throw new ArgumentNullException(nameof(ItemCompra));
    }
    [Required]
    public string nombre { get; set; }
    [Required]
    public string apellido1 { get; set; }
    [Required]
    public string apellido2 { get; set; }
    [Required]
    public CompraBono.MetodoPago metodoPago { get; set; }

    public IList<Item_Bono_DTO> ItemCompra { get; set; }

    private double PrecioTotal
    {
        get
        {
            return ItemCompra.Sum(d => d.cantidad * d.precio);

        }

    }

    public override bool Equals(object? obj)
    {
        return obj is POST_Bono_DTO dTO &&
               nombre == dTO.nombre &&
               apellido1 == dTO.apellido1 &&
               apellido2 == dTO.apellido2 &&
               metodoPago == dTO.metodoPago &&
               ItemCompra.SequenceEqual(dTO.ItemCompra) &&
               PrecioTotal == dTO.PrecioTotal;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(nombre, apellido1, apellido2, metodoPago, ItemCompra, PrecioTotal);
    }
}
