using AppForMovies.UT;
using AppForSEII2526.API.Controllers;
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
                new Bocadillo("Poli", 5f, 20, Tamaño.normal, tipoPan[0]),
                new Bocadillo("Completo", 5f, 20, Tamaño.pequeño, tipoPan[1]),
            };

            ApplicationUser user = new ApplicationUser("Fernando", "Martinez", "Panadero");

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
                    new ArticuloPedidoDTO (1, "Poli",  20, 5f, "Baguette")
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
                articulopedido: new List<ArticuloPedidoDTO>() { new ArticuloPedidoDTO(999, "FalsoBocadillo", 1, 2f, "Normal") }
            );

            var allTests = new List<object[]>
            {
                //new object[] { sinAticulosDto, "Error! Debes seleccionar algun bocadillo" },
                new object[] { noUserDto, "Usuario no registrado" },
                new object[] { metodoNoRegistradoDto, "Método de pago no válido. Usa: Tarjeta, Paypal o GooglePay." },
                new object[] { bocadilloNoExisteDto, "El bocadillo no existe" },
            };
            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestParaCasos_CrearPedido))]
        public async Task CrearPedido_Error_test(CrearPedidoDTO dto, string errorExpected)
        {
            var mock = new Mock<ILogger<PedidoController>>();
            ILogger<PedidoController> logger = mock.Object;
            var controller = new PedidoController(_context, logger);

            var result = await controller.CrearPedido(dto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];
            Assert.StartsWith(errorExpected, errorActual);
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
                new ArticuloPedidoDTO(2, "Completo", 5, 20, "Chapata")
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
                "Fernando",
                Metodo_Pago.Tarjeta,
                "Martinez",
                "Panadero",
                DateTime.Today,
                new List<ArticuloPedidoDTO>()
                {
                        new ArticuloPedidoDTO(2, "Completo", 5, 20, "Chapata")
                },
                10
            );

            Assert.Equal(expectedDetalles, detalles);
        }
    }
}
