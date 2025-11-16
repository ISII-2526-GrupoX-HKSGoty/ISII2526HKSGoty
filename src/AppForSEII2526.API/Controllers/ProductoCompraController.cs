using Microsoft.AspNetCore.Mvc;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoCompraController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoCompraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductoCompra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto_Compra>>> GetProductoCompras()
        {
            return await _context.Producto_Compras
                .Include(pc => pc.Producto)
                .Include(pc => pc.Compra)
                .ToListAsync();
        }

        // GET: api/ProductoCompra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto_Compra>> GetProductoCompra(int id)
        {
            var item = await _context.Producto_Compras
                .Include(pc => pc.Producto)
                .Include(pc => pc.Compra)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (item == null)
                return NotFound();

            return item;
        }

        // POST: api/ProductoCompra

        [HttpPost]


        public async Task<ActionResult<Producto_Compra>> PostProductoCompra(Producto_Compra productoCompra)
        {
            _context.Producto_Compras.Add(productoCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductoCompra), new { id = productoCompra.Id }, productoCompra);
        }
    }
}
