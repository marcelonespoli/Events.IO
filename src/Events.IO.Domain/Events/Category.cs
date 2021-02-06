using Events.IO.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Events.IO.Domain.Events
{
    public class Category : Entity<Category>
    {
        public Category(Guid id)
        {
            Id = id;  
        }

        public string Name { get; private set; }

        // Contructor for EF
        protected Category() { }

        // EF propery for navegating
        public virtual ICollection<Occasion> Occasions { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}