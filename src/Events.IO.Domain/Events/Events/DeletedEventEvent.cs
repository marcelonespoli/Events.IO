using System;

namespace Events.IO.Domain.Events.Events
{
    public class DeletedEventEvent : BaseEventEvent
    {
        public DeletedEventEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
