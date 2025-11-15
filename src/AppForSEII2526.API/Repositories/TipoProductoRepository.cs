using AppForSEII2526.Models;
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

        public async Task<List<TipoProducto>> GetAllAsync()
        {
            return await _context.TipoProductos.ToListAsync();
        }

        public async Task<TipoProducto> GetByIdAsync(int id)
        {
            return await _context.TipoProductos.FindAsync(id);
        }

        public async Task<TipoProducto> CreateAsync(TipoProducto tipoProducto)
        {
            _context.TipoProductos.Add(tipoProducto);
            await _context.SaveChangesAsync();
            return tipoProducto;
        }
    }
}
