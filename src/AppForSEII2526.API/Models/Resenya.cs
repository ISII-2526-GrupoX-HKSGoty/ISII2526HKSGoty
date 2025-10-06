
namespace AppForSEII2526.API.Models
{
    public class Resenya
    {
        public string descripcion { get; set; }
        public DateTime fechaPublicacion { get; set;}
        [Key]
        public int Id { get; set; }
        public string nombreUsuario { get; set; }
        public string titulo { get; set; }

        public IList<ResenyaBocadillo> ResenyaBocadillo { get; set; }
        public Resenya() { }

        public Resenya(string descripcion, DateTime fechaPublicacion, int id, string nombreUsuario, string titulo, IList<ResenyaBocadillo> resenyaBocadillo)
        {
            this.descripcion = descripcion;
            this.fechaPublicacion = fechaPublicacion;
            Id = id;
            this.nombreUsuario = nombreUsuario;
            this.titulo = titulo;
            ResenyaBocadillo = resenyaBocadillo;
        }

        public enum Valoracion_General
        {
            Una, Dos, Tres, Cuatro, Cinco
        }

        public override bool Equals(object? obj)
        {
            return obj is Resenya resenya &&
                   descripcion == resenya.descripcion &&
                   fechaPublicacion == resenya.fechaPublicacion &&
                   Id == resenya.Id &&
                   nombreUsuario == resenya.nombreUsuario &&
                   titulo == resenya.titulo &&
                   EqualityComparer<IList<ResenyaBocadillo>>.Default.Equals(ResenyaBocadillo, resenya.ResenyaBocadillo);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(descripcion, fechaPublicacion, Id, nombreUsuario, titulo, ResenyaBocadillo);
        }
    }
}
