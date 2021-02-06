using System;

namespace Events.IO.Domain.Events.Commands
{
    public class RegisterEventCommand : BaseEventCommand
    {
        public RegisterEventCommand(string name,
            DateTime startDate,
            DateTime endDate,
            bool free,
            decimal value,
            bool online,
            string companyName,
            Guid organizerId,
            Address address,
            Category category)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Free = free;
            Value = value;
            Online = online;
            CompanyName = companyName;
            OrganizerId = organizerId;
            Address = address;
            Category = category;
        }
    }
}
