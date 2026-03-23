using AppForMovies.UT;
using AppForSEII2526.API.Controllers.BonosContollers;
using AppForSEII2526.API.DTOs.DTOsCompraBono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppForSEII2526.UT.Bono_test
{
    public class PostBono_UT : AppForMovies4SqliteUT
    {
        private DateTime fecha = DateTime.Today;

        public PostBono_UT()
        {

            var usuario = new ApplicationUser("Jose", "Juan", "Juan", "Jose@Juan");

            var tipoBocadillo = new List<TipoBocadillo>()
            {
                new TipoBocadillo("Bacon"),
                new TipoBocadillo("Vegetal")
            };

            var bonos = new List<BonoBocadillo>()
            {
                new BonoBocadillo(50,15,"Bono1",20,tipoBocadillo[0]),
                new BonoBocadillo(50,15,"Bono2",20,tipoBocadillo[1])
            };

            var metodoPago = CompraBono.MetodoPago.Tarjeta;

            var compra = new CompraBono(usuario, fecha, metodoPago, new List<BonosComprados>());

            var bonosComprados = new List<BonosComprados>()
            {
                new BonosComprados(bonos[0],compra,2),
                new BonosComprados(bonos[1],compra,1)
            };

            _context.ApplicationUser.Add(usuario);
            _context.AddRange(tipoBocadillo);
            _context.AddRange(bonos);
            _context.Add(compra);
            _context.AddRange(bonosComprados);
            _context.SaveChanges();

        }

        [Fact]
        [Trait("DataBase", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CreateCompraBono_Success_Test()
        {
            var mock = new Mock<ILogger<CompraBonosController>>();
            ILogger<CompraBonosController> logger = mock.Object;

            var controller  = new CompraBonosController(_context, logger);

            var CompraDTO = new CompraBonoDTO("Jose", "Juan", "Juan", CompraBono.MetodoPago.Tarjeta, new List<ItemBonoDTO>()
            {
                new ItemBonoDTO(20,15,"Bono1",2,"Bacon"),
                new ItemBonoDTO(20,15,"Bono2",1,"Vegetal")
            });

            var expectedDetailDTO = new Detail_CompraBonoDTO(2, fecha, 60, "Jose", "Juan", "Juan", CompraBono.MetodoPago.Tarjeta,new List<ItemBonoDTO>()
            {
                new ItemBonoDTO(20,15,"Bono1",2,"Bacon"),
                new ItemBonoDTO(20,15,"Bono2",1,"Vegetal")
            });

            var result = await controller.createCompraBonos(CompraDTO);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actual = Assert.IsType<Detail_CompraBonoDTO>(createdResult.Value);

            Assert.Equal(expectedDetailDTO, actual);
        }

    }
}
