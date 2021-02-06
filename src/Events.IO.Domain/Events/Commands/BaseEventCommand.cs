using System;
using Events.IO.Domain.Core.Commands;

namespace Events.IO.Domain.Events.Commands
{
    public abstract class BaseEventCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string ShortDescription { get; protected set; }
        public string LongDescription { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public bool Free { get; protected set; }
        public decimal Value { get; protected set; }
        public bool Online { get; protected set; }
        public string CompanyName { get; protected set; }
    }
}
