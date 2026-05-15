using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
namespace AppForSEII2526.UIT.PedirBocadillo_UIT_test
{
    public class SelectPedido_PO : PageObject
    {
        By inputTamaño = By.Id("selectTamaño");
        By inputTipoPan = By.Id("selectTipoPan");
        By buttonSearchBocadillos = By.Id("BuscarBocadillos");
        By tableOfBocadillos = By.Id("TableOfBocadillos");
        By errorShownBy = By.Id("ErrorsShown");
        By buttonComprarBocadillos = By.Id("purchaseBocadilloButton");
        public SelectPedido_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchBocadillos(string tamaño, string tipoPan)
        {
            WaitForBeingVisibleIgnoringExeptionTypes(inputTamaño);
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputTamaño);
            _driver.FindElement(inputTamaño).SendKeys(tamaño);


            if (tipoPan == "") tipoPan = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputTipoPan));
            selectElement.SelectByText(tipoPan);

            if (tamaño == "") tamaño = "All";
            selectElement = new SelectElement(_driver.FindElement(inputTamaño));
            selectElement.SelectByText(tamaño);

            var createCompraButton = _driver.FindElement(buttonSearchBocadillos);
            createCompraButton.Click();
        }

        public bool CheckListOfBocadillos(List<string[]> expectedBocadillos)
        {
            return CheckBodyTable(expectedBocadillos, tableOfBocadillos);
        }

        public void AddBocadilloParaComprar(string nombreBocadillo)
        {
            WaitForBeingClickable(By.Id("bocadilloParaComprar_" + nombreBocadillo));

            _driver.FindElement(By.Id("bocadilloParaComprar_" + nombreBocadillo)).Click();
        }

        public void seleccionarBotonCompra()
        {
            WaitForBeingVisible(buttonComprarBocadillos);
            WaitForBeingClickable(buttonComprarBocadillos);
            _driver.FindElement(buttonComprarBocadillos).Click();
        }

        public void RemoveBocadilloParaComprar(string nombreBocadillo)
        {
            WaitForBeingClickable(By.Id("removeBocadillo_" + nombreBocadillo));
            _driver.FindElement(By.Id("removeBocadillo_" + nombreBocadillo)).Click();
        }

        public bool CompraNotAvailable()
        {
            //the button is not Displayed=hidden
            return _driver.FindElement(buttonComprarBocadillos).Displayed == false;
        }
    }
}