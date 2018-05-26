using System.Collections.Generic;

namespace CoffeeTest.Domain
{
    public class Office : BaseEntity
    {
        public string OfficeName { get; set; }
        public string Location { get; set; }
        public ICollection<Pantry> Pantries { get; set; }
    }
}
