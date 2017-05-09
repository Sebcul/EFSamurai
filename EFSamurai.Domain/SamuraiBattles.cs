using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamurai.Domain
{
    public class SamuraiBattles
    {
        public virtual Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }

        public virtual Battle Battle { get; set; }
        public int BattleId { get; set; }
    }
}
