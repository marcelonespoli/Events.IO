using Events.IO.Domain.Events.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Events.IO.Domain.Events.Handlers
{
    public class EventSystemHandlers :
        INotificationHandler<RegisteredEventEvent>,
        INotificationHandler<UpdatedEventEvent>,
        INotificationHandler<DeletedEventEvent>
    {
        public Task Handle(RegisteredEventEvent message, CancellationToken cancellationToken)
        {
            // send an email
            // register a log
            throw new NotImplementedException();
        }

        public Task Handle(UpdatedEventEvent message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(DeletedEventEvent message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
