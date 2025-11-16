using Microsoft.AspNetCore.Mvc;
using AppForSEII2526.API.Repositories;
using AppForSEII2526.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppForSEII2526.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoProductoController : ControllerBase
    {
        private readonly TipoProductoRepository _repo;

        public TipoProductoController(TipoProductoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProductoDTO>>> GetAll()
        {
            var tipos = await _repo.GetAllAsync();
            return Ok(tipos.Select(t => new TipoProductoDTO
            {
                TipoProductoId = t.TipoProductoId,
                Nombre = t.Nombre
            }));
        }

        [HttpPost]
        public async Task<ActionResult<TipoProductoDTO>> Post(TipoProductoDTO dto)
        {
            var nuevo = new TipoProducto
            {
                Nombre = dto.Nombre
            };

            var creado = await _repo.CreateAsync(nuevo);

            return CreatedAtAction(nameof(GetAll), new { id = creado.TipoProductoId }, new TipoProductoDTO
            {
                TipoProductoId = creado.TipoProductoId,
                Nombre = creado.Nombre
            });
        }
    }
}
