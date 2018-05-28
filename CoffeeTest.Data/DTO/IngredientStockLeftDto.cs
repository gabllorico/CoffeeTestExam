using System.Collections.Generic;

namespace CoffeeTest.Data.DTO
{
    public class IngredientStockLeftDto
    {
        public string IngredientName { get; set; }
        public int UnitStocksLeft { get; set; }
        public double TotalStocksLeft { get; set; }
    }

    public class AllIngredientsStocksLeftPerOfficeDto
    {
        public OfficeDto Office { get; set; }
        public List<IngredientStockLeftDto> IngredientStockLeft { get; set; }
        public bool IsDefaultStock { get; set; }
    }
}
