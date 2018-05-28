using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeTest.Data.DTO
{
    public class PantryDrinksWithoutIngredientsDto
    {
        public PantryDto Pantry { get; set; }
        public List<DrinkDto> Drinks { get; set; }
    }
}
