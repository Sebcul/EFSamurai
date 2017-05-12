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
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public bool IsBrutal { get; set; }
        public virtual BattleLog BattleLog { get; set; }
        public int BattleLogId { get; set; }

    }
}
