using AppForMovies.UT;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.UT_CU_CompraBonos
{
    public class DetailBonos_UT : AppForMovies4SqliteUT
    {
        public DetailBonos_UT()
        {
            var usuario = new ApplicationUser("JoseJuan","Jose","Juan");

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


            var compraBono = new CompraBono(usuario, DateTime.Today, metodoPago,new List<BonosComprados>());

            var bonosComprados = new List<BonosComprados>()
            {
                new BonosComprados(bonos[0],compraBono),
                new BonosComprados(bonos[1],compraBono)
            };

            compraBono.BonosComprados.Add(bonosComprados[0]);
            compraBono.BonosComprados.Add(bonosComprados[1]);

            compraBono.nBonos = bonosComprados[0].Cantidad + bonosComprados[1].Cantidad;


            //compraBono.PrecioTotalBono = bonosComprados[0].Precio * bonosComprados[0].Cantidad + bonosComprados[1].Precio * bonosComprados[1].Cantidad;
            compraBono.PrecioTotalBono = 0;

            _context.Add(usuario);
            _context.AddRange(tipoBocadillo);
            _context.AddRange(bonos);
            _context.Add(compraBono);
            _context.AddRange(bonosComprados);
            _context.SaveChanges();
        }


        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task getCompra_NotFound_UT()
        {
            var mock = new Mock<ILogger<BonosController>>();
            ILogger<BonosController> logger = mock.Object;

            var conroller = new BonosController(_context, logger);

            var result = await conroller.getCompraBono(0);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task getCompra_Found_UT()
        {

            var mock = new Mock<ILogger<BonosController>>();
            ILogger<BonosController> logger = mock.Object;

            var conroller = new BonosController(_context, logger);


            var expectedCompra = new DETAIL_Bono_DTO(1, "JoseJuan", "Jose", "Juan", CompraBono.MetodoPago.Tarjeta, 0, DateTime.Today, new List<Item_Bono_DTO>());

            expectedCompra.ItemCompra.Add(new Item_Bono_DTO(1, 1, "Bono1", "Bacon", 3.5, 5));
            expectedCompra.ItemCompra.Add(new Item_Bono_DTO(1, 2, "Bono2", "Vegetal", 6.0, 10));

            var result = await conroller.getCompraBono(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualCompra = Assert.IsType<DETAIL_Bono_DTO>(okResult.Value);
            var eq = expectedCompra.Equals(actualCompra);

            

            Assert.Equal(expectedCompra, actualCompra);
        }
    }
}
