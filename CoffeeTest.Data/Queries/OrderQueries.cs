

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Data.DTO;

namespace CoffeeTest.Data.Queries
{
    public class OrderQueries
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public OrderQueries()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public AllOfficeOrderHistoryDto OrdersHistoryPerOffice()
        {
            var orders = _dbContext.Orders.Include(x => x.Drink).Include(x => x.Drink.DrinkIngredients).Include(x => x.Pantry).Include(x => x.Office).ToList();

            var listOfDrinkIngredients = _dbContext.DrinkIngredients.Include(x => x.Drink).Include(x => x.Ingredient).ToList();

            var allOffices = new AllOfficeOrderHistoryDto
            {
                Offices = new List<SelectedOfficeOrderHistoryDto>()
            };

            foreach (var order in orders)
            {
                if (!allOffices.Offices.Any(x => x.OfficeId == order.Office.Id))
                {
                    allOffices.Offices.Add(new SelectedOfficeOrderHistoryDto
                    {
                        OfficeId = order.Office.Id,
                        OfficeName = order.Office.OfficeName,
                        Drinks = new List<DrinkDto>(),
                        IngredientsUsed = new List<IngredientsUsedDto>()
                    });
                }
            }
            foreach (var office in allOffices.Offices)
            {
                foreach (var order in orders)
                {
                    office.Drinks.Add(new DrinkDto
                    {
                        DrinkName = order.Drink.DrinkName,
                        DrinkId = order.Drink.Id
                    });

                    foreach (var listOfDrinkIngredient in listOfDrinkIngredients.Where(x => x.Drink.Id == order.Drink.Id).ToList())
                    {
                        if (
                            !office.IngredientsUsed.Any(
                                x => x.Ingredient.IngredientId == listOfDrinkIngredient.Ingredient.Id))
                        {
                            office.IngredientsUsed.Add(new IngredientsUsedDto
                            {
                                Ingredient = new IngredientDto
                                {
                                    IngredientId =
                                        order.Drink.DrinkIngredients.First(x => x.Id == listOfDrinkIngredient.Id)
                                            .Ingredient.Id,
                                    IngredientName =
                                        order.Drink.DrinkIngredients.First(x => x.Id == listOfDrinkIngredient.Id)
                                            .Ingredient.IngredientName
                                },
                                UnitsUsed =
                                    order.Drink.DrinkIngredients.First(x => x.Id == listOfDrinkIngredient.Id).UnitsUsed
                            });
                        }
                        else
                        {
                            
                            office.IngredientsUsed.First(
                                x => x.Ingredient.IngredientId == listOfDrinkIngredient.Ingredient.Id).UnitsUsed =
                                office.IngredientsUsed.First(
                                    x => x.Ingredient.IngredientId == listOfDrinkIngredient.Ingredient.Id).UnitsUsed +
                                order.Drink.DrinkIngredients.First(x => x.Ingredient.Id == listOfDrinkIngredient.Ingredient.Id).UnitsUsed;
                        }
                    }
                }

                
            }
            return allOffices;

        }

        public AllOrdersDto GetAllOrders()
        {
            var orders = _dbContext.Orders.Include(x => x.Pantry).Include(x => x.Office).Include(x => x.Drink).ToList();
            var allOrdersDto = new AllOrdersDto();
            allOrdersDto.Orders = new List<OrderDto>();
            foreach (var order in orders)
            {
                allOrdersDto.Orders.Add(new OrderDto
                {
                    Drink = new DrinkDto
                    {
                        DrinkId = order.Drink.Id,
                        DrinkName = order.Drink.DrinkName
                    },
                    Pantry = new PantryDto
                    {
                        PantryName = order.Pantry.PantryName,
                        PantryId = order.Pantry.Id
                    },
                    Office = new OfficeDto
                    {
                        OfficeId = order.Office.Id,
                        OfficeName = order.Office.OfficeName
                    }, OrderDate = order.DateCreated
                });
            }
            return allOrdersDto;
        }

        public AllIngredientsStocksLeftPerOfficeDto GetStocksLeftByOffice(int officeId)
        {
            var office = _dbContext.Offices.FirstOrDefault(x => x.Id == officeId);

            if (office == null)
                throw new Exception("Data not found");

            var officeIngredient = new AllIngredientsStocksLeftPerOfficeDto
            {
                Office = new OfficeDto
                {
                    OfficeId = office.Id,
                    OfficeName = office.OfficeName
                },
                IngredientStockLeft = new List<IngredientStockLeftDto>(),
                IsDefaultStock = true
            };

            var ingredientsStocks =
                _dbContext.OfficeIngredients.Include(x => x.Office)
                    .Include(x => x.Ingredient)
                    .Where(x => x.Office.Id == office.Id)
                    .Select(c => new IngredientStockLeftDto
                    {
                        IngredientName = c.Ingredient.IngredientName,
                        UnitStocksLeft = c.TotalUnitsLeft,
                        TotalStocksLeft = c.StacksLeft
                    }).ToList();

            officeIngredient.IngredientStockLeft = ingredientsStocks;

            return officeIngredient;

        }


    }
}
