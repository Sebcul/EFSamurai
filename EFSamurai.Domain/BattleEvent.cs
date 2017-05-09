using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamurai.Domain
{
    public class BattleEvent
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string EventSummary { get; set; }
        public string Description { get; set; }
    }
}
