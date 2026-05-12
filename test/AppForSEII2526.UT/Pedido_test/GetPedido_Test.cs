using AppForMovies.UT;
using AppForSEII2526.API.Controllers.PedidosControllers;
using AppForSEII2526.API.DTOs.DTOs_PedirBocadillo;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bocadillo;

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
                new Bocadillo(1, "Poli", 5, 20, Tamaño.Normal, tipoPan[0]),
                new Bocadillo(2, "Vegetal", 5, 20, Tamaño.Pequeño, tipoPan[1])
            };

            ApplicationUser user = new ApplicationUser("Fernando", "Martinez", "Panadero", "Fernando.Martinez22@alu.uclm.es");

            var compra = new Compra(user, DateTime.Today, Metodo_Pago.Paypal, new List<CompraBocadillo>());
            compra.BocadillosComprados.Add(new CompraBocadillo(bocadillo[0], compra, 2));

            compra.PrecioTotal = compra.BocadillosComprados.Sum(cb => cb.Precio * cb.Cantidad);

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

            var result = await controller.GetPedido(0);

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

            var expectedPedido = new DetallesPedidoDTO(1,"Fernando", Metodo_Pago.Paypal, "Martinez", "Panadero", DateTime.Today, new List<ArticuloPedidoDTO>(), 10);

            expectedPedido.ArticuloPedido.Add(new ArticuloPedidoDTO(1, "Poli", 2, 5, "Semillas"));

            var result = await controller.GetPedido(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var pedidoDTOActual = Assert.IsType<DetallesPedidoDTO>(okResult.Value);
            var eq = expectedPedido.Equals(pedidoDTOActual);
            Assert.Equal(expectedPedido, pedidoDTOActual);
        }
    }
}
