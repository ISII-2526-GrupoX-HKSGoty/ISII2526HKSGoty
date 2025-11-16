using AppForSEII2526.API.Repositories;
using AppForSEII2526.Shared;
using AppForSEII2526.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppForSEII2526.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoRepository _repository;

        public ProductoController(ProductoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> GetAll()
        {
            var productos = await _repository.GetAllAsync();
            var productosDto = new List<ProductoDTO>();

            foreach (var producto in productos)
            {
                productosDto.Add(new ProductoDTO
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    TipoProductoId = producto.TipoProductoId
                });
            }

            return Ok(productosDto);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var producto = await _repository.GetByIdAsync(id);

            if (producto == null)
                return NotFound();

            var productoDto = new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                TipoProductoId = producto.TipoProductoId
            };

            return Ok(productoDto);
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Create(ProductoDTO dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
                TipoProductoId = dto.TipoProductoId
            };

            var created = await _repository.CreateAsync(producto);

            dto.Id = created.Id;
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
    }
}
