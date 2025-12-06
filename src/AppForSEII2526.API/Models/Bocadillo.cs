using AppForSEII2526.API.Models;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Bocadillo
{
    
    public Bocadillo() { }

    public Bocadillo(int id, string nombre, float pvp, int stock, Tamaño tamaño, TipoPan tipoPan)
    {
        Id = id;
        this.nombre = nombre;
        PVP = pvp;
        this.stock = stock;
        this.tamaño = tamaño;
        this.tipoPan = tipoPan;
        ResenyaBocadillos = new List<ResenyaBocadillo>();
        ComprasDelBocadillo = new List<CompraBocadillo>();
    }

    public Bocadillo(string nombre, float pvp, int stock, Tamaño tamaño, TipoPan tipoPan)
    {
        this.nombre = nombre;
        PVP = pvp;
        this.stock = stock;
        this.tamaño = tamaño;
        this.tipoPan = tipoPan;
        ResenyaBocadillos = new List<ResenyaBocadillo>();
        ComprasDelBocadillo = new List<CompraBocadillo>();
    }

    public Bocadillo(string nombre, float pvp, int stock, Tamaño tamaño)
    {
        this.nombre = nombre;
        PVP = pvp;
        this.stock = stock;
        this.tamaño = tamaño;
        ResenyaBocadillos = new List<ResenyaBocadillo>();
        ComprasDelBocadillo = new List<CompraBocadillo>();
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

    public List<CompraBocadillo> ComprasDelBocadillo { get; set; }

    public Bocadillo(int id, string nombre, float pvp, int stock, TipoPan tipoPan, Tamaño tamaño, IList<ResenyaBocadillo> resenyaBocadillos)
    {
        Id = id;
        this.nombre = nombre;
        PVP = pvp;
        this.stock = stock;
        this.tipoPan = tipoPan;
        this.tamaño = tamaño;
        ResenyaBocadillos = resenyaBocadillos;
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
               EqualityComparer<List<CompraBocadillo>>.Default.Equals(ComprasDelBocadillo, bocadillo.ComprasDelBocadillo);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, nombre, PVP, stock, tipoPan, tamaño, ResenyaBocadillos, ComprasDelBocadillo);
    }
    
}
