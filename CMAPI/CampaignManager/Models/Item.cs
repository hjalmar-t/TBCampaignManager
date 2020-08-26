using System;
using System.Collections.Generic;

namespace CampaignManager.Models
{
    public partial class Item
    {
        public Item()
        {
            OwnedItem = new HashSet<OwnedItem>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }

        public virtual ICollection<OwnedItem> OwnedItem { get; set; }
    }
}
