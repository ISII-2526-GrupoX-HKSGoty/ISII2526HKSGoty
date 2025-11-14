using AppForSEII2526.API.Repositories.TipoProducto;
using Microsoft.AspNetCore.Mvc;
using AppForSEII2526.API.Repositories.TipoProducto;
using AppForSEII2526.Shared.TipoProductoDTOs;

namespace AppForSEII2526.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoProductoController : ControllerBase
    {
        private readonly TipoProductoRepository _repository;

        public TipoProductoController(TipoProductoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProductoDTO>>> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProductoDTO>> GetById(int id)
        {
            var tipo = await _repository.GetByIdAsync(id);
            if (tipo == null) return NotFound();
            return Ok(tipo);
        }

        [HttpPost]
        public async Task<ActionResult<TipoProductoDTO>> Create([FromBody] TipoProductoDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
