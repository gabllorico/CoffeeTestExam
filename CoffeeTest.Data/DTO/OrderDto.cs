using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeTest.Data.DTO
{
    public class OrderDto
    {
        public DrinkDto Drink { get; set; }
        public PantryDto Pantry { get; set; }
        public OfficeDto Office { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
