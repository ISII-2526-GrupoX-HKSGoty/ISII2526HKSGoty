using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.DTOs_PedirBocadillo;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppForSEII2526.API.Models.CompraBono;

namespace AppForSEII2526.UT.PedidoContrller_test
{
    public class GetPedido_test : AppForMovies4SqliteUT
    {
        public GetPedido_test()
        {
            var tipoPan = new List<TipoPan>()
            {
                new TipoPan("Semillas"),
                new TipoPan("Integral")
            };

            var bocadillo = new List<Bocadillo>()
            {
                new Bocadillo("Atun", 2, 15,tipoPan[0], Tamaño.normal),
                new Bocadillo("Vegetal", 3, 15, tipoPan[1], Tamaño.pequeño)
            };

            ApplicationUser user = new ApplicationUser("Fernando", "Martinez", "Panadero", "Fernando@uclm.es");

            var compra = new Compra(DateTime.Today, new List<CompraBocadillo>(), MetodoPago.Paypal, user);
            compra.CompraBocadillos.Add(new CompraBocadillo(bocadillo[0], compra, 2));

            compra.PrecioTotal = compra.CompraBocadillos.Sum(cb => cb.Precio * cb.Cantidad);

            _context.Add(user);
            _context.AddRange(bocadillo);
            _context.AddRange(tipoPan);
            _context.Add(compra);
            _context.SaveChanges();
        }


        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]


        public async Task GetPedidos_NotFound_Test()
        {
            var mock = new Mock<ILogger<PedidoController>>();
            ILogger<PedidoController> logger = mock.Object;

            var controller = new PedidoController(_context, logger);

            var result = await controller.GetPedidos(0);

            Assert.IsType<NotFoundResult>(result);
        }




        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]

        public async Task GetPedidos_Found_test()
        {
            var mock = new Mock<ILogger<PedidoController>>();
            ILogger<PedidoController> logger = mock.Object;

            var controller = new PedidoController(_context, logger);

            var expectedPedido = new DetallesPedidoDTO("Fernando", MetodoPago.Paypal, "Martinez", "Panadero", DateTime.Today, 4, new List<ArticuloPedidoDTO>());

            expectedPedido.ArticuloPedido.Add(new ArticuloPedidoDTO(1, "Atun", 2, 2.0f, "Semillas"));


            var result = await controller.GetPedidos(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var pedidoDTOActual = Assert.IsType<DetaPedidoDDTO>(okResult.Value);
            var eq = expectedPedido.Equals(pedidoDTOActual);
            Assert.Equal(expectedPedido, pedidoDTOActual);
        }
    }
}
