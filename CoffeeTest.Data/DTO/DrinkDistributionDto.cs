
using System.Collections.Generic;

namespace CoffeeTest.Data.DTO
{
    public class DrinksDistributionDto
    {
        public List<DrinkWithTallyDto> DrinkTally { get; set; }
    }

    public class DrinkWithTallyDto
    {
        public string DrinkName { get; set;}
        public int DrinkCount { get; set; }
    }
}
