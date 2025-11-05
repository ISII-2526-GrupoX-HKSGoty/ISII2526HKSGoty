using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AppForSEII2526.API.Controllers
{
    [System.Web.Mvc.Route("api/[controller]")]
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
                _logger.LogError("no hay bonos");
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

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(DETAIL_Bono_DTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> getCompraBono(int id)
        {
            if(_context.CompraBono == null)
            {

                _logger.LogError("Error");
                return NotFound();

            }

            var compra = await _context.CompraBono
                .Where(r => r.CompraBonoId == id)
                .Include(r => r.BonosComprados)
                .ThenInclude(ri => ri.BonoBocadillo)
                .Select(r => new DETAIL_Bono_DTO(r.CompraBonoId, r.User.nombre, r.User.apellido1, r.User.apellido2, r.metodoPago, r.PrecioTotalBono, r.ReleaseDate, r.BonosComprados
                .Select(ri => new Item_DTO(ri.CompraBonoId, ri.BonoId, ri.BonoBocadillo.nombre, ri.BonoBocadillo.TipoBocadillo, ri.BonoBocadillo.PVP,ri.Cantidad)).ToList<Item_DTO>()))
                .FirstOrDefaultAsync();


        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DETAIL_Bono_DTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> PostBonos(POST_Bono_DTO postBonoDTO)
        {
            if (postBonoDTO.ItemCompra.Count == 0)
            {
                ModelState.AddModelError("ItemCompra", "La compra debe contener al menos un bono.");
            }

            var user = _context.ApplicationUser.FirstOrDefault(u => u.nombre == postBonoDTO.nombre &&
            u.apellido1 == postBonoDTO.apellido1 && u.apellido2 == postBonoDTO.apellido2);

            if (user == null)
            {
                ModelState.AddModelError("Usuario", "El usuario no existe.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var nombrebono = postBonoDTO.ItemCompra.Select(b => b.nombre).ToList();
            var bonos = _context.BonoBocadillos
                .Where(b => nombrebono.Contains(b.nombre))
                .ToList();

            CompraBono compraBono = new CompraBono(user, DateTime.Now, postBonoDTO.ItemCompra.Count, postBonoDTO.metodoPago, new List<BonosComprados>());

            compraBono.PrecioTotalBono = 0;

            foreach (var item in postBonoDTO.ItemCompra)
            {
                var bono = bonos.FirstOrDefault(b => b.nombre == item.nombre);

                if ((bono.cantidadDisponible < item.numero) || (bono == null))
                {
                    ModelState.AddModelError("Items", $"Error! Bono '{item.nombre}' no esta disponible");
                }
                else
                {
                    compraBono.BonosComprados.Add(new BonosComprados(bono, bono.BonoId, compraBono, compraBono.CompraBonoId, item.numero, bono.PVP));
                    bono.cantidadDisponible = bono.cantidadDisponible - item.numero;
                    compraBono.PrecioTotalBono = compraBono.PrecioTotalBono + (bono.PVP * item.numero);
                }

            }

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(compraBono);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Rental", $"Error! There was an error while saving your rental, plese, try again later");
                return Conflict("Error" + ex.Message);

            }

            var bonoDetail = new DETAIL_Bono_DTO(compraBono.CompraBonoId,user.nombre, user.apellido1, user.apellido2, postBonoDTO.metodoPago, compraBono.PrecioTotalBono, DateTime.Now,compraBono.BonosComprados);

            return CreatedAtAction ("GetBonos", new { id = compraBono.CompraBonoId }, bonoDetail);

        }
        
    } 

}
