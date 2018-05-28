using System;
using System.Data.Entity;
using System.Linq;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Domain;

namespace CoffeeTest.Data.Commands
{
    public class OrderCommands
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public OrderCommands()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public bool OrderDrinks(int drinkId, int pantryId)
        {
            var drink = _dbContext.Drinks.FirstOrDefault(x => x.Id == drinkId);

            if (drink == null)
                throw new Exception("Data not found");

            var pantry = _dbContext.Pantries.Include(x => x.Office).FirstOrDefault(x => x.Id == pantryId);

            if (pantry == null)
                throw new Exception("Data not found");

            var officeIngredients =
                _dbContext.OfficeIngredients.Include(x => x.Ingredient)
                    .Include(x => x.Office)
                    .Where(x => x.Office.Id == pantry.Office.Id)
                    .ToList();

            var drinkIngredients =
                _dbContext.DrinkIngredients.Include(x => x.Ingredient)
                    .Include(x => x.Drink)
                    .Where(x => x.Drink.Id == drink.Id)
                    .ToList();

            var hasEnough = true;
            foreach (var officeIngredient in officeIngredients)
            {
                var drinkIngredient =
                    drinkIngredients.First(x => x.Ingredient.Id == officeIngredient.Ingredient.Id);

                if (drinkIngredient.UnitsUsed >= officeIngredient.TotalUnitsLeft)
                {
                    hasEnough = false;
                    break;
                }

                officeIngredient.TotalUnitsLeft = officeIngredient.TotalUnitsLeft - drinkIngredient.UnitsUsed;
            }


            if (hasEnough)
            {
                var order = new Order
                {
                    Drink = drink,
                    Pantry = pantry,
                    Office = pantry.Office
                };

                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
            }

            return hasEnough;
        }
    }
}
