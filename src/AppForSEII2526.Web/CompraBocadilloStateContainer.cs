using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class CompraBocadilloStateContainer
    {

        //we create an instance of Rental when an instance of RentalStateContainer is created
        public CrearPedidoDTO Compra { get; private set; } = new CrearPedidoDTO()
        {
            ArticuloPedido = new List<ArticuloPedidoDTO>()
        };

        //we compute the TotalPrice of the movies we have selected for renting them
        public decimal TotalPrice
        {
            get
            {
                return Convert.ToDecimal(Compra.ArticuloPedido.Sum(ri => ri.Pvp));
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();



        public void AddBocadilloParaComprar(BocadilloDTO bocadillo)
        {
            //before adding a movie we checked whether it has been already added
            if (!Compra.ArticuloPedido.Any(ri => ri.Id == bocadillo.Id))
                //we add it if it is not in the list
                Compra.ArticuloPedido.Add(new ArticuloPedidoDTO()
                {
                    Id = bocadillo.Id,
                    TipoPan = bocadillo.TipoPanNombre,
                    Pvp = bocadillo.Pvp,
                    NombreBocadillo = bocadillo.Nombre,
                    Cantidad = 1
                });
        }

        //to delete movies from the list of selected movies
        public void RemoveCompraArticuloParaComprar(ArticuloPedidoDTO item)
        {
            Compra.ArticuloPedido.Remove(item);
        }

        //we eliminate all the movies from the list
        public void LimpiarCarroCompra()
        {
            Compra.ArticuloPedido.Clear();
        }

        //we have already finished the process of renting, thus, we create a new Rental 
        public void CompraProcessed()
        {
            //we have finished the rental process so we create a new object without data
            Compra = new CrearPedidoDTO()
            {
                ArticuloPedido = new List<ArticuloPedidoDTO>()
            };
        }
    }
}


