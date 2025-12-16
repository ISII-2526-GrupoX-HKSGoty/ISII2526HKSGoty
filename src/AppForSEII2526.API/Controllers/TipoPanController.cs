using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoPanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public TipoPanController(ApplicationDbContext context, ILogger<CompraBocadillo> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTipoPanes(string? tipoBocadilloString)
        {
            IList<string> tipos = await _context.tipoPan
                .Where(tipob => (tipoBocadilloString == null || tipob.Nombre.Contains(tipoBocadilloString)))
                .OrderBy(tipob => tipob.Nombre)
                .Select(tipob => tipob.Nombre)
                .ToListAsync();
            return Ok(tipos);
        }
    }
}
