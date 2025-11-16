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

            // Inserta datos previos para el test GET
            context.Producto_Compras.Add(new Producto_Compra
            {
                Id = 1,
                ProductoId = 1,
                CompraId = 1,
                Cantidad = 2,
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
            Assert.NotEmpty(okResult.Value);
        }
    }
}
