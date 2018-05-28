using System;
using System.Linq;
using System.Collections.Generic;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Data.DTO;
using CoffeeTest.Domain;

namespace CoffeeTest.Data.Commands
{
    public class PantryCommand
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public PantryCommand()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public PantryWithOfficeIdDto AddPantry(PantryWithOfficeIdDto pantryDto)
        {
            if (_dbContext.Pantries.Any(x => x.PantryName.ToLower() == pantryDto.PantryName.ToLower() && x.Office.Id == pantryDto.OfficeId))
                throw new Exception("Pantry Already Exists");
            var office = _dbContext.Offices.FirstOrDefault(x => x.Id == pantryDto.OfficeId);

            if (office == null)
                throw new Exception("Data not found");

            var pantry = new Pantry
            {
                PantryName = pantryDto.PantryName,
                Office = office
            };

            _dbContext.Pantries.Add(pantry);
            _dbContext.SaveChanges();
            pantryDto.PantryId = pantry.Id;
            return pantryDto;
        }

        public bool AddDrinkToPantry(List<int> drinkIds, int pantryId)
        {
            var pantry = _dbContext.Pantries.FirstOrDefault(x => x.Id == pantryId);
            var drinks = _dbContext.Drinks.Where(x => drinkIds.Contains(x.Id)).ToList();

            foreach (var drink in drinks)
            {
                _dbContext.PantryDrinks.Add(new PantryDrink
                {
                    Pantry = pantry,
                    Drink = drink
                });
            }

            _dbContext.SaveChanges();
            return true;
        }

    }
}
