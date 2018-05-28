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
            var pantryWithDrinks =
                _dbContext.PantryDrinks.Include(x => x.Pantry)
                    .Include(x => x.Drink)
                    .Include(x => x.Drink.DrinkIngredients)
                    .Where(x => x.Pantry.Id == pantryId)
                    .ToList();

            
            

            if (pantryWithDrinks == null)
                throw new Exception("Data not found");


            var pantryDrinksDto = new PantryDrinksDto();

            pantryDrinksDto.Drinks = new List<DrinkWithIngredientsDto>();
            if (pantryWithDrinks.Count != 0)
            {
                pantryDrinksDto.Pantry = new PantryDto
                {
                    PantryId = pantryId,
                    PantryName = pantryWithDrinks.Select(c => c.Pantry).First().PantryName
                };

                if (pantryWithDrinks.Select(c => c.Drink).ToList().Count == 0)
                {
                    return pantryDrinksDto;
                }

                pantryDrinksDto.Drinks = pantryWithDrinks.Select(c => new DrinkWithIngredientsDto
                {
                    DrinkName = c.Drink.DrinkName,
                    DrinkId = c.Drink.Id,
                    IngredientWithUnitsUsed =
                        _dbContext.DrinkIngredients.Include(x => x.Ingredient)
                            .Where(b => b.Drink.Id == c.Drink.Id).OrderBy(x => x.Ingredient.IngredientName)
                            .Select(y => new IngredientWithUnitsUsedDto
                            {
                                IngredientUsed = y.Ingredient.IngredientName,
                                UnitsUsed = y.UnitsUsed
                            }).ToList()
                }).ToList();
                pantryDrinksDto.Drinks = pantryDrinksDto.Drinks.OrderBy(x => x.DrinkName).ToList();
            }

            pantryDrinksDto.Pantry =
                _dbContext.Pantries.Where(x => x.Id == pantryId).Select(c => new PantryDto
                {
                    PantryName = c.PantryName,
                    PantryId = c.Id
                }).First();

            return pantryDrinksDto;
        }
    }
}
