using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.Repositories;
using AppForSEII2526.Models;
using AppForSEII2526.API.Data;
using AppForSEII2526.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.Tests
{
    public class TipoProductoControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            // Crear contexto en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            context.TipoProductos.Add(new TipoProducto { TipoProductoId = 1, Nombre = "Camisetas" });
            context.SaveChanges();

            // Usar instancia real del repositorio con ese contexto
            var repository = new TipoProductoRepository(context);
            var controller = new TipoProductoController(repository);

            // ACT
            var actionResult = await controller.GetAll();

            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);  //.Result contiene el OkObjectResult
            var tipoProductos = Assert.IsAssignableFrom<IEnumerable<TipoProductoDTO>>(okResult.Value); //Validamos que Value es del tipo esperado
            Assert.NotEmpty(tipoProductos); 
        }


    }
}
