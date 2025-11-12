using AppForMovies.UT;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.Pedido_test
{
    public class GetPedido_Test : AppForMovies4SqliteUT
    {
        public GetPedido_Test()
        {
            var tipoPan = new List<TipoPan>() {
            new TipoPan("Baguette", 0),
            new TipoPan("Integral", 1),
            new TipoPan("Molde", 2),
            new TipoPan("Chapata", 3),
            new TipoPan("Cereal", 4),
            new TipoPan("Sin gluten", 5)

            };

            var bocadillos = new List<Bocadillo>()
            {
                new Bocadillo(1, "Vegetal", 5, 20, tipoPan[1], Tamaño.normal),
                new Bocadillo(2, "Atún", 5, 15, tipoPan[0], Tamaño.pequeño),
                new Bocadillo(3, "Jamón y queso", 7, 10, tipoPan[3], Tamaño.normal),
                new Bocadillo(4, "Politecnico", 4, 5, tipoPan[2], Tamaño.pequeño),
                new Bocadillo(5, "Completo", 9, 8, tipoPan[4], Tamaño.normal),
                new Bocadillo(6, "Trifasico", 3, 12, tipoPan[5], Tamaño.pequeño), 
                new Bocadillo(7, "Bufalo", 2, 7, tipoPan[0], Tamaño.normal),
                new Bocadillo(8, "Sumarino", 6, 9, tipoPan[1], Tamaño.pequeño)
            };
            
            ApplicationUser user = new ApplicationUser("Fernando", "Martinez", "Panadero");

            _context.Add(user);
            _context.AddRange(bocadillos);
            _context.AddRange(tipoPan);
            _context.SaveChanges();

        }
        public static IEnumerable<object[]> TestCasosPara_GetPedido_Test_Ok()
        {
            var tipoPan = new List<TipoPan>() {
            new TipoPan("Baguette", 0),
            new TipoPan("Integral", 1),
            new TipoPan("Molde", 2),
            new TipoPan("Chapata", 3),
            new TipoPan("Cereal", 4),
            new TipoPan("Sin gluten", 5)

            };
            var bocadilloDTOs = new List<BocadilloDTO>()
            {

                new BocadilloDTO(1, "Vegetal", tipoPan[1], Tamaño.normal, 5),
                new BocadilloDTO(2, "Atún", tipoPan[0], Tamaño.pequeño, 5),
                new BocadilloDTO(3, "Jamón y queso", tipoPan[3], Tamaño.normal, 5),
                new BocadilloDTO (4, "Politecnico", tipoPan[2], Tamaño.pequeño, 5),
                new BocadilloDTO (5, "Completo", tipoPan[4], Tamaño.normal, 5),
                new BocadilloDTO (6, "Trifasico", tipoPan[5], Tamaño.pequeño, 5),
                new BocadilloDTO (7, "Bufalo", tipoPan[0], Tamaño.normal, 5),
                new BocadilloDTO (8, "Sumarino", tipoPan[1], Tamaño.pequeño, 5)

            };
            return bocadilloDTOs;
        }
    }
}
