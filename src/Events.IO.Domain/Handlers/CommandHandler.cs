using Events.IO.Domain.Core.Notifications;
using Events.IO.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Handlers
{
    public abstract class CommandHandler 
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHanlder _mediator;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(
            IUnitOfWork uow, 
            IMediatorHanlder mediator, 
            INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotifyValidationErros(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Console.WriteLine(item.ErrorMessage);
                _mediator.PublishEvent(new DomainNotification(item.PropertyName, item.ErrorMessage));
;            }
        }

        protected bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            var commandResponse = _uow.Comit();
            if (commandResponse.Success) return true;

            Console.WriteLine("Occurred an error saving the data");
            _mediator.PublishEvent(new DomainNotification("Commit", "Occurred an error saving the data"));
            return false; 
        }
    }
}
