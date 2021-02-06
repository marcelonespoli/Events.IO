using MediatR;
using Events.IO.Domain.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Events.Commands;
using System.Threading.Tasks;
using System.Threading;
using Events.IO.Domain.Events.Repository;
using Events.IO.Domain.Interfaces;
using Events.IO.Domain.Events.Events;
using Events.IO.Domain.Core.Notifications;

namespace Events.IO.Domain.Events.Handlers
{
    public class EventCommandHandler : CommandHandler,
        IRequestHandler<RegisterEventCommand, bool>,
        IRequestHandler<UpdateEventCommand, bool>,
        IRequestHandler<DeleteEventCommand, bool>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMediatorHanlder _mediator;

        public EventCommandHandler(
            IEventRepository eventRepository,
            IMediatorHanlder mediator,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications) : base (uow, mediator, notifications)
        {
            _eventRepository = eventRepository;
            _mediator = mediator;            
        }


        public Task<bool> Handle(RegisterEventCommand message, CancellationToken cancellationToken)
        {
            var occasion = Occasion.OccasionFactory.NewOccasionComplete(message.Id, message.Name, message.ShortDescription,
                message.LongDescription, message.StartDate, message.EndDate, message.Free, message.Value,
                message.Online, message.CompanyName, message.OrganizerId, message.Address, message.Category.Id); 

            if (!IsEventValid(occasion)) return Task.FromResult(false);
            
            _eventRepository.Add(occasion);

            if (Commit())
            {
                Console.WriteLine("Event registered with success");
                _mediator.PublishEvent(new RegisteredEventEvent(occasion.Id, occasion.Name, occasion.StartDate, occasion.EndDate, occasion.Free, occasion.Value, occasion.Online, occasion.CompanyName));

                return Task.FromResult(true);
            }

            return Task.FromResult(false); 
        }

        public Task<bool> Handle(UpdateEventCommand message, CancellationToken cancellationToken)
        {
            var currentEvent = _eventRepository.GetById(message.Id);

            if (!ExistsEvent(message.Id, message.MessageType)) return Task.FromResult(false);

            //TODO validate if the event below the organizer

            var occasion = Occasion.OccasionFactory.NewOccasionComplete(message.Id, message.Name, message.ShortDescription,
                message.LongDescription, message.StartDate, message.EndDate, message.Free, message.Value,
                message.Online, message.CompanyName, message.OrganizerId, currentEvent.Address, message.Category.Id); 

            if (!IsEventValid(occasion)) return Task.FromResult(false); 
            
            _eventRepository.Update(occasion);

            if (Commit())
            {
                _mediator.PublishEvent(new UpdatedEventEvent(occasion.Id, occasion.Name, occasion.ShortDescription, occasion.LongDescription, occasion.StartDate, occasion.EndDate, occasion.Free, occasion.Value, occasion.Online, occasion.CompanyName));
                return Task.FromResult(true);
            }

            return Task.FromResult(false); 
        }

        public Task<bool> Handle(DeleteEventCommand message, CancellationToken cancellationToken)
        {
            if (!ExistsEvent(message.Id, message.MessageType)) return Task.FromResult(false);

            _eventRepository.Remove(message.Id);

            if (Commit())
            {
                _mediator.PublishEvent(new DeletedEventEvent(message.Id));
            }

            return Task.FromResult(true); 
        }

        private bool IsEventValid(Occasion occasion)
        {
            if (occasion.IsValid()) return true;

            NotifyValidationErros(occasion.ValidationResult);
            return false;
        }


        private bool ExistsEvent(Guid id, string messageType)
        {
            var occasion = _eventRepository.GetById(id);

            if (occasion != null) return true;

            _mediator.PublishEvent(new DomainNotification(messageType, "Event not found"));
            return false;
        }
    }
}
