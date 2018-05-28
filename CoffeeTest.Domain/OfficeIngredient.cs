
namespace CoffeeTest.Domain
{
    public class OfficeIngredient : BaseEntity
    {
        public Ingredient Ingredient { get; set; }
        public Office Office { get; set; }
        public int TotalUnitsLeft { get; set; }
        public double StacksLeft { get; set; }
    }
}
