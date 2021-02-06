using System;

namespace Events.IO.Domain.Events.Commands
{
    public class DeleteEventCommand : BaseEventCommand
    {
        public DeleteEventCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
