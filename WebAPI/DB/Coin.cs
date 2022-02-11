using System;
using System.Collections.Generic;

namespace WebAPI.DB
{
    public partial class Coin
    {
        public Coin()
        {
            VendingMachineCoins = new HashSet<VendingMachineCoin>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<VendingMachineCoin> VendingMachineCoins { get; set; }
    }
}
