using AppForMovies.UT;
using AppForSEII2526.API.Controllers.BonosContollers;
using AppForSEII2526.API.DTOs.DTOsCompraBono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.Bono_test
{
    public class GetBono_UT : AppForMovies4SqliteUT
    {
        public GetBono_UT()
        {
            var tipoBocadillo = new List<TipoBocadillo>()
            {
                new TipoBocadillo("Vegetal"),
                new TipoBocadillo("Bacon"),
            };

            var bonos = new List<BonoBocadillo>()
            {
                new BonoBocadillo(50,8,"Bono1", 10, tipoBocadillo[0]),
                new BonoBocadillo(50,10,"Bono2", 11, tipoBocadillo[1]),
            };

            _context.AddRange(tipoBocadillo);
            _context.AddRange(bonos);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> Test_GetBono_UT_Ok()
        {
            var bonoDTO_T1 = new List<Get_BonosDTO>()
            {
                new Get_BonosDTO("Bono1", 10, 8, "Vegetal"),
                new Get_BonosDTO("Bono2", 11, 10, "Bacon")
            };

            var bonoDTO_T2 = new List<Get_BonosDTO>()
            {
                bonoDTO_T1[0]

            }.OrderBy(b => b.Nombre).ToList();

            var bonoDTO_T3 = new List<Get_BonosDTO>()
            {
                bonoDTO_T1[1]

            }.OrderBy(b => b.Nombre).ToList();

            var allTests = new List<object[]>
            {
                new object[] { null, null, bonoDTO_T1 }, 
                new object[] { "Bono1", null, bonoDTO_T2 }, 
                new object[] { null, "Bacon", bonoDTO_T3 }, 
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(Test_GetBono_UT_Ok))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting","Unit Testing")]
        public async Task GetBono_OK(string? nombre, string? tipo, IList<Get_BonosDTO> expectedBonos)
        {
            var controller = new BonosController(_context, null);

            var result = await controller.GetBonos(nombre, tipo);

            var okResult = Assert.IsType<OkObjectResult>(result);   

            var bonoActual = Assert.IsType<List<Get_BonosDTO>>(okResult.Value);

            Assert.Equal(expectedBonos, bonoActual);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBono_NotFound()
        {
            var mock = new Mock<ILogger<BonosController>>();
            ILogger<BonosController> logger = mock.Object;

            var controller = new BonosController(_context, logger);

            var result = await controller.GetBonos("Bono27", "Vegetal");

            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}
