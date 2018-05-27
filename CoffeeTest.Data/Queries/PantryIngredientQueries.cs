using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
