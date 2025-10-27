using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;

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
        [ProducesResponseType(typeof(List<GET_CrearResenaDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBocadillosResenya(string? nombre, Tamaño? tamaño, string? tipoPan, float? PVP)
        {
            IList<GET_CrearResenaDTO> bocadillos = await _context.Bocadillos
                .Include(b => b.tipoPan)
                .Where(b =>
                    (nombre == null || b.nombre.Contains(nombre)) &&
                    ((!tamaño.HasValue || b.tamaño == tamaño.Value)) &&
                    (tipoPan == null || b.tipoPan.Nombre.Contains(tipoPan)) &&
                    (!PVP.HasValue || (float) b.PVP <= PVP.Value)
                )
                .Select(b => new GET_CrearResenaDTO(b.nombre, b.PVP, b.tipoPan, b.tamaño))
                .ToListAsync();
            return Ok(bocadillos);
        }
    }
}
