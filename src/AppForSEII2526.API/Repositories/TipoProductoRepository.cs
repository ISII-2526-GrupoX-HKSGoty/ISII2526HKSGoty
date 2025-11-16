using AppForSEII2526.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppForSEII2526.API.Repositories
{
    public class TipoProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<TipoProducto>> GetAllAsync()
        {
            return await _context.TipoProductos.ToListAsync();
        }

        public virtual async Task<TipoProducto> CreateAsync(TipoProducto tipo)
        {
            _context.TipoProductos.Add(tipo);
            await _context.SaveChangesAsync();
            return tipo;
        }
    }
}
