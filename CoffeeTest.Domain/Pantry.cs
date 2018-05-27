using System.Collections.Generic;

namespace CoffeeTest.Domain
{
    public class Pantry : BaseEntity
    {
        public Office Office { get; set; }
        public string PantryName { get; set; }
        public ICollection<Drink> Drinks { get; set; } 
        
    }
}
