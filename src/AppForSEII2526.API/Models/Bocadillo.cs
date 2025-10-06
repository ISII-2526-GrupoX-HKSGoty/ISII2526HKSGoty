using System;

public class Bocadillo
{

    public Bocadillo() { }

    public Bocadillo(int id, string nombre, int PVP, int stock, TipoPan tipoPan)
    {
        Id = id;
        this.nombre = nombre;
        this.PVP = PVP;
        this.stock = stock;
        this.tipoPan = tipoPan;
    }
    [Key]
    public int Id { get; set; }

    [StringLength(30, ErrorMessage = "El nombre no debe superar los 30 caracteres")]
    public string nombre { get; set; }

    [Precision(10,2)]
    public decimal PVP { get; set; }

    public int stock { get; set; }

    public TipoPan tipoPan { get; set; }

    public Tamaño tamaño { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Bocadillo bocadillo &&
               Id == bocadillo.Id &&
               nombre == bocadillo.nombre &&
               PVP == bocadillo.PVP &&
               stock == bocadillo.stock &&
               EqualityComparer<TipoPan>.Default.Equals(tipoPan, bocadillo.tipoPan);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, nombre, PVP, stock, tipoPan);
    }
}
