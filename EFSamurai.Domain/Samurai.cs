using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EFSamurai.Domain
{
    public class Samurai
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public Haircut Haircut { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
    }

    public enum Haircut
    {
        Chonmage,
        Oicho,
        Western
    }
}