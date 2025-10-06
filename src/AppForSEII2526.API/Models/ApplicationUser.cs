using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {

    public ApplicationUser() { }
    public ApplicationUser(string nombre, string apellido1, string apellido2)
    {
        this.nombre = nombre;
        this.apellido1 = apellido1;
        this.apellido2 = apellido2;
    }

    public string nombre { get; set; }

    public string apellido1 { get; set; }

    public string apellido2 { get; set; }
}