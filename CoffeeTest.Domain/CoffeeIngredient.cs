using System.Collections.Generic;

namespace CoffeeTest.Domain
{
    public class CoffeeIngredient : BaseEntity
    {
        public string IngredientName { get; set; }
        public int IngredientUnitsOfMeasurement { get; set; }
        public ICollection<Drink> Drinks { get; set; }
    }
}
