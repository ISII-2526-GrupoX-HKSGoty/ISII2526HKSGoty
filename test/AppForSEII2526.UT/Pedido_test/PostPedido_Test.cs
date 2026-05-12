using AppForMovies.UT;
using AppForSEII2526.API.Controllers.PedidosControllers;
using AppForSEII2526.API.DTOs.DTOs_PedirBocadillo;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static AppForSEII2526.API.Models.CompraBono;
using static Bocadillo;

namespace AppForSEII2526.UT.Pedido_test
{
    public class PostPedido_Test : AppForMovies4SqliteUT
    {
        public PostPedido_Test()
        {
            var tipoPan = new List<TipoPan>()
                {
                new TipoPan("Baguette"),
                new TipoPan("Capata")
                };

            var bocadillo = new List<Bocadillo>()
            {
                new Bocadillo("Poli", 5, 20, Tamaño.Normal, tipoPan[0]),
                new Bocadillo("Completo", 5, 20, Tamaño.Pequeño, tipoPan[1]),
            };

            ApplicationUser user = new ApplicationUser("Fernando", "Martinez", "Panadero", "Fernando.Martinez22@alu.uclm.es");

            var compra = new Compra(user, DateTime.Today, Metodo_Pago.Tarjeta, new List<CompraBocadillo>());

            compra.BocadillosComprados.Add(new CompraBocadillo(bocadillo[0], compra, 2));

            _context.ApplicationUser.Add(user);
            //_context.Add(user);
            _context.AddRange(tipoPan);
            _context.AddRange(bocadillo);
            _context.Add(compra);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestParaCasos_CrearPedido()
        {
            var bocadillo = new List<ArticuloPedidoDTO>()
                {
                    new ArticuloPedidoDTO (1, "Poli",  2, 5, "Baguette")
                };

            var sinAticulosDto = new CrearPedidoDTO
            (
                nombre: "Fernando",
                metododepago: Metodo_Pago.Tarjeta,
                apellido1: "Martinez",
                apellido2: "Panadero",
                articulopedido: new List<ArticuloPedidoDTO>()
            );

            var noUserDto = new CrearPedidoDTO
            (
                nombre: "NoExiste",
                metododepago: Metodo_Pago.Tarjeta,
                apellido1: "X",
                apellido2: null,
                articulopedido: bocadillo
            );

            var metodoNoRegistradoDto = new CrearPedidoDTO
            (
                nombre: "Fernando",
                metododepago: (Metodo_Pago)999,
                apellido1: "Martinez",
                apellido2: "Panadero",
                articulopedido: bocadillo
            );

            var bocadilloNoExisteDto = new CrearPedidoDTO
            (
                nombre: "Fernando",
                metododepago: Metodo_Pago.Tarjeta,
                apellido1: "Martinez",
                apellido2: "Panadero",
                articulopedido: new List<ArticuloPedidoDTO>() { new ArticuloPedidoDTO(999, "FalsoBocadillo", 1, 2, "Normal") }
            );

            var demasiadoBocadillos = new CrearPedidoDTO
            (
                nombre: "Fernando",
                metododepago: Metodo_Pago.Tarjeta,
                apellido1: "Martinez",
                apellido2: "Panadero",
                articulopedido: new List<ArticuloPedidoDTO>() { new ArticuloPedidoDTO(1, "Poli", 8, 5, "Baguette") }
            );

            var allTests = new List<object[]>
            {
                //new object[] { sinAticulosDto, "Error! Debes seleccionar algun bocadillo" },
                new object[] { noUserDto, "Error: El nombre y apellido introducidos no corresponden a un usuario registrado." },
                new object[] { metodoNoRegistradoDto, "Método de pago no válido. Usa: Tarjeta, Paypal o GooglePay." },
                new object[] { bocadilloNoExisteDto, "Error! El bocadillo no existe" },
                //new object[] { demasiadoBocadillos, "Error!, no nos quedan panes para realizar tu pedido" }

            };
            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestParaCasos_CrearPedido))]
        public async Task CrearPedido_Error_test(CrearPedidoDTO pedidoDTO, string errorEsperado)
        {
            var mock = new Mock<ILogger<PedidoController>>();
            ILogger<PedidoController> logger = mock.Object;
            var controller = new PedidoController(_context, logger);

            var result = await controller.CrearPedido(pedidoDTO);

            var badRequestResult = Assert.IsAssignableFrom<ObjectResult>(result);


            if (badRequestResult.Value is ValidationProblemDetails problemDetails)
            {
                var errorActual = problemDetails.Errors.First().Value[0];
                Assert.StartsWith(errorEsperado, errorActual);
            }
            else if (badRequestResult.Value is string errorMessage)
            {
                Assert.StartsWith(errorEsperado, errorMessage);
            }
            else
            {
                Assert.True(false, "Unexpected error response type");
            }
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearPedido_Success_test()
        {
            var mock = new Mock<ILogger<PedidoController>>();
            ILogger<PedidoController> logger = mock.Object;
            var controller = new PedidoController(_context, logger);

            var item = new List<ArticuloPedidoDTO>()
            {
                new ArticuloPedidoDTO(2, "Completo", 2, 5, "Chapata")
            };

            var pedidoDto = new CrearPedidoDTO
            (
                nombre: "Fernando",
                metododepago: Metodo_Pago.Tarjeta,
                apellido1: "Martinez",
                apellido2: "Panadero",
                articulopedido: item
            );

            var result = await controller.CrearPedido(pedidoDto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var detalles = Assert.IsType<DetallesPedidoDTO>(created.Value);

            var expectedDetalles = new DetallesPedidoDTO
            (
                1,
                "Fernando",
                Metodo_Pago.Tarjeta,
                "Martinez",
                "Panadero",
                DateTime.Today,
                new List<ArticuloPedidoDTO>()
                {
                        new ArticuloPedidoDTO(2, "Completo", 2, 5, "Chapata")
                },
                10
            );

            Assert.Equal(expectedDetalles, detalles);
            
        }
    }
}
