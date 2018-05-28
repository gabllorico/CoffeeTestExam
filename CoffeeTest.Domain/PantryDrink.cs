
namespace CoffeeTest.Domain
{
    public class PantryDrink : BaseEntity
    {
        public Pantry Pantry { get; set; }
        public Drink Drink { get; set; }
    }
}
