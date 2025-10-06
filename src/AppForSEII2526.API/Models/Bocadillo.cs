using System;

public class Bocadillo
{

    public Bocadillo() { }

   
    [Key]
    public int Id { get; set; }

    [StringLength(30, ErrorMessage = "El nombre no debe superar los 30 caracteres")]
    public string nombre { get; set; }

    [Precision(10,2)]
    public decimal PVP { get; set; }

    public int stock { get; set; }

    public TipoPan tipoPan { get; set; }

    public Tamaño tamaño { get; set; }

    public IList<CompraBocadillo> ComprasDelBocadillo { get; set; }

    public Bocadillo(int id, string nombre, decimal pVP, int stock, TipoPan tipoPan, Tamaño tamaño, IList<CompraBocadillo> comprasDelBocadillo)
    {
        Id = id;
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
               EqualityComparer<IList<CompraBocadillo>>.Default.Equals(ComprasDelBocadillo, bocadillo.ComprasDelBocadillo);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, nombre, PVP, stock, tipoPan, tamaño, ComprasDelBocadillo);
    }
}
