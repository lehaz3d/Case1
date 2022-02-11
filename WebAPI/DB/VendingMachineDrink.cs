using System;
using System.Collections.Generic;

namespace WebAPI.DB
{
    public partial class VendingMachineDrink
    {
        public int Id { get; set; }
        public int? VendingMachinesId { get; set; }
        public int? DrinksId { get; set; }
        public int Count { get; set; }

        public virtual Drink? Drinks { get; set; }
        public virtual VendingMachine? VendingMachines { get; set; }
    }
}
