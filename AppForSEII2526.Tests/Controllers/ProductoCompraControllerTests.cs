using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Data;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.Tests.Controllers
{
    public class ProductoCompraControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // bd nueva por test
                .Options;

            var context = new ApplicationDbContext(options);

            // Insertar Producto requerido
            var producto = new Producto
            {
                Id = 1,
                Nombre = "Camiseta",
                Precio = 15.99m,
                Stock = 100,
                TipoProductoId = 1
            };
            context.Productos.Add(producto);

            // Insertar Compra requerida
            var compra = new Compra
            {
                CompraId = 1,
                FechaCompra = DateTime.Now,
                Metodo_Pago = Metodo_Pago.Tarjeta,
                PrecioTotal = 32,
                nBocadillos = 2
            };
            context.Compras.Add(compra);

            // Insertar Producto_Compra relacionado
            context.Producto_Compras.Add(new Producto_Compra
            {
                Id = 1,
                ProductoId = producto.Id,
                CompraId = compra.CompraId,
                Cantidad = 2
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetProductoCompras_ReturnsAll()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new ProductoCompraController(context);

            // Act
            var result = await controller.GetProductoCompras();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Producto_Compra>>>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<Producto_Compra>>(okResult.Value);
            Assert.NotEmpty(value);
        }
    }
}
