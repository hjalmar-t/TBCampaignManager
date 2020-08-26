using System;
using System.Collections.Generic;

namespace CampaignManager.Models
{
    public partial class Character
    {
        public int CharacterId { get; set; }
        public int PlayerId { get; set; }
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual Player Player { get; set; }
    }
}
