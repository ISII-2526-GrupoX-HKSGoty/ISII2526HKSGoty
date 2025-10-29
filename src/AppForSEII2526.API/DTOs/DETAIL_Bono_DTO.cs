using System;
using AppForSEII2526.API.DTOs;

public class DETAIL_Bono_DTO
{
    public DETAIL_Bono_DTO(string nombre, string apellido1, string apellido2, CompraBono.MetodoPago metodopago, double preciototalbono, DateTime fecha, IList<BonosComprados> bonoscomprados)
    {
        this.nombre = nombre;
        this.apellido1 = apellido1;
        this.apellido2 = apellido2;
        this.metodopago = metodopago;
        this.preciototalbono = preciototalbono;
        this.fecha = fecha;
        this.bonoscomprados = bonoscomprados;
    }

    public string nombre { get; set; }

    public string apellido1 { get; set; }

    public string apellido2 { get; set; }

    public CompraBono.MetodoPago  metodopago { get; set; }

    public double preciototalbono { get; set; }

    public DateTime fecha { get; set; }

    public IList<BonosComprados> bonoscomprados { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is DETAIL_Bono_DTO dTO &&
               nombre == dTO.nombre &&
               apellido1 == dTO.apellido1 &&
               apellido2 == dTO.apellido2 &&
               metodopago == dTO.metodopago &&
               preciototalbono == dTO.preciototalbono &&
               fecha == dTO.fecha &&
               EqualityComparer<IList<BonosComprados>>.Default.Equals(bonoscomprados, dTO.bonoscomprados);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(nombre, apellido1, apellido2, metodopago, preciototalbono, fecha, bonoscomprados);
    }
}	




