
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
                DrinkName = "Double Americano",
            };

            var sweetLatte = new Drink
            {
                DrinkName = "Sweet Latte",
            };

            var flatWhite = new Drink
            {
                DrinkName = "Flat White",
            };

            dbContext.Drinks.Add(doubleAmericano);
            dbContext.Drinks.Add(sweetLatte);
            dbContext.Drinks.Add(flatWhite);

            #endregion

            #region Pantry Ingredients

            var pantryIngredientMilk = new OfficeIngredient
            {
                Ingredient = milk,
                Office = office,
                TotalUnitsLeft = 45,
                StacksLeft = 3
            };

            var pantryIngredientSugar = new OfficeIngredient
            {
                Ingredient = sugar,
                Office = office,
                TotalUnitsLeft = 45,
                StacksLeft = 3
            };

            var pantryIngredientCoffeeBean = new OfficeIngredient
            {
                Ingredient = coffeeBean,
                TotalUnitsLeft = 45,
                Office = office,
                StacksLeft = 3
            };

            dbContext.OfficeIngredients.Add(pantryIngredientCoffeeBean);
            dbContext.OfficeIngredients.Add(pantryIngredientSugar);
            dbContext.OfficeIngredients.Add(pantryIngredientMilk);

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

            #region Pantry Drink

            var pantryDoubleAmericano = new PantryDrink
            {
                Pantry = pantry,
                Drink = doubleAmericano
            };
            var pantrySweetLatte = new PantryDrink
            {
                Pantry = pantry,
                Drink = sweetLatte
            };
            var pantryFlatWhite = new PantryDrink
            {
                Pantry = pantry,
                Drink = flatWhite
            };

            dbContext.PantryDrinks.Add(pantryDoubleAmericano);
            dbContext.PantryDrinks.Add(pantrySweetLatte);
            dbContext.PantryDrinks.Add(pantryFlatWhite);

            #endregion

            dbContext.SaveChanges();
        }
    }
}
