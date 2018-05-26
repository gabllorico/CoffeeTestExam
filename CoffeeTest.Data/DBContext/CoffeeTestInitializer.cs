
using System.Collections.Generic;
using System.Data.Entity;
using CoffeeTest.Domain;

namespace CoffeeTest.Data.DBContext
{
    public class CoffeeTestInitializer : IDatabaseInitializer<CoffeeTestDbContext>
    {
        public void InitializeDatabase(CoffeeTestDbContext context)
        {
            if (context.Database.Exists())
            {
                /*
                 * For purpose of test, will drop and re create DB every run
                */
                context.Database.Delete();
                context.Database.Create();

                InitialData(context);
            }
            else
            {
                context.Database.Create();
            }
        }

        private static void InitialData(CoffeeTestDbContext dbContext)
        {
            #region Ingredients

            var coffeeBean = new Ingredient
            {
                IngredientName = "Coffee"
            };

            var sugar = new Ingredient
            {
                IngredientName = "Sugar"
            };

            var milk = new Ingredient
            {
                IngredientName = "Milk"
            };

            dbContext.Ingredients.Add(coffeeBean);
            dbContext.Ingredients.Add(sugar);
            dbContext.Ingredients.Add(milk);

            #endregion

            #region Office

            var office = new Office
            {
                Location = "Location 1",
                OfficeName = "Office 1"
            };

            dbContext.Offices.Add(office);

            #endregion

            #region Pantry

            var pantry = new Pantry
            {
                Office = office,
                PantryName = "Pantry 1"
            };

            dbContext.Pantries.Add(pantry);

            #endregion

            #region Drinks

            var doubleAmericano = new Drink
            {
                DrinkName = "Double Americano"
            };

            var sweetLatte = new Drink
            {
                DrinkName = "Sweet Latte"
            };

            var flatWhite = new Drink
            {
                DrinkName = "Flat White"
            };

            dbContext.Drinks.Add(doubleAmericano);
            dbContext.Drinks.Add(sweetLatte);
            dbContext.Drinks.Add(flatWhite);

            #endregion

            #region Pantry Drinks

            var pantryDrinkDoubleAmericano = new PantryDrink
            {
                Pantry = pantry,
                Drink = doubleAmericano
            };

            var pantryDrinkSweetLatte = new PantryDrink
            {
                Drink = sweetLatte,
                Pantry = pantry
            };

            var pantryDrinkFlatWhite = new PantryDrink
            {
                Drink = flatWhite,
                Pantry = pantry
            };

            dbContext.PantryDrinks.Add(pantryDrinkDoubleAmericano);
            dbContext.PantryDrinks.Add(pantryDrinkSweetLatte);
            dbContext.PantryDrinks.Add(pantryDrinkFlatWhite);

            #endregion

            #region Pantry Ingredients

            var pantryIngredientMilk = new PantryIngredient
            {
                Ingredient = milk,
                Pantry = pantry,
                TotalUnitsLeft = 45,
                StacksLeft = 3
            };

            var pantryIngredientSugar = new PantryIngredient
            {
                Ingredient = sugar,
                Pantry = pantry,
                TotalUnitsLeft = 45,
                StacksLeft = 3
            };

            var pantryIngredientCoffeeBean = new PantryIngredient
            {
                Ingredient = coffeeBean,
                TotalUnitsLeft = 45,
                Pantry = pantry,
                StacksLeft = 3
            };

            dbContext.PantryIngredients.Add(pantryIngredientCoffeeBean);
            dbContext.PantryIngredients.Add(pantryIngredientSugar);
            dbContext.PantryIngredients.Add(pantryIngredientMilk);

            #endregion  

            #region Drink Ingredient

            var doubleAmericanoCoffeeBeans = new DrinkIngredient
            {
                Drink = doubleAmericano,
                Ingredient = coffeeBean,
                UnitsUsed = 3
            };

            var doubleAmericanoSugar = new DrinkIngredient
            {
                Drink = doubleAmericano,
                Ingredient = sugar,
                UnitsUsed = 0
            };

            var doubleAmericanoMilk = new DrinkIngredient
            {
                Drink = doubleAmericano,
                Ingredient = milk,
                UnitsUsed = 0
            };

            dbContext.DrinkIngredients.Add(doubleAmericanoCoffeeBeans);
            dbContext.DrinkIngredients.Add(doubleAmericanoMilk);
            dbContext.DrinkIngredients.Add(doubleAmericanoSugar);

            var sweetLatteCoffeeBeans = new DrinkIngredient
            {
                Drink = sweetLatte,
                Ingredient = coffeeBean,
                UnitsUsed = 2
            };

            var sweetLatteSugar = new DrinkIngredient
            {
                Drink = sweetLatte,
                Ingredient = sugar,
                UnitsUsed = 5
            };

            var sweetLatteMilk = new DrinkIngredient
            {
                Drink = sweetLatte,
                Ingredient = milk,
                UnitsUsed = 3
            };

            dbContext.DrinkIngredients.Add(sweetLatteCoffeeBeans);
            dbContext.DrinkIngredients.Add(sweetLatteSugar);
            dbContext.DrinkIngredients.Add(sweetLatteMilk);

            var flatWhiteCoffeeBeans = new DrinkIngredient
            {
                Drink = flatWhite,
                Ingredient = coffeeBean,
                UnitsUsed = 2
            };

            var flatWhiteSugar = new DrinkIngredient
            {
                Drink = flatWhite,
                Ingredient = sugar,
                UnitsUsed = 1
            };

            var flatWhiteMilk = new DrinkIngredient
            {
                Drink = flatWhite,
                Ingredient = milk,
                UnitsUsed = 4
            };

            dbContext.DrinkIngredients.Add(flatWhiteCoffeeBeans);
            dbContext.DrinkIngredients.Add(flatWhiteSugar);
            dbContext.DrinkIngredients.Add(flatWhiteMilk);

            #endregion

            dbContext.SaveChanges();
        }
    }
}
