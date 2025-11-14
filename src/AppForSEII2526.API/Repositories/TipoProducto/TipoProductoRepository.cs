using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;
using AppForSEII2526.API.Data;
using AppForSEII2526.Shared.TipoProductoDTOs;




namespace AppForSEII2526.API.Repositories.TipoProducto
{
    public class TipoProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoProductoDTO>> GetAllAsync()
        {
            return await _context.TipoProductos
                .Select(tp => new TipoProductoDTO { Id = tp.Id, Nombre = tp.Nombre })
                .ToListAsync();
        }

        public async Task<TipoProductoDTO?> GetByIdAsync(int id)
        {
            var tipo = await _context.TipoProductos.FindAsync(id);
            if (tipo == null) return null;

            return new TipoProductoDTO { Id = tipo.Id, Nombre = tipo.Nombre };
        }

        public async Task<TipoProductoDTO> AddAsync(TipoProductoDTO dto)
        {
            var entity = new TipoProducto { Nombre = dto.Nombre };
            _context.TipoProductos.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tipo = await _context.TipoProductos.FindAsync(id);
            if (tipo == null) return false;

            _context.TipoProductos.Remove(tipo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
