using System.Collections.Generic;

namespace CoffeeTest.Data.DTO
{
    public class PantryDrinksDto
    {
        public PantryDto Pantry { get; set; }
        public List<DrinkWithIngredientsDto> Drinks { get; set; }
    }
}
