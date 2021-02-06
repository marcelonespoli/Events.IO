using Events.IO.Domain.Core.Models;
using System;

namespace Events.IO.Domain.Organizers
{
    public class Organizer : Entity<Organizer>
    {
        public Organizer(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}