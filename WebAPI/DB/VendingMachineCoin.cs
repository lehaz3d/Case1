using System;
using System.Collections.Generic;

namespace WebAPI.DB
{
    public partial class VendingMachineCoin
    {
        public int Id { get; set; }
        public int? VendingMachinesId { get; set; }
        public int? CoinsId { get; set; }
        public int Count { get; set; }
        public bool? IsActive { get; set; }

        public virtual Coin? Coins { get; set; }
        public virtual VendingMachine? VendingMachines { get; set; }
    }
}
