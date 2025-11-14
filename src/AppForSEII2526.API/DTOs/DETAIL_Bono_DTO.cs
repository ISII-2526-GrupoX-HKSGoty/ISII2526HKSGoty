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

    public DateTime fecha { get; set; }

}	




