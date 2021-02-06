using Events.IO.Domain.Core.Commands;
using Events.IO.Domain.Core.Events;
using Events.IO.Domain.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace Events.IO.Domain.Handlers
{
    public class MediatorHanlder : IMediatorHanlder
    {
        private readonly IMediator _mediator;

        public MediatorHanlder(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }
    }
}
