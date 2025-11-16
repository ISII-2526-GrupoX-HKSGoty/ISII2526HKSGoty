using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.Repositories;
using AppForSEII2526.Models;
using AppForSEII2526.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AppForSEII2526.Tests
{
    public class TipoProductoControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            // Arrange
            var mockRepo = new Mock<TipoProductoRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TipoProducto>
            {
                new TipoProducto { TipoProductoId = 1, Nombre = "Bebidas" },
                new TipoProducto { TipoProductoId = 2, Nombre = "Ropa" }
            });

            var controller = new TipoProductoController(mockRepo.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsAssignableFrom<IEnumerable<TipoProductoDTO>>(okResult.Value);
            Assert.Equal(2, items.Count());
        }
    }
}
