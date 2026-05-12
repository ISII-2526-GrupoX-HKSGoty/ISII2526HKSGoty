using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class CompraBonoStateContainer
    {

        //we create an instance of Rental when an instance of RentalStateContainer is created
        public CompraBonoDTO Compra { get; private set; } = new CompraBonoDTO()
        {
            Items = new List<ItemBonoDTO>()
        };

        //we compute the TotalPrice of the movies we have selected for renting them
        public double TotalPrice
        {
            get
            {
                return Convert.ToDouble(Compra.Items.Sum(ri => ri.Pvp * ri.Cantidad));
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();



        public void AddBonoCompra(Get_BonosDTO bono, int cantidad)
        {
            //before adding a movie we checked whether it has been already added
            if (!Compra.Items.Any(ri => ri.NombreBono == bono.Nombre))
                //we add it if it is not in the list
                Compra.Items.Add(new ItemBonoDTO()
                {
                    Pvp = bono.Pvp,
                    NBocadillos = bono.NBocadillos,
                    NombreBono = bono.Nombre,
                    Cantidad = cantidad,
                    TipoBocadillo = bono.TipoBocadillo

                });
        }

        //to delete movies from the list of selected movies
        public void RemoveItemBono(ItemBonoDTO item)
        {
            Compra.Items.Remove(item);
        }

        //we eliminate all the movies from the list
        public void LimpiarCompraBonos()
        {
            Compra.Items.Clear();
        }

        //we have already finished the process of renting, thus, we create a new Rental 
        public void CompraBonosProcesada()
        {
            //we have finished the rental process so we create a new object without data
            Compra = new CompraBonoDTO()
            {
                Items = new List<ItemBonoDTO>()
            };
        }
    }
}


