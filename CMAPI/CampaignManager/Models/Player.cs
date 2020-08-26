using System;
using System.Collections.Generic;

namespace CampaignManager.Models
{
    public partial class Player
    {
        public Player()
        {
            Character = new HashSet<Character>();
        }

        public int PlayerId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Character> Character { get; set; }
    }
}
