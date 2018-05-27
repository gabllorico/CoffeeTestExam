
using System.Collections.Generic;

namespace CoffeeTest.Data.DTO
{
    public class DrinkWithIngredientsDto
    {
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public List<IngredientWithUnitsUsedDto> IngredientWithUnitsUsed { get; set; }
    }
}
