using System.Collections.Generic;

namespace CoffeeTest.Domain
{
    public class Drink: BaseEntity
    {
        public Pantry Pantry { get; set; }
        public string DrinkName { get; set; }
        public ICollection<DrinkIngredient> DrinkIngredients { get; set; }
    }
}
