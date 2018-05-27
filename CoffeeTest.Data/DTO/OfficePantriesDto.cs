using System.Collections.Generic;

namespace CoffeeTest.Data.DTO
{
    public class OfficePantriesDto
    {
        public OfficeDto Office { get; set; }
        public List<PantryDrinksDto> PantriesAndDrinks { get; set; }
    }
}
