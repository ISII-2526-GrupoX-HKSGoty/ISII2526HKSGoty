using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using System;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BocadillosController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<BocadillosController> _logger;

        public BocadillosController(ApplicationDbContext context, ILogger<BocadillosController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<BocadilloDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBocadillosParaPedir(string? filTamaño, string? filTipoPan)
        {
            Tamaño? tamanoFiltrado = null;

            if (!string.IsNullOrWhiteSpace(filTamaño) &&
                Enum.TryParse<Tamaño>(filTamaño, ignoreCase: true, out var parsed))
            {
                tamanoFiltrado = parsed;
            }

            var query = _context.Bocadillos
            .AsNoTracking()
            .Include(b => b.tipoPan)
            .Include(b => b.ComprasDelBocadillo).ThenInclude(cb => cb.Compra)
            .AsQueryable();

            if (tamanoFiltrado.HasValue)
                query = query.Where(b => b.tamaño == tamanoFiltrado.Value);  //filtro por string

            if (!string.IsNullOrWhiteSpace(filTipoPan))
                query = query.Where(b => b.tipoPan.Nombre == filTipoPan);

            var bocadillos = await query
                .OrderBy(b => b.nombre)
                    .Select(b => new BocadilloDTO(b.Id, b.nombre, b.tipoPan.Nombre, b.tamaño, b.PVP))
                    .ToListAsync();

            if (!bocadillos.Any())
                return NotFound("No hay bocadillos que cumplan los requisitos");

            return Ok(bocadillos);


        }
    }
}
