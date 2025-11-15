using AppForMovies.UT;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.DTOs_PedirBocadillo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppForSEII2526.API.Models.CompraBono;

namespace AppForSEII2526.UT.Pedido_test
{
    public class PostPedido_Test
    {
        public class Post_CrearPedido_test : AppForMovies4SqliteUT
        {
            private readonly ApplicationUser _user;
            private readonly MetodoPago _metodo;
            private readonly Bocadillo _bocadillo;
            private readonly TipoPan _tipoPan;

            public Post_CrearPedido_test()
            {
                _tipoPan = new TipoPan { Nombre = "Integral" };

                _metodo = MetodoPago.Tarjeta;

                _user = new ApplicationUser("Fernando", "Martinez", "Panadero") { Id = Guid.NewGuid().ToString() };

                _bocadillo = new Bocadillo
                {
                    Id = 10,
                    nombre = "Pollo",
                    PVP = 4.5M,
                    stock = 5,
                    tamaño = Tamaño.normal,
                    tipoPan = _tipoPan,
                    ResenyaBocadillos = new List<ResenyaBocadillo>() // evita SQLite NOT NULL constraint failed
                };

                // Añadimos todas las entidades al contexto de pruebas y guardamos.
                _context.AddRange(_tipoPan, _metodo, _user, _bocadillo);
                _context.SaveChanges();
            }

            // Método que proporciona casos de prueba parametrizados para errores en CreatePedido.
            // Cada elemento devuelto es un array con: (CrearPedidoDTO dto, string mensajeEsperado).
            public static IEnumerable<object[]> TestParaCasos_CrearPedido()
            {
                var sinAticulosDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: MetodoPago.Tarjeta,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO>()
                );

                var sinStockDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: MetodoPago.Tarjeta,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO> { new ArticuloPedidoDTO { Id = 10, nombreBocadillo = "Pollo", TipoPan = "Integral", Cantidad = 0, PVP = 4.5M } }
                );

                var noUserDto = new CrearPedidoDTO
                (
                    nombre: "NoExiste",
                    metododepago: MetodoPago.Tarjeta,
                    apellido1: "X",
                    apellido2: null,
                    articulopedido: new List<ArticuloPedidoDTO> { new ArticuloPedidoDTO { Id = 10, nombreBocadillo = "Pollo", TipoPan = "Integral", Cantidad = 1, PVP = 4.5M } }
                );

                var metodoNoRegistradoDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: MetodoPago.MetodoInexistente,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO> { new ArticuloPedidoDTO { Id = 10, nombreBocadillo = "Pollo", TipoPan = "Integral", Cantidad = 1, PVP = 4.5M } }
                );

                var stockInsuficienteDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: MetodoPago.Tarjeta,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO> { new ArticuloPedidoDTO { Id = 10, nombreBocadillo = "Pollo", TipoPan = "Integral", Cantidad = 10, PVP = 4.5M } }
                ); 

                var bocadilloNoExisteDto = new CrearPedidoDTO
                (
                    nombre: "Fernando",
                    metododepago: MetodoPago.Tarjeta,
                    apellido1: "Martinez",
                    apellido2: "Panadero",
                    articulopedido: new List<ArticuloPedidoDTO> { new ArticuloPedidoDTO { Id = 999, nombreBocadillo = "NoExiste", TipoPan = "Integral", Cantidad = 1, PVP = 1.0M } }
                );

                var allTests = new List<object[]>
            {
                new object[] { sinAticulosDto, "Error! Debes seleccionar algun bocadillo" },
                new object[] { sinStockDto, "La cantidad es obligatoria y debe ser mayor que 0." },
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

                // Comparaciones robustas: comprobamos datos relevantes (evitamos comparar Fecha/Id generados runtime).
                Assert.Equal(_user.nombre, detalles.nombre);
                Assert.Equal(_user.apellido1, detalles.apellido1);
                Assert.Equal(_metodo.ToString, detalles.Metodo_Pago.ToString);
                Assert.Equal(1, _context.Compras.Count()); // se ha creado una compra en la BBDD de pruebas
                Assert.Equal(2, detalles.ArticuloPedido.First().Cantidad);
            }
        }
    } 
}
