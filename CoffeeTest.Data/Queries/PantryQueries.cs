using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Data.DTO;

namespace CoffeeTest.Data.Queries
{
    public class PantryQueries
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public PantryQueries()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public PantryWithOfficeIdDto GetSelectedPantry(int pantryId)
        {
            var pantry = _dbContext.Pantries.Include(x => x.Office).FirstOrDefault(x => x.Id == pantryId);

            if (pantry == null)
                throw new Exception("Data Not Found");

            return new PantryWithOfficeIdDto
            {
                PantryId = pantry.Id,
                PantryName = pantry.PantryName,
                OfficeId = pantry.Office.Id
            };
        }

        public PantryDrinksDto GetDrinksOfSelectedPantry(int pantryId)
        {
            var pantryWithDrinks = _dbContext.Drinks.Include(x => x.Pantry).Include(x => x.DrinkIngredients).Where(x => x.Pantry.Id == pantryId).ToList();

            var pantry = _dbContext.Pantries.Include(x => x.Drinks).FirstOrDefault(x => x.Id == pantryId);

            if (pantry == null)
                throw new Exception("Data not found");

            if (pantryWithDrinks == null)
                throw new Exception("Data not found");


            var pantryDrinks = new PantryDrinksDto();

            pantryDrinks.Drinks = new List<DrinkWithIngredientsDto>();

            pantryDrinks.Pantry = new PantryDto
            {
                PantryId = pantryId,
                PantryName = pantry.PantryName
            };

            if (pantry.Drinks.Count == 0)
            {
                return pantryDrinks;
            }

            pantryDrinks.Drinks = pantry.Drinks.Select(c => new DrinkWithIngredientsDto
            {
                DrinkName = c.DrinkName,
                DrinkId = c.Id,
                IngredientWithUnitsUsed =
                    _dbContext.DrinkIngredients.Include(x => x.Ingredient)
                        .Where(b => b.Drink.Id == c.Id).OrderBy(x => x.Ingredient.IngredientName)
                        .Select(y => new IngredientWithUnitsUsedDto
                        {
                            IngredientUsed = y.Ingredient.IngredientName,
                            UnitsUsed = y.UnitsUsed
                        }).ToList()
            }).ToList();
            pantryDrinks.Drinks = pantryDrinks.Drinks.OrderBy(x => x.DrinkName).ToList();
            return pantryDrinks;
        }
    }
}
