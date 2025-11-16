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
        public async Task<IActionResult> GetBocadillosParaPedir(Tamaño? filTamaño, string? filTipoPan)
        {
            IList<BocadilloDTO> bocadillos = await _context.Bocadillos
                .Include(b => b.tipoPan)
                .Where(b =>

                (filTipoPan == null || b.tipoPan.Nombre.Contains(filTipoPan))


                && (filTamaño == null || b.tamaño == filTamaño))


                .Select(b => new BocadilloDTO
                {
                    Id = b.Id,
                    Nombre = b.nombre,
                    Tamaño = b.tamaño,
                    TipoPanNombre = b.tipoPan.Nombre,
                    PVP = b.PVP,
                })

            .ToListAsync();
            return Ok(bocadillos);

        }
    }
}
