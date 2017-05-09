using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EFSamurai.Domain
{
    public class Samurai
    {
        public Samurai()
        {
            Quotes = new HashSet<Quote>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public Haircut Haircut { get; set; }
        public SecretIdentity Alias { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Secret Identity: {Alias.RealName}, Strength: {Strength}, Agility: {Agility}, Haircut: {Haircut}";}
        }
    }

    public enum Haircut
    {
        Chonmage,
        Oicho,
        Western
    }