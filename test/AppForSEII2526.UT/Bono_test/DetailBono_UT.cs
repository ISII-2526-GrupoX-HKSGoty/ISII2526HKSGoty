using AppForMovies.UT;
using AppForSEII2526.API.Controllers.BonosContollers;
using AppForSEII2526.API.DTOs.DTOsCompraBono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.Bono_test
{
    public class DetailBono_UT : AppForMovies4SqliteUT
    {

        public DetailBono_UT()
        {

            var usuario = new ApplicationUser("Jose", "Juan", "Juan", "Juan@jose.com");

            var tipoBocadillo = new List<TipoBocadillo>()
            {

                new TipoBocadillo("Bacon"),
                new TipoBocadillo("Vegetal")

            };

            var bonos = new List<BonoBocadillo>()
            {
                new BonoBocadillo(50,10,"Bono1",15,tipoBocadillo[0]),
                new BonoBocadillo(50,10,"Bono2",15,tipoBocadillo[1])
            };

            var metodoPago = CompraBono.MetodoPago.Tarjeta;

            var compra = new CompraBono(usuario, DateTime.Today, metodoPago, new List<BonosComprados>());

            var bonosComprados = new List<BonosComprados>()
            {
                new BonosComprados(bonos[0],compra,2),
                new BonosComprados(bonos[1],compra,1)
            };

            compra.BonosComprados.Add(bonosComprados[0]);
            compra.BonosComprados.Add(bonosComprados[1]);
            compra.nBonos = 3;
            compra.PrecioTotalBono = 30;

            _context.Add(usuario);
            _context.AddRange(tipoBocadillo);
            _context.AddRange(bonos);
            _context.Add(compra);
            _context.AddRange(bonosComprados);
            _context.SaveChanges();


        }

        [Fact]
        [Trait("DataBase", "WithoutFixture")]
        [Trait("LevelTesting", "UnitTest")]
        public async Task getCompraBono_NotFound_UT()
        {
            var mock = new Mock<ILogger<CompraBonosController>>();
            ILogger<CompraBonosController> logger = mock.Object;

            var controller = new CompraBonosController(_context, logger);

            var result = await controller.getCompra(0);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("DataBase", "WithoutFixture")]
        [Trait("LevelTesting", "UnitTest")]
        public async Task getCompraBono_Ok_UT()
        {
            var mock = new Mock<ILogger<CompraBonosController>>();
            ILogger<CompraBonosController> logger = mock.Object;

            var controller = new CompraBonosController(_context, logger);

            var expected = new Detail_CompraBonoDTO(1, DateTime.Today, "Jose", "Juan", "Juan", CompraBono.MetodoPago.Tarjeta, 30, new List<ItemBonoDTO>()
            {
                new ItemBonoDTO(15, 10, "Bono1", 2, "Bacon"),
                new ItemBonoDTO(15, 10, "Bono2", 1, "Vegetal")
            });
            var result = await controller.getCompra(1);
            var actual = Assert.IsType<Detail_CompraBonoDTO>(((OkObjectResult)result).Value);
            var eq = expected.Equals(actual);
            Assert.IsType<OkObjectResult>(result);

        }

    }
}
