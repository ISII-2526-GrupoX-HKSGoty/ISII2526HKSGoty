using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.PedirBocadillo_UIT_test;


namespace AppForSEII2526.UIT.PedirBocadillo_UIT_test
{
    public class CU_PedirBocadillo_UIT : UC_UIT
    {
        private SelectPedido_PO selectPedido_PO;
        private PostPedido_PO postPedido_PO;
        private DetailPedido_PO detailPedido_PO;
        private const int bocadilloID = 1;
        private const string nombreBocadillo1 = "Politecnico";
        private const string tipoPan1 = "Normal";
        private const string precio1 = "3";
        private const string tamaño1 = "Normal";

        private const string nombreBocadillo2 = "Completo2";
        private const string tipoPan2 = "Integral";
        private const string precio2 = "5";
        private const string tamaño2 = "Pequeño";

        public CU_PedirBocadillo_UIT(ITestOutputHelper output) : base(output)
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
        public void CU1_1_FB_Compranormal() //CU1_1	Esc-1: Flujo Básico
        {
            InitialStepsForCompraBocadillos();
            DateTime fechahoy = DateTime.Today;

            var expectedBocadillos = new List<string[]>
            {
                new string[]
                {
                    "Politecnico", "Normal", "Normal","3"
                    
                },
                new string[]
                {
                    "Completo2", "Pequeño", "Integral", "5"
                },
                new string[]
                {
                    "Bacon", "Pequeño", "Semilla","2"
                },
            };

            var expectedBocadillosCompra = new List<string[]>
            {
                new string[]
                {
                    "Completo2", "Integral", "5"
                },
                new string[]
                {
                    "Politecnico", "Normal", "3"
                },
            };
            var expectedDatosCompra = new List<string[]>
            {
                new string[]
                {
                    "Politecnico", "Normal", "3 €", "1"
                },
                new string[]
                {
                    "Completo2", "Integral", "5 €", "1"
                },
            };

            //Pagina del select de bocadillos
            selectPedido_PO.SearchBocadillos("", "");
            Assert.True(selectPedido_PO.CheckListOfBocadillos(expectedBocadillos));
            selectPedido_PO.AddBocadilloParaComprar("Completo2");
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
        public void CU1_2_CompraNoValida() //CU1_2	Esc-2: Sin Disponibilidad No hay bocadillos seleccionados
        {
            //Arrange
            InitialStepsForCompraBocadillos();
            //Act
            selectPedido_PO.AddBocadilloParaComprar(nombreBocadillo1);
            selectPedido_PO.RemoveBocadilloParaComprar(nombreBocadillo1);

            //Assert
            Assert.True(selectPedido_PO.CompraNotAvailable());
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_3_FiltroTamaño() //CU1_3	Esc-3: Filtrado de bocadillos por Tamaño
        {
            InitialStepsForCompraBocadillos();
            DateTime fechahoy = DateTime.Today;

            var expectedBocadillos = new List<string[]>
            {
                new string[]
                {
                    "Completo2", "Pequeño", "Integral", "5"
                },
                new string[]
                {
                    "Bacon", "Pequeño", "Semilla","2"
                },
            };

            //Pagina del select de bocadillos
            selectPedido_PO.SearchBocadillos("Pequeño", "");
            Assert.True(selectPedido_PO.CheckListOfBocadillos(expectedBocadillos));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_4_FiltroTipoPan() //CU1_4	Esc-3: Filtrado de bocadillos por Tipo_pan
        {
            InitialStepsForCompraBocadillos();
            DateTime fechahoy = DateTime.Today;

            var expectedBocadillos = new List<string[]>
            {
                new string[]
                {
                    "Bacon", "Pequeño", "Semilla","2"
                },
            };

            //Pagina del select de bocadillos
            selectPedido_PO.SearchBocadillos("", "Semilla");
            Assert.True(selectPedido_PO.CheckListOfBocadillos(expectedBocadillos));
        }


        //POST
        [Theory]
        [InlineData("", "Martinez", "Panadero", "Tarjeta")] //Falta Nombre
        [InlineData("Fernando", "", "Panadero", "Tarjeta")] //Falta Primer Apellido
        [InlineData("Fernando", "Alonso", "NoExite", "Tarjeta")] //Usuario Inexistente
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_5_6_7_Falta_De_Datos(string nombre, string apellido1, string apellido2, string metodopago) //Esc_5, Esc_6, Esc_7 y Esc_8.
        {
            InitialStepsForCompraBocadillos();
            selectPedido_PO.AddBocadilloParaComprar("Completo2");
            selectPedido_PO.AddBocadilloParaComprar("Politecnico");
            selectPedido_PO.seleccionarBotonCompra();
            postPedido_PO.rellenarDatosParaCompra(nombre, apellido1, apellido2, metodopago);
            postPedido_PO.seleccionarBotonCompra();
            Assert.True(postPedido_PO.checkErrorMessage("Errors: Error while processing your request, please try again later!"));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_8_Cantida_Menor_1() //CU1_8	Esc-4:Falta introducir Cantidad mayor que 0
        {
            InitialStepsForCompraBocadillos();
            selectPedido_PO.AddBocadilloParaComprar("Completo2");
            selectPedido_PO.AddBocadilloParaComprar("Politecnico");
            selectPedido_PO.seleccionarBotonCompra();
            postPedido_PO.rellenarDatosParaCompra("Fernando", "Martinez", "Panadero", "PayPal");
            postPedido_PO.modificarCantidadBocadillos("1", "0");
            postPedido_PO.seleccionarBotonCompra();
            Assert.True(postPedido_PO.checkErrorMessage("Errors: Error while processing your request, please try again later!"));
        }

        [Theory]
        [InlineData("Politecnico", "Normal", "3 €", "1")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_10_Modificar_Carrito_Select(string bocadillo, string pan, string precio, string cantidad) //CU1_10	Esc-10: Modificar el carrito Select
        {
            InitialStepsForCompraBocadillos();
            selectPedido_PO.AddBocadilloParaComprar("Completo2");
            selectPedido_PO.AddBocadilloParaComprar("Politecnico");
            selectPedido_PO.RemoveBocadilloParaComprar("Completo2");
            selectPedido_PO.seleccionarBotonCompra();
            postPedido_PO.rellenarDatosParaCompra("Fernando", "Martinez", "Panadero", "PayPal");
            postPedido_PO.seleccionarBotonCompra();
            var expectedDatosCompraModificSelect = new List<string[]>
            {
                new string[]
                {
                    bocadillo, pan, precio, cantidad
                },
            };
            Assert.True(detailPedido_PO.CheckListOfBocadillos(expectedDatosCompraModificSelect));
        }

        [Theory]
        [InlineData("Politecnico", "Normal", "3 €", "2")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_11_Modificar_Carrito_Post(string bocadillo, string pan, string precio, string cantidad) //CU1_11	Esc-11: Modificar el carrito Post
        {
            InitialStepsForCompraBocadillos();
            selectPedido_PO.AddBocadilloParaComprar("Politecnico");
            selectPedido_PO.seleccionarBotonCompra();
            postPedido_PO.rellenarDatosParaCompra("Fernando", "Martinez", "Panadero", "PayPal");
            postPedido_PO.modificarCantidadBocadillos("1", "2");
            postPedido_PO.seleccionarBotonCompra();

            var expectedDatosCompraModificPost = new List<string[]>
            {
                new string[]
                {
                    bocadillo, pan, precio, cantidad
                },
            };
            Assert.True(detailPedido_PO.CheckListOfBocadillos(expectedDatosCompraModificPost));
        }
        [Theory]
        [InlineData("Bacon", "Semilla", "2 €", "1")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_12_Exam_Extra_todo (string bocadillo, string pan, string precio, string cantidad)
        {
            InitialStepsForCompraBocadillos();
            selectPedido_PO.SearchBocadillos("", "Normal");
            selectPedido_PO.AddBocadilloParaComprar("Politecnico");
            selectPedido_PO.SearchBocadillos("Pequeño", "");
            selectPedido_PO.AddBocadilloParaComprar("Bacon");
            selectPedido_PO.RemoveBocadilloParaComprar("Politecnico");
            selectPedido_PO.seleccionarBotonCompra();
            postPedido_PO.rellenarDatosParaCompra("Fernando", "Martinez", "Panadero", "PayPal");
            postPedido_PO.seleccionarBotonCompra();

            var expectedDatosCompraModificPost = new List<string[]>
            {
                new string[]
                {
                    bocadillo, pan, precio, cantidad
                },
            };
            Assert.True(detailPedido_PO.CheckListOfBocadillos(expectedDatosCompraModificPost));
        }
    }
}