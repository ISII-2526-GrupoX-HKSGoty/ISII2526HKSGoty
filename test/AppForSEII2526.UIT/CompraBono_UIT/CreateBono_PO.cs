using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CompraBono_UIT
{
    public class CreateBono_PO : PageObject
    {
        public CreateBono_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        private By _nombreClienteBy = By.Id("NombreCliente");
        private IWebElement _nombreCliente() => _driver.FindElement(_nombreClienteBy);
        private IWebElement _apellido1Cliente() => _driver.FindElement(By.Id("Apellido1Cliente"));
        private IWebElement _metodoPago ()=> _driver.FindElement(By.Id("MetodoPago"));

        public void rellenarCompra(string nombre, string apellido, string apellido2, string metodoPago)
        {
            WaitForBeingVisible(_nombreClienteBy);
            
            _nombreCliente().SendKeys(nombre);
            _apellido1Cliente().SendKeys(apellido);

            SelectElement select = new SelectElement(_metodoPago());
            select.SelectByText(metodoPago);

        }

        public void botonComprar()
        {
            _driver.FindElement(By.Id("submit")).Click();
        }

        public void botonModificar()
        {
            Thread.Sleep(1000);
            _driver.FindElement(By.Id("ModificarCompra")).Click();
        }
        
        public bool CheckListCompraItems(List<string[]>expectedCompraItems)
        {
            return CheckBodyTable(expectedCompraItems, By.Id("TablaCompraBonos"));
        }

        public bool CheckValidationError(string expectederror)
        {
            return _driver.PageSource.Contains(expectederror);
        }
    }
}
