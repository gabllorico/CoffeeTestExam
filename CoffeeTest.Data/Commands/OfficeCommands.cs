using System;
using System.Linq;
using CoffeeTest.Data.DBContext;
using CoffeeTest.Data.DTO;
using CoffeeTest.Domain;

namespace CoffeeTest.Data.Commands
{
    public class OfficeCommands
    {
        private readonly ICoffeeTestDbContext _dbContext;

        public OfficeCommands()
        {
            _dbContext = new CoffeeTestDbContext();
        }


        public OfficeDto AddOffice(OfficeDto officeDto)
        {
            if (
                _dbContext.Offices.Any(
                    x =>
                        x.OfficeName.ToLower() == officeDto.OfficeName.ToLower() &&
                        x.Location.ToLower() == officeDto.Location.ToLower()))
            {
                throw new Exception("Already Existing");
            }
            var office = new Office
            {
                OfficeName = officeDto.OfficeName,
                Location = officeDto.Location
            };
            _dbContext.Offices.Add(office);

            _dbContext.SaveChanges();

            officeDto.OfficeId = office.Id;

            return officeDto;
        }

        public OfficeDto EditOffice(OfficeDto officeDto)
        {
            if (
                _dbContext.Offices.Any(
                    x =>
                        x.OfficeName.ToLower() == officeDto.OfficeName.ToLower() &&
                        x.Location.ToLower() == officeDto.Location.ToLower() && x.Id != officeDto.OfficeId))
            {
                throw new Exception("Already Existing");
            }
            var office = _dbContext.Offices.FirstOrDefault(x => x.Id == officeDto.OfficeId);

            if (office == null)
                throw new Exception("Data not Found");

            office.OfficeName = officeDto.OfficeName;
            office.Location = officeDto.Location;

            _dbContext.SaveChanges();

            officeDto.OfficeId = office.Id;

            return officeDto;

        }
    }
}
