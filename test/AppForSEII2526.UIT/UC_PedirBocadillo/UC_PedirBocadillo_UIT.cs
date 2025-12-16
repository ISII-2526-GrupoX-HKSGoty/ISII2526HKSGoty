using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.UC_Rental;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_PedirBocadillo
{
    public class UC_PedirBocadillo_UIT : UC_UIT
    {
        private SelectBocadillosParaPedir_PO selectBocadillosParaPedir_PO;
        private CrearPedido_PO crearPedido_PO;
        private const int bocadilloID = 1;
        private const string nombreBocadillo1 = "BaconQueso";
        private const string tipoPan1 = "Normal";
        private const string precio1 = "3";
        private const string tamanyo1 = "Normal";

        private const string nombreBocadillo2 = "Serrano";
        private const string tipoPan2 = "Integral";
        private const string precio2 = "5";
        private const string tamanyo2 = "Pequeño";


        public UC_PedirBocadillo_UIT(ITestOutputHelper output) : base(output)
        {
            selectBocadillosParaPedir_PO = new SelectBocadillosParaPedir_PO(_driver, _output);
            crearPedido_PO = new CrearPedido_PO(_driver, _output);
        }


        private void InitialStepsForCompraBocadillos()
        {
            _driver.Navigate().GoToUrl("https://localhost:7081/");
            selectBocadillosParaPedir_PO.WaitForBeingVisible(By.Id("CrearCompra"));

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(By.Id("CrearCompra"));
                    return element != null && element.Displayed && element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            int retries = 3;
            while (retries-- > 0)
            {
                try
                {
                    var createCompraButton = _driver.FindElement(By.Id("CrearCompra"));
                    createCompraButton.Click();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    if (retries == 0) throw;
                    Thread.Sleep(200);
                }
            }
        }



        [Theory]
        [InlineData(nombreBocadillo1, tipoPan1, precio1, tamanyo1, "Normal", "")]
        [InlineData(nombreBocadillo2, tipoPan2, precio2, tamanyo2, "", "Integral")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void TC1_AF2_TC1_3_4filtering(string nombreBocadillo, string tipoPan, string precio, string tamanyo,
            string filterTamanyo, string filterTipoPan)
        {
            //Arrange
            InitialStepsForCompraBocadillos();
            var expectedBocadillos = new List<string[]> { new string[] { nombreBocadillo, tamanyo, tipoPan, precio }, };

            //Act
            selectBocadillosParaPedir_PO.SearchBocadillos(filterTamanyo, filterTipoPan);

            //Assert
            Assert.True(selectBocadillosParaPedir_PO.CheckListOfBocadillos(expectedBocadillos));
        }



        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void TC1_AF2_TC1_3_4CompraNotAvailable()
        {
            //Arrange
            InitialStepsForCompraBocadillos();
            //Act
            selectBocadillosParaPedir_PO.AddBocadilloParaComprar(nombreBocadillo1);
            selectBocadillosParaPedir_PO.RemoveBocadilloParaComprar(nombreBocadillo1);

            //Assert
            Assert.True(selectBocadillosParaPedir_PO.CompraNotAvailable());
        }


        //post
        [Theory]
        [InlineData("", "Martinez", "Panadero", "Tarjeta")] //Falta Nombre
        [InlineData("Fernando", "", "Panadero", "Tarjeta")] //Falta Primer Apellido
        [InlineData("Francisco", "No sabe", "Matematicas", "Tarjeta")] //Usuario Inexistente
        [Trait("LevelTesting", "Funcional Testing")]

        public void TC1_FA3_2_3_4_5_6_7(string nombre, string apellido1, string apellido2, string metodopago) //Esc_6
        {
            InitialStepsForCompraBocadillos();
            selectBocadillosParaPedir_PO.SearchBocadillos("", "");
            selectBocadillosParaPedir_PO.AddBocadilloParaComprar("Completo");
            selectBocadillosParaPedir_PO.AddBocadilloParaComprar("Poli");
            selectBocadillosParaPedir_PO.seleccionarBotonCompra();
            crearPedido_PO.rellenarDatosParaCompra(nombre, apellido1, apellido2, metodopago);
            crearPedido_PO.seleccionarBotonCompra();
            Assert.True(crearPedido_PO.checkErrorMessage("Errores: Error al procesar tu solicitud, inténtalo de nuevo más tarde!"));
        }




        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void TC1_FA3_2_3_4_5_6_7_8() //Cantidad 0
        {
            InitialStepsForCompraBocadillos();
            selectBocadillosParaPedir_PO.SearchBocadillos("", "");
            selectBocadillosParaPedir_PO.AddBocadilloParaComprar("Completo");
            selectBocadillosParaPedir_PO.AddBocadilloParaComprar("Poli");
            selectBocadillosParaPedir_PO.seleccionarBotonCompra();
            crearPedido_PO.rellenarDatosParaCompra("Fernando", "Martinez", "Panadero", "PayPal");
            crearPedido_PO.modificarCantidadBocadillos("1", "0");
            crearPedido_PO.seleccionarBotonCompra();
            Assert.True(crearPedido_PO.checkErrorMessage("Errores: Error al procesar tu solicitud, inténtalo de nuevo más tarde!"));
        }
    }
}
