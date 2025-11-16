using AppForMovies.UT;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.UT_CU_CompraBonos
{
    public class PostBonos_UT : AppForMovies4SqliteUT
    {
        
        public PostBonos_UT()
        {
            var usuario = new ApplicationUser("JoseJuan", "Jose", "Juan");


            var tipoBocadillo = new List<TipoBocadillo>()
            {
                new TipoBocadillo("Bacon"),
                new TipoBocadillo("Vegetal")
            };

            var bonos = new List<BonoBocadillo>()
            {
                new BonoBocadillo(20,5,"Bono1",3.5,tipoBocadillo[0]),
                new BonoBocadillo(15,10,"Bono2",6.0,tipoBocadillo[1])
            };

            var metodoPago = CompraBono.MetodoPago.Tarjeta;

            var compraBono = new CompraBono(usuario, DateTime.Today, metodoPago, new List<BonosComprados>());
            
            compraBono.BonosComprados.Add(new BonosComprados(bonos[0], compraBono, 2));

            _context.Add(usuario);
            _context.AddRange(tipoBocadillo);
            _context.AddRange(bonos);
            _context.Add(compraBono);
            _context.SaveChanges();
        }


    }
}
