using AppForMovies.UT;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.UT_CU_CompraBonos
{
    public class GetBonos_UT : AppForMovies4SqliteUT
    {
        public GetBonos_UT() {

            var tipoBocadillo = new List<TipoBocadillo>()
            {
                new TipoBocadillo("Bacon"),
                new TipoBocadillo("Vegetal")
            };


            var bonos = new List<BonoBocadillo>()
            {
                new BonoBocadillo(20,5,"Bono1",3.5,tipoBocadillo[0]),
                new BonoBocadillo(15,10,"Bono2",6.0,tipoBocadillo[1])
            };

            _context.AddRange(bonos);
            _context.AddRange(tipoBocadillo);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]>test_getBonos_OK()
        {
            var bonosDTO_T1 = new List<GET_Bono_DTO>
            {
                new GET_Bono_DTO("Bono1",3.5,5,"Bacon"),
                new GET_Bono_DTO("Bono2",6.0,10,"Vegetal")
            };

            var bonosDTO_T2 = new List<GET_Bono_DTO>()
            {
                bonosDTO_T1[0],

            }.OrderBy(b => b.nombre).ToList();

            var bonosDTO_T3 = new List<GET_Bono_DTO>()
            {
                bonosDTO_T1[1],

            }.OrderBy(b => b.tipo).ToList();



            var allTests = new List<object[]>
            {
                new object[] {null, null, bonosDTO_T1},
                new object[] {"Bono1", null, bonosDTO_T2},
                new object[] {null, "Vegetal", bonosDTO_T3},

            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(test_getBonos_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBonos_OK(string? nombre, string? tipo, IList<GET_Bono_DTO> expectedBonos)
        {

            var controller = new BonosController(_context, null);

            var result = await controller.GetBonos(nombre, tipo);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var bonosActual = Assert.IsType<List<GET_Bono_DTO>>(okResult.Value);

            Assert.Equal(expectedBonos, bonosActual);
        }


    }
}
