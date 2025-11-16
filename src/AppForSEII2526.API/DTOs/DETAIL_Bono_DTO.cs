using System;
using AppForSEII2526.API.DTOs;

public class DETAIL_Bono_DTO: POST_Bono_DTO
{
    public DETAIL_Bono_DTO(int id, string nombre, string apellido1, string apellido2, CompraBono.MetodoPago metodopago, double preciototalbono, DateTime fecha, IList<Item_Bono_DTO> bonoscomprados)
        :base(nombre, apellido1, apellido2, metodopago, bonoscomprados)
    {
        this.id = id;
        this.preciototalbono = preciototalbono;
        this.fecha = fecha;
    }

    public int id { get; set; }

    public double preciototalbono { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime fecha { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is DETAIL_Bono_DTO dTO &&
               base.Equals(obj) &&
               id == dTO.id &&
               preciototalbono == dTO.preciototalbono &&
               fecha.Date == dTO.fecha.Date;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(base.GetHashCode());
        hash.Add(nombre);
        hash.Add(apellido1);
        hash.Add(apellido2);
        hash.Add(metodoPago);
        hash.Add(ItemCompra);
        hash.Add(id);
        hash.Add(preciototalbono);
        hash.Add(fecha);
        return hash.ToHashCode();
    }
}	




