using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompraProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra_Producto>>> GetCompraProductos()
        {
            return await _context.Compra_Productos
                .Include(cp => cp.Producto)
                .Include(cp => cp.Compra)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Compra_Producto>> PostCompraProducto(Compra_Producto compraProducto)
        {
            _context.Compra_Productos.Add(compraProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompraProductos), new { id = compraProducto.Id }, compraProducto);
        }
    }
}
