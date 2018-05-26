
using System.Data.Entity;
using CoffeeTest.Domain;

namespace CoffeeTest.Data.DBContext
{
    public class CoffeeTestDbContext : BaseDbContext, ICoffeeTestDbContext
    {
        public CoffeeTestDbContext() : base("CoffeeTestDbContext")
        {
            
        }

        public CoffeeTestDbContext(string connectionstring) : base(connectionstring)
        {
            
        }

        public IDbSet<Drink> Drinks { get; set; }
        public IDbSet<DrinkIngredient> DrinkIngredients { get; set; }
        public IDbSet<Ingredient> Ingredients { get; set; }
        public IDbSet<Office> Offices { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Pantry> Pantries { get; set; }
        public IDbSet<PantryDrink> PantryDrinks { get; set; }
        public IDbSet<PantryIngredient> PantryIngredients { get; set; }

    }

    public interface ICoffeeTestDbContext
    {
        //Tables
        IDbSet<Drink> Drinks { get; set; }
        IDbSet<DrinkIngredient> DrinkIngredients { get; set; }
        IDbSet<Ingredient> Ingredients { get; set; }
        IDbSet<Office> Offices { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<Pantry> Pantries { get; set; }
        IDbSet<PantryDrink> PantryDrinks { get; set; }
        IDbSet<PantryIngredient> PantryIngredients { get; set; }
    }
}
