using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CompraBono_UIT
{
    public class SelectBono_PO : PageObject
    {

        private By _nombreBonoBy = By.Id("nombreBono");
        private By _tipoBonoBy = By.Id("tipoBono");


        private By _botonComprarBy = By.Id("BotonComprar");
        private By _botonCompraCartBy = By.Id("MostrarCompracart");
        private By _botonBuscarBonoBy = By.Id("BusacarBonos");

        private By _tablaBonosBy = By.Id("TablaBonos");

        private By _modalBy = By.Id("DialogOKSaveDelete");

        private IWebElement _nombreBono() => _driver.FindElement(_nombreBonoBy);
        private IWebElement _tipoBono() => _driver.FindElement(_tipoBonoBy);
        private IWebElement _botonComprar() => _driver.FindElement(_botonComprarBy);
        private IWebElement _botonCompraCart() => _driver.FindElement(_botonCompraCartBy);
        private IWebElement _botonBuscarBono() => _driver.FindElement(_botonBuscarBonoBy);

        public SelectBono_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void FiltroBono(string nombreBonoFiltro, string tipoBonoFiltro)
        {
            Thread.Sleep(1000);

            _nombreBono().SendKeys(nombreBonoFiltro);

            _tipoBono().SendKeys(tipoBonoFiltro);

            _botonBuscarBono().Click();

        }

        public void SelectBonos(List<string> nombreBonos)
        {
            foreach (var nombreBono in nombreBonos)
            {
                Thread.Sleep(1000);
                WaitForBeingVisible(By.Id($"cantidadComprar_{nombreBono}"));
                _driver.FindElement(By.Id($"cantidadComprar_{nombreBono}")).SendKeys("1");
                WaitForBeingVisible(By.Id($"bonoaComprar_{nombreBono}"));
                _driver.FindElement(By.Id($"bonoaComprar_{nombreBono}")).Click();
            }
        }

        public void SelectCantidadBonos(string nombreBono, string cantidad)
        {
            Thread.Sleep(1000);
            WaitForBeingVisible(By.Id($"cantidadComprar_{nombreBono}"));
            _driver.FindElement(By.Id($"cantidadComprar_{nombreBono}")).SendKeys(cantidad);
            WaitForBeingVisible(By.Id($"bonoaComprar_{nombreBono}"));
            _driver.FindElement(By.Id($"bonoaComprar_{nombreBono}")).Click();
        }

        public void ComprarBonos()
        {
            WaitForBeingClickable(_botonComprarBy);
            _botonComprar().Click();
        }

        public void ModificarCompraCart(string nombre)
        {
            _botonCompraCart().Click();
            WaitForBeingVisible(By.Id($"removeBono_{nombre}"));
            _driver.FindElement(By.Id($"removeBono_{nombre}")).Click();
        }

        public bool CheckListaBonos(List<string[]> expectedBonos)
        {
            return  CheckBodyTable(expectedBonos, _tablaBonosBy);
        }

        public bool ChekComprarBonosDisabled()
        {
            return !(_botonComprar().Enabled);
        }

        public bool CheckCompraCart(string precio)
        {
            return _botonCompraCart().Text.Contains(precio);
        }

        public bool CheckMessageErrorBonosNoDisponibles(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public bool CheckMessageError(string expectedError)
        {
            return CheckModalBodyText(expectedError, _modalBy);
        }
    }
    
}
