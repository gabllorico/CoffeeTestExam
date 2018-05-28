using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CoffeeTest.Data.DTO;
using CoffeeTest.Data.DBContext;

namespace CoffeeTest.Data.Queries
{
    public class PantryIngredientQueries
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public PantryIngredientQueries()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public OfficeIngredientsDto GetIngredientsLeftOfSelectedPantry(int pantryId)
        {
            var pantry = _dbContext.Pantries.Include(x => x.Office).FirstOrDefault(x => x.Id == pantryId);

            if (pantry == null)
                throw new Exception("Data not found");

            var pantryIngredients = new OfficeIngredientsDto
            {
                Office = new OfficeDto
                {
                    OfficeId = pantry.Office.Id,
                    OfficeName = pantry.Office.OfficeName
                },
                IngredientStocksLeft = new List<IngredientWithStocksLeftDto>()
            };

            var ingredientsStocks =
                _dbContext.OfficeIngredients.Include(x => x.Office)
                    .Include(x => x.Ingredient)
                    .Where(x => x.Office.Id == pantry.Office.Id)
                    .Select(c => new IngredientWithStocksLeftDto
                    {
                        IngredientId = c.Ingredient.Id,
                        IngredientName = c.Ingredient.IngredientName,
                        UnitsLeft = c.TotalUnitsLeft
                    }).ToList();

            pantryIngredients.IngredientStocksLeft = ingredientsStocks;

            return pantryIngredients;
        }
    }
}
