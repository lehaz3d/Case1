using System;
using System.Collections.Generic;

namespace WebAPI.DB
{
    public partial class VendingMachine
    {
        public VendingMachine()
        {
            VendingMachineCoins = new HashSet<VendingMachineCoin>();
            VendingMachineDrinks = new HashSet<VendingMachineDrink>();
        }

        public int Id { get; set; }
        public string SecretCode { get; set; } = null!;

        public virtual ICollection<VendingMachineCoin> VendingMachineCoins { get; set; }
        public virtual ICollection<VendingMachineDrink> VendingMachineDrinks { get; set; }
    }
}
