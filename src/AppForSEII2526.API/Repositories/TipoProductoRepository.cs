using AppForSEII2526.Models;
using AppForSEII2526.API.Data;

namespace AppForSEII2526.API.Repositories
{
    public class TipoProductoRepository : Repository<TipoProducto>
    {
        public TipoProductoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
