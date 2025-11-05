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
        [ProducesResponseType(typeof(List<BocadilloDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBocadillosParaPedir(Tamaño? tamaño, string? tipoPan)
        {
            IList<BocadilloDTO> bocadillos = await _context.Bocadillos
                .Include(b => b.tipoPan)
                .Where(b =>

                tipoPan == null || b.tipoPan.Nombre.Contains(tipoPan)


                && tamaño == null || b.tamaño == tamaño)

                .Select(b => new BocadilloDTO
                {
                    Id = b.Id,
                    Nombre = b.nombre,
                    Tamaño = b.tamaño,
                    TipoPan = b.tipoPan.Nombre,
                    PVP = b.PVP,
                })

            .ToListAsync();
            return Ok(bocadillos);

        }

    }
}
