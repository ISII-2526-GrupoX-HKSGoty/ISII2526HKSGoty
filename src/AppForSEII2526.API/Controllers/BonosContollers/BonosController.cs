using AppForSEII2526.API.DTOs.DTOsCompraBono;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace AppForSEII2526.API.Controllers.BonosContollers
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
        [ProducesResponseType(typeof(IList<BonoBocadillo>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBonos(string? nombre, string? tipo)
        {
            var bonos = await _context.BonoBocadillos
                .Where(m=>((m.nombre.Contains(nombre)) || (nombre == null)) && ((m.TipoBocadillo.nombreTipo.Contains(tipo)) || (tipo == null)))
                .Select(m=>new Get_BonosDTO(m.nombre, m.PVP, m.nBocadillos, m.TipoBocadillo.nombreTipo))
                .ToListAsync();

            if (bonos == null || bonos.Count == 0)
            {
                _logger.LogError("Error: Bono no encontrado");
                return NotFound("No se han encontrado bonos con los filtros proporcionados.");
            }
            return Ok(bonos);

        }
    }
}
