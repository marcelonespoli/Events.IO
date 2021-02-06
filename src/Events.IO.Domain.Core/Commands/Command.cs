using System;
using MediatR;
using Events.IO.Domain.Core.Events;

namespace Events.IO.Domain.Core.Commands
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now; 
        }
    }
}
