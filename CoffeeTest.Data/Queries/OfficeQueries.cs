
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Data.DTO;

namespace CoffeeTest.Data.Queries
{
    public class OfficeQueries
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public OfficeQueries()
        {
            _dbContext = new CoffeeTestDbContext();
        }

        public AllOfficesDto GetAllOffices()
        {
            var offices = _dbContext.Offices.Select(c => new OfficeDto
            {
                Location = c.Location,
                OfficeId = c.Id,
                OfficeName = c.OfficeName
            }).ToList();
            return new AllOfficesDto
            {
                Offices = offices
            };
        }

        public OfficeDto GetSelectedOffice(int officeId)
        {
            var office = _dbContext.Offices.FirstOrDefault(x => x.Id == officeId);
            if (office == null)
                throw new Exception("Data not found");

            return new OfficeDto
            {
                Location = office.Location,
                OfficeName = office.OfficeName,
                OfficeId = office.Id
            };

        }

        public OfficePantriesDto GetSelectedOfficePantriesAndDrinks(int officeId)
        {
            var office = _dbContext.Offices.FirstOrDefault(x => x.Id == officeId);
            if (office == null)
                throw new Exception("Data not found");

            var officePantriesDto = new OfficePantriesDto
            {
                Office = new OfficeDto
                {
                    OfficeId = office.Id,
                    Location = office.Location,
                    OfficeName = office.OfficeName
                },
                PantriesAndDrinks = new List<PantryDrinksDto>()
            };

            var pantries =
                _dbContext.Pantries.Include(x => x.Drinks)
                    .Include(x => x.Office)
                    .Where(x => x.Office.Id == office.Id).ToList();

            foreach (var pantry in pantries)
            {
                officePantriesDto.PantriesAndDrinks.Add(new PantryDrinksDto
                {
                    Pantry = new PantryDto
                    {
                        PantryId = pantry.Id,
                        PantryName = pantry.PantryName
                    },
                    Drinks = pantry.Drinks.Select(c => new DrinkDto
                    {
                        DrinkName = c.DrinkName,
                        DrinkId = c.Id
                    }).ToList()
                });
            }
            


            return officePantriesDto;
        }
    }
}
