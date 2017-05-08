﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace EFSamurai.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual bool IsFunny { get; set; }
        public QuoteType Type { get; set; }
        public virtual Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }
    }

    public enum QuoteType
    {
        Lame,
        Cheesy,
        Awesome
    }
}