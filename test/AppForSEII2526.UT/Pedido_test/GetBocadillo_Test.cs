using AppForMovies.UT;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System.Net;


namespace AppForSEII2526.UT.Pedido_test
{
    public class GetPedido_Test : AppForMovies4SqliteUT
    {
        public GetPedido_Test()
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
                new Bocadillo("Vegetal", 5, 20, tipoPan[1], Tamaño.normal),
                new Bocadillo("Atún", 5, 15, tipoPan[0], Tamaño.pequeño),
                new Bocadillo("Jamón y queso", 7, 10, tipoPan[3], Tamaño.normal),
                new Bocadillo("Politecnico", 4, 5, tipoPan[2], Tamaño.pequeño),
                new Bocadillo("Completo", 9, 8, tipoPan[4], Tamaño.normal),
                new Bocadillo("Trifasico", 3, 12, tipoPan[5], Tamaño.pequeño), 
                new Bocadillo("Bufalo", 2, 7, tipoPan[0], Tamaño.normal),
                new Bocadillo("Sumarino", 6, 9, tipoPan[1], Tamaño.pequeño)
            };
            
            ApplicationUser user = new ApplicationUser("Fernando", "Martinez", "Panadero");

            _context.Add(user);
            _context.AddRange(bocadillos);
            _context.AddRange(tipoPan);
            _context.SaveChanges();

        }
        public static IEnumerable<object[]> TestCasosPara_GetPedido_Test_Ok()
        {
            var tipoPan = new List<TipoPan>() {
            new TipoPan("Baguette"),
            new TipoPan("Integral"),
            new TipoPan("Molde"),
            new TipoPan("Chapata"),
            new TipoPan("Cereal"),
            new TipoPan("Sin gluten")
            };
            var bocadilloDTOs = new List<BocadilloDTO>()
            {
                new BocadilloDTO(1, "Vegetal", "Molde", Tamaño.normal, 5),          //0
                new BocadilloDTO(2, "Atún", "Baguette", Tamaño.pequeño, 5),         //1
                new BocadilloDTO(3, "Jamón y queso", "Molde", Tamaño.normal, 5),    //2
                new BocadilloDTO (4, "Politecnico", "Baguette", Tamaño.normal, 5),  //3
                new BocadilloDTO (5, "Completo", "Baguette", Tamaño.normal, 5),     //4
                new BocadilloDTO (6, "Trifasico", "Baguette", Tamaño.normal, 5),    //5
                new BocadilloDTO (7, "Bufalo", "Chapata", Tamaño.normal, 5),        //6
                new BocadilloDTO (8, "Sumarino", "Chapata", Tamaño.pequeño, 5)      //7

            };


            var tc1 = bocadilloDTOs.OrderBy(b => b.Nombre).ToList(); // ordenados por nombre

            var tc2 = new List<BocadilloDTO> { bocadilloDTOs[1], bocadilloDTOs[3], bocadilloDTOs[7], } // tamaño pequeño
            .OrderBy(b => b.Nombre).ToList();

            var tc3 = new List<BocadilloDTO> { bocadilloDTOs[6], bocadilloDTOs[7] } // tipo de pan "Chapata"
            .OrderBy(b => b.Nombre).ToList();

            var tc4 = new List<BocadilloDTO> { bocadilloDTOs[3], bocadilloDTOs[4], bocadilloDTOs[5]} // tamaño normal + tipo de pan "Baguette"
            .OrderBy(b => b.Nombre).ToList();


            var allTests = new List<object[]>
            {
                new object[] { null,                         null,      tc1 },
                new object[] { Tamaño.pequeño.ToString(),    null,      tc2 }, // filtro por tamaño pequeño
                new object[] { null,                         "Chapata", tc3 },
                new object[] { Tamaño.normal.ToString(),     "Baguette",tc4 }, // filtro por tamaño normal + Integral
            };
            return allTests;
        }
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetBocadilloPedir_SinResultados_DevuelveSiNoFunciona()
        {   
            var m = new Mock<ILogger<BocadillosController>>();
            ILogger<BocadillosController> logger = m.Object;
            var controller = new BocadillosController(_context, logger);

            var filtroTamNoValido = "Grande";
            var filtroPanNoValido = "PanInexist";

            var result = await controller.GetBocadillosParaPedir(filtroTamNoValido, filtroPanNoValido);

            var noFunciona = Assert.IsType<NotFoundObjectResult>(result);
            var mensaje = Assert.IsType<string>(noFunciona.Value);
            Assert.Equal("No hay bocadillos con estos requisitos", mensaje);
            Assert.Equal((int)HttpStatusCode.NotFound, noFunciona.StatusCode);


        }
    }
}
