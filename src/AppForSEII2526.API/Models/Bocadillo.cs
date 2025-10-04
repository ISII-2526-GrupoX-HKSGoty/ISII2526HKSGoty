using System;

public class Bocadillo
{

    public Bocadillo() { }

    public Bocadillo(int id, string nombre, int pvp, int stock, TipoPan tipoPan)
    {
        Id = id;
        this.nombre = nombre;
        this.pvp = pvp;
        this.stock = stock;
        this.tipoPan = tipoPan;
    }

    public int Id { get; set; }

    [StringLength(30, ErrorMessage = "El nombre no debe superar los 30 caracteres")]
    public string nombre { get; set; }

    [Precision(10,2)]
    public int pvp { get; set; }

    public int stock { get; set; }

    public TipoPan tipoPan { get; set; }

    public Tamaño tamaño { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Bocadillo bocadillo &&
               Id == bocadillo.Id &&
               nombre == bocadillo.nombre &&
               pvp == bocadillo.pvp &&
               stock == bocadillo.stock &&
               EqualityComparer<TipoPan>.Default.Equals(tipoPan, bocadillo.tipoPan);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, nombre, pvp, stock, tipoPan);
    }
}
