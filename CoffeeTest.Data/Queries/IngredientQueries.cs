using System.Linq;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Data.DTO.Read;
using ShortBus;

namespace CoffeeTest.Data.Queries
{
    public class IngredientQueries
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public IngredientQueries()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public ViewAllIngredientsDto GetAllIngredients()
        {
            var ingredients = _dbContext.Ingredients.Select(c => new ViewIngredientDto
            {
                IngredientId = c.Id,
                IngredientName = c.IngredientName
            }).ToList();

            return new ViewAllIngredientsDto
            {
                Ingredients = ingredients
            };
        }
    }
}
