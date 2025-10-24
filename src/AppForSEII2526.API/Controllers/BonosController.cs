using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BonosController> _logger;

        public BonosController(ApplicationDbContext context, ILogger<BonosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(GET_Bono_DTO), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> GetBonos(string? nombre, string? tipo)
        {
            if(_context.BonoBocadillos == null)
            {
                _logger.LogError("no existe la tabla");
                return NotFound();
            }

            var bonos = await _context.BonoBocadillos
                .Include(b => b.TipoBocadillo)
                .Where(b=>((b.nombre.Contains(nombre)) || nombre == null) &&
                          ((b.TipoBocadillo.nombreTipo.Contains(tipo)) || tipo == null))
                .Select(b=> new GET_Bono_DTO(b.nombre, b.PVP, b.nBocadillos, b.TipoBocadillo))
                .ToListAsync();
            return Ok(bonos);

        }
    }
}
