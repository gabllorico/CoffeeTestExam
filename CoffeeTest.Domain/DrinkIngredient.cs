using System.Collections.Generic;

namespace CoffeeTest.Domain
{
    public class DrinkIngredient : BaseEntity
    {
        public Drink Drink { get; set; }
        public Ingredient Ingredient { get; set; }
        public int UnitsUsed { get; set; }
    }
}
