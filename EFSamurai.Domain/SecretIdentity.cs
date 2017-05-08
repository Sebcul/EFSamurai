using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamurai.Domain
{
    public class SecretIdentity
    {
        public int Id { get; set; }
        public string RealName { get; set; }
        public virtual Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }
    }
}
