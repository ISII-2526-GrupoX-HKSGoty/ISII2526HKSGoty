using AppForSEII2526.API.Data;
using AppForSEII2526.API.DTOs.DTOs_PedirBocadillo;
using AppForSEII2526.API.DTOs;
using System.Linq;

namespace AppForSEII2526.API.Controllers.PedidosControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PedidoController> _logger;
        public PedidoController(ApplicationDbContext context, ILogger<PedidoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetallesPedidoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPedido(int id)
        {
            if (_context.Compras == null)
            {
                _logger.LogError("No existen las compras");
                return NotFound();
            }

            var pedido = await _context.Compras
                .Where(c => c.CompraId == id)
                .Include(c => c.BocadillosComprados)
                .ThenInclude(cb => cb.Bocadillo)
                .ThenInclude(b => b.tipoPan)
                .Select(c => new DetallesPedidoDTO(
                    c.CompraId,
                    c.User.nombre,
                    c.Metodo_Pago,
                    c.User.apellido1,
                    c.User.apellido2,
                    c.FechaCompra,
                    c.BocadillosComprados.Select(
                        cb => new ArticuloPedidoDTO {
                        Id = cb.BocadilloId,
                        nombreBocadillo = cb.NombreBocadillo,
                        Cantidad = cb.Cantidad,
                        PVP = cb.Precio,
                        TipoPan = cb.Bocadillo.tipoPan.Nombre
                        }).ToList(),
                    c.PrecioTotal
                )).FirstOrDefaultAsync();

            if (pedido == null)
            {
                _logger.LogError("Error: No se ha encontrado el pedido con id {Id}", id);
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ArticuloPedidoDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearPedido(CrearPedidoDTO pedidoParaCrear)
        {
            if (pedidoParaCrear.ArticuloPedido.Count == 0)
                ModelState.AddModelError("CompraItems", "Error. Tienes que añadir al menos un bocadillo para realizar la compra");

            var usuario = _context.ApplicationUser.FirstOrDefault(au => au.nombre == pedidoParaCrear.nombre && au.apellido1 == pedidoParaCrear.apellido1);
            if (usuario == null)
            {
                return NotFound($"Error: El nombre y apellido introducidos no corresponden a un usuario registrado.");
            }

            var metodoPagoEnum = pedidoParaCrear.Metodo_Pago;
            if (!Enum.IsDefined(typeof(Metodo_Pago), pedidoParaCrear.Metodo_Pago))
            {
                ModelState.AddModelError("Metodo_Pago", "Método de pago no válido. Usa: Tarjeta, Paypal o GooglePay.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }


            var pedidoNombre = pedidoParaCrear.ArticuloPedido.Select(ri => ri.Id).ToList();

            var bocadillos = _context.Bocadillos
                .Include(b => b.ComprasDelBocadillo)
                .ThenInclude(cb => cb.Compra)
                .Where(b => pedidoNombre.Contains(b.Id))
                .Select(b => new { b.nombre, b.PVP, b.stock, b.tamaño, b.Id, b.tipoPan}).ToList();

            Compra compra = new Compra(usuario, DateTime.Today, metodoPagoEnum, new List<CompraBocadillo>());
            compra.PrecioTotal = 0;


            foreach (var articulo in pedidoParaCrear.ArticuloPedido)
            {
                var bocadillo = bocadillos.FirstOrDefault(p => p.nombre == articulo.nombreBocadillo);
                if (bocadillo == null)
                {
                    ModelState.AddModelError("Bocadillo", "Error! El bocadillo no existe");
                    return ValidationProblem(ModelState);
                }
                
                if(articulo.Cantidad <= 0)
                {
                    ModelState.AddModelError("Cantidad", $"Error! La cantidad es negativa");
                    return ValidationProblem(ModelState);
                }

                if (articulo.Cantidad > bocadillo.stock)
                {
                    ModelState.AddModelError("Cantidad", "Error! La cantidad para el bocadillo es mayor que la cantidad disponible");
                    return ValidationProblem(ModelState);
                }
                // Si todo va bien, añadimos al pedido
                compra.BocadillosComprados.Add(new CompraBocadillo(bocadillo.Id, articulo.Cantidad, compra, compra.CompraId, bocadillo.nombre, bocadillo.PVP));
                articulo.PVP = bocadillo.PVP;
            }
            
            compra.PrecioTotal = compra.BocadillosComprados.Sum(cb => cb.Precio * cb.Cantidad);
            compra.nBocadillos = compra.BocadillosComprados.Sum(cb => cb.Cantidad);

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Compras.Add(compra);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el pedido");
            }

            var detallesPedidoDTO = new DetallesPedidoDTO(
                compra.CompraId,
                usuario.nombre,
                compra.Metodo_Pago,
                usuario.apellido1,
                usuario.apellido2,
                compra.FechaCompra,
                pedidoParaCrear.ArticuloPedido.ToList(),
                compra.PrecioTotal
                );
            
            return CreatedAtAction(
                    nameof(GetPedido), // <-- Esto detectará automáticamente el nombre "GetPedido"
                    //"GetPedido",
                    new { id = compra.CompraId },
                    detallesPedidoDTO
                );
        }
    }
}
