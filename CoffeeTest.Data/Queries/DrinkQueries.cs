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
    public class DrinkQueries
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public DrinkQueries()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public AllDrinksDto GetAllDrinks()
        {
            var drinks = _dbContext.Drinks.Select(c => new DrinkDto
            {
                DrinkId = c.Id,
                DrinkName = c.DrinkName
            }).ToList();

            return new AllDrinksDto
            {
                Drinks = drinks
            };
        }

        public DrinksDistributionDto GetAllCountOfDrinks()
        {
            var drinkDistributionDto = new DrinksDistributionDto();
            drinkDistributionDto.DrinkTally = new List<DrinkWithTallyDto>();

            var orders = _dbContext.Orders.Include(x => x.Drink).ToList();
            var drinks = _dbContext.Drinks.ToList();

            foreach (var drink in drinks)
            {
                drinkDistributionDto.DrinkTally.Add(new DrinkWithTallyDto
                {
                    DrinkName = drink.DrinkName,
                    DrinkCount = orders.Count(x => x.Drink.Id == drink.Id)
                });
            }

            return drinkDistributionDto;
        }
    }
}
