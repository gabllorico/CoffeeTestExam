using System.Collections.Generic;

namespace CoffeeTest.Domain
{
    public class Drink: BaseEntity
    {
        public string DrinkName { get; set; }
        public ICollection<DrinkIngredient> DrinkIngredients { get; set; }
    }
}
