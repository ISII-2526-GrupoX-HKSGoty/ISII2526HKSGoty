using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Data;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.Models;
using AppForSEII2526.Models;
using AppForSEII2526.Shared.DTOs;

namespace AppForSEII2526.Tests.Controllers
{
    public class CompraProductoControllerTests
    {
        private ApplicationDbContext GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            // Insertar TipoProducto requerido
            var tipoProducto = new TipoProducto
            {
                TipoProductoId = 1,
                Nombre = "Merch"
            };
            context.TipoProductos.Add(tipoProducto);

            // Insertar Producto válido
            var producto = new Producto
            {
                Id = 1,
                Nombre = "Camisa",
                Precio = 20.99m,
                TipoProductoId = tipoProducto.TipoProductoId,
                Stock = 10
            };
            context.Productos.Add(producto);

            // Insertar Compra válida
            var compra = new Compra
            {
                CompraId = 1,
                FechaCompra = DateTime.Now,
                Metodo_Pago = Metodo_Pago.Tarjeta,
                PrecioTotal = 21,
                nBocadillos = 1
            };
            context.Compras.Add(compra);

            // Insertar Compra_Producto relacionada
            context.Compra_Productos.Add(new Compra_Producto
            {
                Id = 1,
                ProductoId = producto.Id,
                CompraId = compra.CompraId,
                Cantidad = 1,
                PrecioUnitario = 20.99m
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetCompraProductos_ReturnsCompraProductos()
        {
            // Arrange
            var context = GetDbContextWithData();
            var controller = new CompraProductoController(context);

            // Act
            var result = await controller.GetCompraProductos();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Compra_Producto>>>(result);
            var compraProductos = Assert.IsAssignableFrom<IEnumerable<Compra_Producto>>(actionResult.Value);
            Assert.NotEmpty(compraProductos);
        }

        [Fact]
        public async Task PostCompraProducto_CreatesNewCompraProducto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            // Crear tipo de producto necesario
            var tipoProducto = new TipoProducto
            {
                TipoProductoId = 2,
                Nombre = "Accesorios"
            };
            context.TipoProductos.Add(tipoProducto);

            // Crear producto asociado
            var producto = new Producto
            {
                Id = 2,
                Nombre = "Gorra",
                Precio = 10.50m,
                TipoProductoId = tipoProducto.TipoProductoId,
                Stock = 20
            };
            context.Productos.Add(producto);

            // Crear compra asociada
            var compra = new Compra
            {
                CompraId = 2,
                FechaCompra = DateTime.Now,
                Metodo_Pago = Metodo_Pago.Tarjeta,
                PrecioTotal = 10,
                nBocadillos = 1
            };
            context.Compras.Add(compra);

            context.SaveChanges();

            var controller = new CompraProductoController(context);

            // DTO para el POST
            var nuevo = new CompraProductoDTO
            {
                ProductoId = producto.Id,
                CompraId = compra.CompraId,
                Cantidad = 1,
                PrecioUnitario = 10.50m
            };

            // Act
            var result = await controller.PostCompraProducto(nuevo);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdValue = Assert.IsType<Compra_Producto>(created.Value);

            Assert.Equal(nuevo.ProductoId, createdValue.ProductoId);
            Assert.Equal(nuevo.CompraId, createdValue.CompraId);
            Assert.Equal(nuevo.Cantidad, createdValue.Cantidad);
            Assert.Equal(nuevo.PrecioUnitario, createdValue.PrecioUnitario);
        }
    }
}
