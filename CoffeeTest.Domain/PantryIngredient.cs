
namespace CoffeeTest.Domain
{
    public class PantryIngredient : BaseEntity
    {
        public Ingredient Ingredient { get; set; }
        public Pantry Pantry { get; set; }
        public int TotalUnitsLeft { get; set; }
        public int StacksLeft { get; set; }
    }
}
