using System;
using System.ComponentModel.DataAnnotations;

namespace EFSamurai.Domain
{
    public class Samurai
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Strength { get; set; }

        public int Agility { get; set; }
    }
}
