﻿using System.Linq;
using CoffeeTest.Data.DBContext;
using ShortBus;


namespace CoffeeTest.Data.Commands
{
    public class IngredientCommand
    {
        public int PantryId { get; set; }
        public int IngredientId { get; set; }
        public int Bags { get; set; }
    }
}
