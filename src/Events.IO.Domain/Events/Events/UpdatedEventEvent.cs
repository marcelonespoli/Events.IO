using System;

namespace Events.IO.Domain.Events.Events
{
    public class UpdatedEventEvent : BaseEventEvent
    {
        public UpdatedEventEvent(
            Guid id,
            string name,
            string shortDescription,
            string longDescription,
            DateTime startDate,
            DateTime endDate,
            bool free,
            decimal value,
            bool online,
            string companyName)
        {
            Id = id;
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
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
