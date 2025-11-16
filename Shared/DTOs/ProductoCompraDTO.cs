using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.Shared.DTOs
{
    public class ProductoCompraDTO
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }

        public int CompraId { get; set; }

        public int Cantidad { get; set; }
    }
}

