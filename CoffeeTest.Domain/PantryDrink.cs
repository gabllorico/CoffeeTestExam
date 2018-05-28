using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeTest.Domain
{
    public class PantryDrink : BaseEntity
    {
        public Pantry Pantry { get; set; }
        public Drink Drink { get; set; }
    }
}
