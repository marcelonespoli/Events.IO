using Events.IO.Domain.Core.Events;
using System;

namespace Events.IO.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; } // event name
        public string Value { get; private set; } // message
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
            Version = 1;
            DomainNotificationId = Guid.NewGuid();
        }
    }
}
