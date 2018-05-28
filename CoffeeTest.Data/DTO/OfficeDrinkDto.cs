

using System.Collections.Generic;

namespace CoffeeTest.Data.DTO
{
    public class OfficeDrinkDto
    {
        public OfficeDto Office { get; set; }
        public List<DrinkDto> Drinks { get; set; }
    }
}
