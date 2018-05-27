using System.Linq;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Data.DTO;


namespace CoffeeTest.Data.Queries
{
    public class IngredientQueries
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public IngredientQueries()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public AllIngredientsDto GetAllIngredients()
        {
            var ingredients = _dbContext.Ingredients.Select(c => new IngredientDto
            {
                IngredientId = c.Id,
                IngredientName = c.IngredientName
            }).ToList();

            return new AllIngredientsDto
            {
                Ingredients = ingredients
            };
        }
    }
}
