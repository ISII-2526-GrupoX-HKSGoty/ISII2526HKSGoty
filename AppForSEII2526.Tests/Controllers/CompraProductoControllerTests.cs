using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Data;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore.InMemory;


namespace AppForSEII2526.Tests.Controllers
{
    public class CompraProductoControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDbCompraProducto")
                .Options;

            var context = new ApplicationDbContext(options);
            context.Compra_Productos.Add(new Compra_Producto
            {
                Id = 1,
                IdProducto = 1,
                IdCompra = 1,
                Cantidad = 2,
                PrecioUnitario = 9.99m
            });
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetCompraProductos_ReturnsCompraProductos()
        {
            var context = GetDbContext();
            var controller = new CompraProductoController(context);

            var result = await controller.GetCompraProductos();

            var okResult = Assert.IsType<ActionResult<IEnumerable<Compra_Producto>>>(result);
            Assert.Single(okResult.Value);
        }

        [Fact]
        public async Task PostCompraProducto_CreatesNewCompraProducto()
        {
            var context = GetDbContext();
            var controller = new CompraProductoController(context);

            var nuevo = new Compra_Producto
            {
                IdProducto = 2,
                IdCompra = 2,
                Cantidad = 1,
                PrecioUnitario = 5.50m
            };

            var result = await controller.PostCompraProducto(nuevo);
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);

            var createdValue = Assert.IsType<Compra_Producto>(created.Value);
            Assert.Equal(nuevo.IdProducto, createdValue.IdProducto);
        }
    }
}
