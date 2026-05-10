using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.UC_PedirBocadillo;

namespace AppForSEII2526.UIT.UC_Rental
{
    public class UC_PedirBocadillo_UIT : UC_UIT
    {
        private SelectPedido_PO selectPedido_PO;
        private PostPedido_PO postPedido_PO;
        private DetailPedido_PO detailPedido_PO;
        private const int bocadilloID = 1;
        private const string nombreBocadillo1 = "Politecnico";
        private const string tipoPan1 = "Normal";
        private const string precio1 = "3";
        private const string tamanyo1 = "Normal";

        private const string nombreBocadillo2 = "Serrano";
        private const string tipoPan2 = "Integral";
        private const string precio2 = "5";
        private const string tamanyo2 = "Pequeño";

        public UC_PedirBocadillo_UIT(ITestOutputHelper output) : base(output)
        {
            selectPedido_PO = new SelectPedido_PO(_driver, _output);
            postPedido_PO = new PostPedido_PO(_driver, _output);
            detailPedido_PO = new DetailPedido_PO(_driver, _output);
        }

        private void InitialStepsForCompraBocadillos()
        {
            _driver.Navigate().GoToUrl("https://localhost:7081/");
            selectPedido_PO.WaitForBeingVisible(By.Id("CreateCompra"));

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(By.Id("CreateCompra"));
                    return element != null && element.Displayed && element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            // Reintenta el click si ocurre un StaleElementReferenceException
            int retries = 3;
            while (retries-- > 0)
            {
                try
                {
                    var createCompraButton = _driver.FindElement(By.Id("CreateCompra"));
                    createCompraButton.Click();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    if (retries == 0) throw;
                    Thread.Sleep(200); // Espera breve antes de reintentar
                }
            }
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void TC1_FB_1_2_3_4_5_6_7_8() //Esc_1, UC3_1
        {
            InitialStepsForCompraBocadillos();
            DateTime fechahoy = DateTime.Today;

            var expectedBocadillos = new List<string[]>
            {
                new string[]
                {
                    "Serrano", "Pequeño", "Integral", "5"
                },
                new string[]
                {
                    "Bacon", "Pequeño", "Semilla","2"
                },
                new string[]
                {
                    "Politecnico", "Normal", "Normal","3"
                },
            };

            var expectedBocadillosCompra = new List<string[]>
            {
                new string[]
                {
                    "Serrano","Integral", "5"
                },
                new string[]
                {
                    "Politecnico","Normal", "3"
                },
            };

            var expectedDatosCompra = new List<string[]>
            {
                new string[]
                {
                    "Serrano", "Integral", "5 €", "1"
                },
                new string[]
                {
                    "Politecnico", "Normal", "3 €", "1"
                },
            };

            //Pagina del select de bocadillos
            selectPedido_PO.SearchBocadillos("", "");
            Assert.True(selectPedido_PO.CheckListOfBocadillos(expectedBocadillos));
            selectPedido_PO.AddBocadilloParaComprar("Serrano");
            selectPedido_PO.AddBocadilloParaComprar("Politecnico");
            selectPedido_PO.seleccionarBotonCompra();

            //Pagina de la compra de bocadillos
            Assert.True(postPedido_PO.CheckListOfBocadillos(expectedBocadillosCompra));
            postPedido_PO.rellenarDatosParaCompra("Fernando", "Martinez", "Panadero", "Tarjeta");
            Assert.True(postPedido_PO.checkPrecio("8"));
            postPedido_PO.seleccionarBotonCompra();

            //Pagina de detalle del pedido
            Assert.True(detailPedido_PO.CheckListOfDatos("Fernando Martinez Panadero", "Tarjeta", "8", fechahoy.ToString("dd/MM/yyyy hh:mm:ss"), "8"));
            Assert.True(detailPedido_PO.CheckListOfBocadillos(expectedDatosCompra));
        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void TC1_AF1_TC1_2_CompraNotAvailable() //No hay bocadillos seleccionados
        {
            //Arrange
            InitialStepsForCompraBocadillos();
            //Act
            selectPedido_PO.AddBocadilloParaComprar(nombreBocadillo1);
            selectPedido_PO.RemoveBocadilloParaComprar(nombreBocadillo1);

            //Assert
            Assert.True(selectPedido_PO.CompraNotAvailable());
        }


        //POST
        [Theory]
        [InlineData("", "Martinez", "Panadero", "Tarjeta")] //Falta Nombre
        [InlineData("Fernando", "", "Panadero", "Tarjeta")] //Falta Primer Apellido
        [InlineData("NoExite", "NoExite", "NoExite", "Tarjeta")] //Usuario Inexistente
        [Trait("LevelTesting", "Funcional Testing")]
        public void TC1_FA3_2_3_4_5_6_7(string nombre, string apellido1, string apellido2, string metodopago) //Esc_6
        {
            InitialStepsForCompraBocadillos();
            selectPedido_PO.SearchBocadillos("", "");
            selectPedido_PO.AddBocadilloParaComprar("Serrano");
            selectPedido_PO.AddBocadilloParaComprar("Politecnico");
            selectPedido_PO.seleccionarBotonCompra();
            postPedido_PO.rellenarDatosParaCompra(nombre, apellido1, apellido2, metodopago);
            postPedido_PO.seleccionarBotonCompra();
            Assert.True(postPedido_PO.checkErrorMessage("Errors: Error while processing your request, please try again later!"));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void TC1_FA3_2_3_4_5_6_7_8() //Cantidad 0
        {
            InitialStepsForCompraBocadillos();
            selectPedido_PO.SearchBocadillos("", "");
            selectPedido_PO.AddBocadilloParaComprar("Serrano");
            selectPedido_PO.AddBocadilloParaComprar("Politecnico");
            selectPedido_PO.seleccionarBotonCompra();
            postPedido_PO.rellenarDatosParaCompra("Fernando", "Martinez", "Panadero", "PayPal");
            postPedido_PO.modificarCantidadBocadillos("1", "0");
            postPedido_PO.seleccionarBotonCompra();
            Assert.True(postPedido_PO.checkErrorMessage("Errors: Error while processing your request, please try again later!"));
        }
    }
}
