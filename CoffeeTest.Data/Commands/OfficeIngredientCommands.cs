using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Data.DTO;
using CoffeeTest.Domain;

namespace CoffeeTest.Data.Commands
{
    public class OfficeIngredientCommands
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public OfficeIngredientCommands()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public bool AddIngredientToOffice(int sugar, int coffee, int milk, int officeId)
        {
            var office = _dbContext.Offices.FirstOrDefault(x => x.Id == officeId);

            if (office == null)
                throw new Exception("Data not found");

            var officeIngredients =
                _dbContext.OfficeIngredients.Include(x => x.Office)
                    .Include(x => x.Ingredient)
                    .Where(x => x.Office.Id == officeId)
                    .ToList();
            if (officeIngredients.Count != 0)
            {
                foreach (var officeIngredient in officeIngredients)
                {
                    if (officeIngredient.Ingredient.IngredientName.ToLower().Contains("milk"))
                    {
                        officeIngredient.TotalUnitsLeft = officeIngredient.TotalUnitsLeft + milk*15;
                        officeIngredient.StacksLeft = (double) officeIngredient.TotalUnitsLeft/15;
                    }
                    if (officeIngredient.Ingredient.IngredientName.ToLower().Contains("sugar"))
                    {
                        officeIngredient.TotalUnitsLeft = officeIngredient.TotalUnitsLeft + sugar*15;
                        officeIngredient.StacksLeft = (double) officeIngredient.TotalUnitsLeft/15;
                    }
                    if (officeIngredient.Ingredient.IngredientName.ToLower().Contains("coffee"))
                    {
                        officeIngredient.TotalUnitsLeft = officeIngredient.TotalUnitsLeft + coffee*15;
                        officeIngredient.StacksLeft = (double) officeIngredient.TotalUnitsLeft/15;
                    }
                }
            }
            else
            {
                var sugarEntity = _dbContext.Ingredients.First(x => x.IngredientName.ToLower().Contains("sugar"));
                var coffeeEntity = _dbContext.Ingredients.First(x => x.IngredientName.ToLower().Contains("coffee"));
                var milkEntity = _dbContext.Ingredients.First(x => x.IngredientName.ToLower().Contains("milk"));
                _dbContext.OfficeIngredients.Add(new OfficeIngredient
                {
                    Ingredient = sugarEntity,
                    Office = office,
                    StacksLeft = sugar,
                    TotalUnitsLeft = sugar*15
                });
                _dbContext.OfficeIngredients.Add(new OfficeIngredient
                {
                    Ingredient = coffeeEntity,
                    Office = office,
                    StacksLeft = coffee,
                    TotalUnitsLeft = coffee * 15
                });
                _dbContext.OfficeIngredients.Add(new OfficeIngredient
                {
                    Ingredient = milkEntity,
                    Office = office,
                    StacksLeft = milk,
                    TotalUnitsLeft = milk * 15
                });
            }
            _dbContext.SaveChanges();
            return true;
        }

        public PantryDrinksWithoutIngredientsDto AddDrinkModal(int pantryId)
        {
            var pantry = _dbContext.Pantries.First(x => x.Id == pantryId);
            var drinks =
                _dbContext.PantryDrinks.Include(x => x.Drink)
                    .Include(x => x.Pantry)
                    .Where(x => x.Pantry.Id != pantry.Id)
                    .Select(c => c.Drink).Distinct()
                    .ToList();
            
            var pantryDrinks = new PantryDrinksWithoutIngredientsDto();

            pantryDrinks.Pantry = new PantryDto
            {
                PantryName = pantry.PantryName, PantryId = pantry.Id
            };

            pantryDrinks.Drinks = new List<DrinkDto>();
            foreach (var drink in drinks)
            {
                pantryDrinks.Drinks.Add(new DrinkDto
                {
                    DrinkName = drink.DrinkName,
                    DrinkId = drink.Id
                });
            }


            return pantryDrinks;
        }

        
    }
}
