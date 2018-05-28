
using System.Collections.Generic;

namespace CoffeeTest.Data.DTO
{
    public class OfficeIngredientsDto
    {
        public OfficeDto Office { get; set; }
        public List<IngredientWithStocksLeftDto> IngredientStocksLeft { get; set; }
    }
}
