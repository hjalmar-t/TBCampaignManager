using System;
using System.Collections.Generic;

namespace CampaignManager.Models
{
    public partial class Inventory
    {
        public Inventory()
        {
            Character = new HashSet<Character>();
        }

        public int InventoryId { get; set; }
        public int WeightLimit { get; set; }

        public virtual ICollection<Character> Character { get; set; }
    }
}
