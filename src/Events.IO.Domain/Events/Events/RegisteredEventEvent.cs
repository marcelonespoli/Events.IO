using System;

namespace Events.IO.Domain.Events.Events
{
    public class RegisteredEventEvent : BaseEventEvent
    {
        public RegisteredEventEvent(
            Guid id,
            string name,
            DateTime startDate,
            DateTime endDate,
            bool free,
            decimal value,
            bool online,
            string companyName)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Free = free;
            Value = value;
            Online = online;
            CompanyName = companyName;

            AggregateId = id;
        }
    }
}
