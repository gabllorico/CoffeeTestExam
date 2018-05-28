using System.Collections.Generic;


namespace CoffeeTest.Data.DTO
{
    public class SelectedOfficeOrderHistoryDto
    {
        public string OfficeName { get; set; }
        public int OfficeId { get; set; }
        public List<DrinkDto> Drinks { get; set; }
        public List<IngredientsUsedDto> IngredientsUsed { get; set; }
    }
}
