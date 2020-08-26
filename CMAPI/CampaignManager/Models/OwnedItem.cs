using System;
using System.Collections.Generic;

namespace CampaignManager.Models
{
    public partial class OwnedItem
    {
        public int OwnedItemId { get; set; }
        public int InventoryId { get; set; }
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
