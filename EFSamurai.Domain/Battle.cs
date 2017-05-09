using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFSamurai.Domain
{
    public class Battle
    {
        public int Id { get; set; }
        public ICollection<SamuraiBattles> SamuraiBattles {get; set; }
        public virtual BattleLog BattleLog { get; set; }

    }
}
