using AppForSEII2526.API.Models;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Bocadillo
{
    
    public Bocadillo() { }

    public Bocadillo(int id, string nombre, int pvp, int stock, TipoPan tipoPan, Tamaño tamaño, IList<ResenyaBocadillo> resenyaBocadillos)
    {
        Id = id;
        this.nombre = nombre;
        this.PVP = pvp;
        this.stock = stock;
        this.tipoPan = tipoPan;
        this.tamaño = tamaño;
        ResenyaBocadillos = resenyaBocadillos;
    }
    public Bocadillo(string nombre, float pVP, int stock, Tamaño tamaño)
    {
        this.nombre = nombre;
        PVP = pVP;
        this.stock = stock;
        tamaño = tamaño;
        ResenyaBocadillos = new List<ResenyaBocadillo>();
        ComprasDelBocadillo = new List<CompraBocadillo>();
        tipoPan = ;
    }

    [Key]
    public int Id { get; set; }

    [StringLength(30, ErrorMessage = "El nombre no debe superar los 30 caracteres")]
    public string nombre { get; set; }

    [Precision(10,2)]
    public float PVP { get; set; }

    public int stock { get; set; }

    public TipoPan tipoPan { get; set; }

    public Tamaño tamaño { get; set; }
    public IList<ResenyaBocadillo> ResenyaBocadillos { get; set; }

    public IList<CompraBocadillo> ComprasDelBocadillo { get; set; }

    public Bocadillo(string nombre, float pVP, int stock, TipoPan tipoPan, Tamaño tamaño)
    {
        this.nombre = nombre;
        PVP = pVP;
        this.stock = stock;
        this.tipoPan = tipoPan;
        this.tamaño = tamaño;
    }
    public Bocadillo(string nombre, float pVP, int stock, TipoPan tipoPan, Tamaño tamaño, IList<CompraBocadillo> comprasDelBocadillo*/)
    {
        this.nombre = nombre;
        PVP = pVP;
        this.stock = stock;
        this.tipoPan = tipoPan;
        this.tamaño = tamaño;
        ComprasDelBocadillo = comprasDelBocadillo;
    }

    public override bool Equals(object? obj)
    {
        return obj is Bocadillo bocadillo &&
               Id == bocadillo.Id &&
               nombre == bocadillo.nombre &&
               PVP == bocadillo.PVP &&
               stock == bocadillo.stock &&
               EqualityComparer<TipoPan>.Default.Equals(tipoPan, bocadillo.tipoPan) &&
               tamaño == bocadillo.tamaño &&
               EqualityComparer<IList<ResenyaBocadillo>>.Default.Equals(ResenyaBocadillos, bocadillo.ResenyaBocadillos) &&
               EqualityComparer<IList<CompraBocadillo>>.Default.Equals(ComprasDelBocadillo, bocadillo.ComprasDelBocadillo);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, nombre, PVP, stock, tipoPan, tamaño, ResenyaBocadillos, ComprasDelBocadillo);
    }
}
