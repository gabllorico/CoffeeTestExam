using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeTest.Data.DTO
{
    public class IngredientsUsedDto
    {
        public IngredientDto Ingredient { get; set; }
        public int UnitsUsed { get; set; }
    }
}
