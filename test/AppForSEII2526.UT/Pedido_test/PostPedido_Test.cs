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
    public class PostPedido_Test
    {
        public class Post_CrearPedido_test : AppForMovies4SqliteUT
        {
            private readonly ApplicationUser _user;
            private readonly Metodo_Pago _metodo;
            private readonly Bocadillo _bocadillo;
            private readonly TipoPan _tipoPan;

            public Post_CrearPedido_test()
            {
                _tipoPan = new TipoPan {PanId=1, Nombre = "Integral" };

                _metodo = Metodo_Pago.Tarjeta;

                _user = new ApplicationUser("Fernando", "Martinez", "Panadero") { Id = Guid.NewGuid().ToString() };

                _bocadillo = new Bocadillo
                {
                    Id = 10,
                    nombre = "Politecnico",
                    PVP = 4.5M,
                    stock = 5,
                    tamaño = Tamaño.normal,
                    tipoPan = _tipoPan,
                    ResenyaBocadillos = new List<ResenyaBocadillo>() 
                };

                var _compra = new Compra(_user, DateTime.Today, _metodo, new List<CompraBocadillo>());

                _compra.BocadillosComprados.Add(new CompraBocadillo(_bocadillo, _compra, 2));

                _context.ApplicationUser.Add(_user);
                _context.AddRange(_tipoPan);
                _context.AddRange(_bocadillo);
                _context.Add(_compra);
                _context.SaveChanges();
            }

            public static IEnumerable<object[]> TestParaCasos_CrearPedido()
            {
                var bocadillo = new List<ArticuloPedidoDTO>()
                {
                    new ArticuloPedidoDTO { Id = 10, nombreBocadillo = "Politecnico", TipoPan = "Normal", Cantidad = 4, PVP = 4.5M }
                };

                var sinAticulosDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: Metodo_Pago.Tarjeta,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO>()
                );

                var sinCantidadDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: Metodo_Pago.Tarjeta,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO> { new ArticuloPedidoDTO { Id = 10, nombreBocadillo = "Pollo", TipoPan = "Integral", Cantidad = 0, PVP = 4.5M } }
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

                var stockInsuficienteDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: Metodo_Pago.Tarjeta,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO> { new ArticuloPedidoDTO { Id = 10, nombreBocadillo = "Pollo", TipoPan = "Integral", Cantidad = 1000, PVP = 4.5M } }
                );


                var bocadilloNoExisteDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: Metodo_Pago.Tarjeta,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO>(){new ArticuloPedidoDTO(999, "FalsoBocadillo", 1, 2M, "Normal")}
                );

                var allTests = new List<object[]>
            {
                new object[] { sinAticulosDto, "Error! Debes seleccionar algun bocadillo" },
                new object[] { sinCantidadDto, "La cantidad es obligatoria y debe ser mayor que 0." },
                new object[] { noUserDto, "Error! Nombre y/o apellidos no registrados." },
                new object[] { metodoNoRegistradoDto, "El método de pago 'MetodoInexistente' no está registrado." },
                new object[] { stockInsuficienteDto, "Error! se han pedido 10 bocadillos, pero no hay suficientes" },
                new object[] { bocadilloNoExisteDto, "El bocadillo 999 no existe." },
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

                var dto = new CrearPedidoDTO
                (
                    nombre: _user.nombre,
                    apellido1: _user.apellido1,
                    apellido2: _user.apellido2,
                    metododepago: _metodo,
                    articulopedido: new List<ArticuloPedidoDTO> { new ArticuloPedidoDTO { Id = _bocadillo.Id, nombreBocadillo = _bocadillo.nombre, TipoPan = _tipoPan.Nombre, Cantidad = 2, PVP = _bocadillo.PVP } }
                );

                var result = await controller.CrearPedido(dto);

                var created = Assert.IsType<CreatedAtActionResult>(result);
                var detalles = Assert.IsType<DetallesPedidoDTO>(created.Value);

                Assert.Equal(_user.nombre, detalles.nombre);
                Assert.Equal(_user.apellido1, detalles.apellido1);
                Assert.Equal(_metodo, detalles.Metodo_Pago);
                Assert.Equal(1, _context.Compras.Count());
                Assert.Equal(2, detalles.ArticuloPedido.First().Cantidad);
            }
        }
    } 
}
