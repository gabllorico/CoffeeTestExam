using System;
using System.Collections.Generic;
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
    }
}
