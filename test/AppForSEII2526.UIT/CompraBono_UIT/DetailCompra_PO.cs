using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CompraBono_UIT
{
    public class DeatailCompra_PO : PageObject
    {
        public DeatailCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckCompraDetail(string nombreCliente, string apellidoCliente, string metodoPago, DateTime fecha, string precioTotal)
        {
            WaitForBeingVisible(By.Id("PrecioTotal"));
            bool result = true;
            result = result && _driver.FindElement(By.Id("Usuario")).Text.Contains(nombreCliente);
            result = result && _driver.FindElement(By.Id("Usuario")).Text.Contains(apellidoCliente);
            result = result && _driver.FindElement(By.Id("MetodoPago")).Text.Contains(metodoPago);
            result = result && _driver.FindElement(By.Id("PrecioTotal")).Text.Contains(precioTotal);

            var actualFecha = DateTime.Parse(_driver.FindElement(By.Id("FechaCompra")).Text);
            result = result && actualFecha.Date == fecha.Date;

            return result;
        }

        public bool CheckCompraBonos(List<string[]> expectedCompraBonos)
        {
            return CheckBodyTable(expectedCompraBonos, By.Id("BonosComprados"));
        }

    }
}
