using AppForSEII2526.API.DTOs.DTOsCompraBono;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers.BonosContollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraBonosController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompraBonosController> _logger;

        public CompraBonosController(ApplicationDbContext context, ILogger<CompraBonosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(Detail_CompraBonoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> getCompra(int id)
        {
            if(_context.CompraBono == null)
            {
                _logger.LogError("Error: No existe la tabla");
                return NotFound();
            }

            var compra = await _context.CompraBono
                .Where(c => c.CompraBonoId == id)
                    .Include(ic => ic.BonosComprados)
                        .ThenInclude(bc => bc.BonoBocadillo)
                .Select(c => new Detail_CompraBonoDTO(c.CompraBonoId, c.ReleaseDate, c.PrecioTotalBono,c.User.nombre, c.User.apellido1, c.User.apellido2, c.metodoPago, 
                c.BonosComprados.Select(ic => new ItemBonoDTO(ic.Precio, ic.BonoBocadillo.nBocadillos, ic.BonoBocadillo.nombre,ic.Cantidad, ic.BonoBocadillo.TipoBocadillo.nombreTipo)).ToList<ItemBonoDTO>()))
                .FirstOrDefaultAsync();

            if(compra == null)
            {
                _logger.LogError("Error: Compra no encontrada");
                return NotFound();
            }

            return Ok(compra);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(Detail_CompraBonoDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string),(int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> createCompraBonos(CompraBonoDTO compra)
        {
            if(compra.Items.Count == 0)
            {
                ModelState.AddModelError("ItemsCompra", "Minimo un item");
                return BadRequest(ModelState);
            }

            var user = _context.ApplicationUser.FirstOrDefault(au => au.nombre == compra.nombreCliente);
            if (user == null)
            {
                ModelState.AddModelError("Usuario", "Usuario no encontrado");
                return BadRequest(ModelState);
            }

            if(ModelState.ErrorCount > 0) return BadRequest(new ValidationProblemDetails(ModelState));

            CompraBono compraBono = new CompraBono(user, DateTime.Now, compra.metdoPago, new List<BonosComprados>());
            compraBono.PrecioTotalBono = 0;
            compraBono.nBonos = 0;

            foreach (var item in compra.Items)
            {
               var bono = _context.BonoBocadillos.FirstOrDefault(b => b.nombre == item.nombreBono);

                if ((bono == null) || (bono.cantidadDisponible < item.cantidad))
                {
                    ModelState.AddModelError("ItemsCompra", $"Error bono {item.nombreBono} no encontrado o sin stock");
                }

                else
                {
                    compraBono.BonosComprados.Add(new BonosComprados(bono.BonoId, compraBono, item.cantidad, bono.PVP));
                    compraBono.PrecioTotalBono += item.cantidad * bono.PVP;
                    compraBono.nBonos += item.cantidad;
                    bono.cantidadDisponible -= item.cantidad;

                }

            }

            if(ModelState.ErrorCount > 0) return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Add(compraBono);

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Conflict("Error" + ex.Message);

            }

            var compraDetail = new Detail_CompraBonoDTO(compraBono.CompraBonoId,compraBono.ReleaseDate, compraBono.PrecioTotalBono, compraBono.User.nombre,compraBono.User.apellido1, compraBono.User.apellido2, compraBono.metodoPago, compra.Items);

            return CreatedAtAction("getCompra", new {id = compraBono.CompraBonoId}, compraDetail);

        }

    }
}
