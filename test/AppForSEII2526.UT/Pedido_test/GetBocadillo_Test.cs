using AppForMovies.UT;
using AppForSEII2526.API.Controllers.PedidosControllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using System.Net;
using static Bocadillo;


namespace AppForSEII2526.UT.Pedido_test
{
    public class GetBocadillo_Test : AppForMovies4SqliteUT
    {
        public GetBocadillo_Test()
        {
            var tipoPan = new List<TipoPan>() {
            new TipoPan("Baguette"),
            new TipoPan("Integral"),
            new TipoPan("Molde"),
            new TipoPan("Chapata"),
            new TipoPan("Cereal"),
            new TipoPan("Sin gluten")
            };

            var bocadillos = new List<Bocadillo>()
            {
                new Bocadillo("Vegetal",        5, 20, Tamaño.Normal, tipoPan[1]),
                new Bocadillo("Atún",           5, 20, Tamaño.Pequeño, tipoPan[0]),
                new Bocadillo("Jamón y queso",  5, 20, Tamaño.Normal, tipoPan[3]),
                new Bocadillo("Politecnico",    5, 20, Tamaño.Pequeño, tipoPan[2]),
                new Bocadillo("Completo",       5, 20, Tamaño.Normal, tipoPan[4]),
                new Bocadillo("Trifasico",      5, 20, Tamaño.Pequeño, tipoPan[5]), 
                new Bocadillo("Bufalo",         5, 20, Tamaño.Normal, tipoPan[0]),
                new Bocadillo("Sumarino",       5, 20, Tamaño.Pequeño, tipoPan[1])

            };
            
            ApplicationUser user = new ApplicationUser("Fernando", "Martinez", "Panadero", "Fernando.Martinez22@alu.uclm.es");

            _context.Add(user);
            _context.AddRange(bocadillos);
            _context.AddRange(tipoPan);
            _context.SaveChanges();

        }
        public static IEnumerable<object[]> TestCasosPara_GetBocadillo_Test_Ok()
        {
            var bocadilloDTOs = new List<BocadilloDTO>()
            { 
                new BocadilloDTO (1, "Vegetal",         "Integral", Tamaño.Normal, 5),         //0
                new BocadilloDTO (2, "Atún",            "Baguette", Tamaño.Pequeño, 5),        //1
                new BocadilloDTO (3, "Jamón y queso",   "Chapata", Tamaño.Normal, 5),          //2
                new BocadilloDTO (4, "Politecnico",     "Molde", Tamaño.Pequeño, 5),           //3
                new BocadilloDTO (5, "Completo",        "Cereal", Tamaño.Normal, 5),           //4
                new BocadilloDTO (6, "Trifasico",       "Sin gluten", Tamaño.Pequeño, 5),      //5
                new BocadilloDTO (7, "Bufalo",          "Baguette", Tamaño.Normal, 5),         //6
                new BocadilloDTO (8, "Sumarino",        "Integral", Tamaño.Pequeño, 5)         //7
            };

            var tc1 = bocadilloDTOs; // ordenados por nombre

            var tc2 = new List<BocadilloDTO> { bocadilloDTOs[1], bocadilloDTOs[3], bocadilloDTOs[5], bocadilloDTOs[7]}; // tamaño pequeño

            var tc3 = new List<BocadilloDTO> { bocadilloDTOs[2]}; // tipo de pan "Chapata"

            var tc4 = new List<BocadilloDTO> { bocadilloDTOs[6]}; // tamaño normal + tipo de pan "Baguette"


            var allTests = new List<object[]>
            {
                new object[] { null,            null,      tc1 },
                new object[] { Tamaño.Pequeño,  null,      tc2 },
                new object[] { null,            "Chapata", tc3 },
                new object[] { Tamaño.Normal,   "Baguette",tc4 },
            };
            return allTests;
        }
        [Theory]
        [MemberData(nameof(TestCasosPara_GetBocadillo_Test_Ok))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBocadillosParaPedir_OK(Tamaño? tamaño, string? tipoPan, List<BocadilloDTO> expectedBocadillo)
        {
            var controller = new BocadillosController(_context, null);

            var resultado = await controller.GetBocadillosParaPedir(tamaño, tipoPan);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var bocadilloDTOsActual = Assert.IsType<List<BocadilloDTO>>(okResult.Value);
            Assert.Equal(expectedBocadillo, bocadilloDTOsActual);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetBocadillosParaPedir_badrequest_test()
        {
            var mock = new Mock<ILogger<BocadillosController>>();
            ILogger<BocadillosController> logger = mock.Object;
            var controller = new BocadillosController(_context, logger);

            var resultado = await controller.GetBocadillosParaPedir(Tamaño.Normal, "NoExiste");
        }
    }
}
