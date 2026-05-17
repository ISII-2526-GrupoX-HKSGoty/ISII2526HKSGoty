using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CompraBono_UIT
{
    public class CU_CompraBono_UIT : UC_UIT
    {

        public CU_CompraBono_UIT(ITestOutputHelper output) : base(output)
        {
            Initial_step_opening_the_web_page();
            listbonos= new SelectBono_PO(_driver, _output);
        }
        
        private SelectBono_PO listbonos;

        private const int bonoId1 = 1;
        private const string nombreBono1 = "bonoVegano";
        private const string nBocadillos1 = "5";
        private const string pvp1 = "10";
        private const string tipoBocata1 = "vegano";

        private const int bonoId2 = 2;
        private const string nombreBono2 = "bonoVegetariano";
        private const string nBocadillos2 = "3";
        private const string pvp2 = "7,5";
        private const string tipoBocata2 = "vegetariano";


        private void InitialStepsCompraBonos_UIT()
        {
            listbonos.WaitForBeingVisibleIgnoringExeptionTypes(By.Id("CrearCompraBonos"));
            _driver.FindElement(By.Id("CrearCompraBonos")).Click();
        }

        [Theory]
        [InlineData(nombreBono1, nBocadillos1, pvp1, tipoBocata1, "bonoVegano", "")]
        [InlineData(nombreBono2, nBocadillos2, pvp2, tipoBocata2, "", "vegetariano")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC_AF1_filtrosNombreTipo(string nombreBono, string nBocadillos, string pvp, string tipoBocadillo, string filtroNombre, string filtroTipo)
        {
            var expectedBonos = new List<string[]> { new string[] {nombreBono, tipoBocadillo, pvp, nBocadillos, "Add"}, };

            InitialStepsCompraBonos_UIT();

            listbonos.FiltroBono(filtroNombre, filtroTipo);

            Assert.True(listbonos.CheckListaBonos(expectedBonos));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void cantidadBono()
        {
            InitialStepsCompraBonos_UIT();
            listbonos.FiltroBono("", "");
            listbonos.SelectCantidadBonos(nombreBono1, "2");

            Thread.Sleep(500);
            Assert.True(listbonos.CheckCompraCart("20"));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_10_AF3_ModificarBonos()
        {
            
            InitialStepsCompraBonos_UIT();

            listbonos.FiltroBono("", "");
            listbonos.SelectBonos(new List<string> { nombreBono1, nombreBono2 });
            listbonos.ModificarCompraCart(nombreBono2);

            Thread.Sleep(500);
            Assert.True(listbonos.CheckCompraCart(pvp1));
            
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_11_AF4_BotonCompraNotAvailable()
        {

            InitialStepsCompraBonos_UIT();

            listbonos.FiltroBono("", "");
            listbonos.SelectBonos(new List<string> { nombreBono1 });
            listbonos.ModificarCompraCart(nombreBono1);

            Thread.Sleep(1000);

            Assert.True(listbonos.ChekComprarBonosDisabled(), "Rent button should be disabled");
        }

        [Theory]
        [InlineData("", "Cifuentes","Paypal", "The NombreCliente field is required.")]
        [InlineData("Miguel", "", "Paypal", "The Apellido1Cliente field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC_errorDatosObligatorios(string nombre, string apellido1, string metodoPago, string expectedError)
        {
            var createBono = new CreateBono_PO(_driver, _output);

            InitialStepsCompraBonos_UIT();
            listbonos.FiltroBono("", "");
            listbonos.SelectBonos(new List<string> { nombreBono1 });
            listbonos.ComprarBonos();
            createBono.rellenarCompra(nombre, apellido1, "", metodoPago);

            Thread.Sleep(500);

            createBono.botonComprar();

            Thread.Sleep(500);

            Assert.True(createBono.CheckValidationError(expectedError), $"Expected error message: {expectedError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC_ModificarCompra()
        {
            var createBono = new CreateBono_PO(_driver, _output);

            InitialStepsCompraBonos_UIT();

            listbonos.FiltroBono("", "");   
            listbonos.SelectBonos(new List<string> { nombreBono1, nombreBono2 });
            listbonos.ComprarBonos();
            createBono.botonModificar();

            listbonos.ModificarCompraCart(nombreBono2);
            listbonos.ComprarBonos();

            var expectecCompraItems = new List<string[]> { new string[] { nombreBono1, tipoBocata1, pvp1, nBocadillos1}, };
            Assert.True(createBono.CheckListCompraItems(expectecCompraItems));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC_BasicFlow()
        {
            var createBono = new CreateBono_PO(_driver, _output);
            var detailCompra = new DeatailCompra_PO(_driver, _output);

            InitialStepsCompraBonos_UIT();

            listbonos.FiltroBono("", "");
            listbonos.SelectBonos(new List<string> { nombreBono1 });
            listbonos.ComprarBonos();

            createBono.rellenarCompra("Miguel", "Cifuentes", "", "Paypal");
            createBono.botonComprar();
            createBono.PressOkModalDialog();

            Assert.True(detailCompra.CheckCompraDetail("Miguel","Cifuentes","Paypal",DateTime.Now,pvp1),"Error: detail compra no concide");

            var expectedCompraBonos = new List<string[]> { new string[] { nombreBono1,"1", pvp1, nBocadillos1, tipoBocata1 }, };

            Assert.True(detailCompra.CheckCompraBonos(expectedCompraBonos), "Error: compra bonos no concide");
        }
    }

}
