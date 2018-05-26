
namespace CoffeeTest.Domain
{
    public class Order : BaseEntity
    {
        public Drink Drink { get; set; }
        public Office Office { get; set; }
        public Pantry Pantry { get; set; }
    }
}
