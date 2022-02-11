using System;
using System.Collections.Generic;

namespace WebAPI.DB
{
    public partial class Drink
    {
        public Drink()
        {
            VendingMachineDrinks = new HashSet<VendingMachineDrink>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public byte[]? Image { get; set; }

        public virtual ICollection<VendingMachineDrink> VendingMachineDrinks { get; set; }
    }
}
