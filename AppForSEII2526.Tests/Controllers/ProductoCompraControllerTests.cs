using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AppForSEII2526.API.Data;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;

namespace AppForSEII2526.Tests.Controllers
{
    public class ProductoCompraControllerTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ProductoCompraControllerTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestProductoCompra")
                .Options;
        }

        [Fact]
        public async Task GetProductoCompras_ReturnsAll()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                context.Productos_Compras.Add(new Producto_Compra { Id = 1, Cantidad = 2, ProductoId = 1, CompraId = 1, PVP = 5.5m });
                context.SaveChanges();

                var controller = new ProductoCompraController(context);
                var result = await controller.GetProductoCompras();

                var okResult = Assert.IsType<ActionResult<IEnumerable<Producto_Compra>>>(result);
                Assert.NotEmpty(okResult.Value);
            }
        }
    }
}
